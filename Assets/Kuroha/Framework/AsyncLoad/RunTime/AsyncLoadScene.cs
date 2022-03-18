﻿using Kuroha.Framework.Message.RunTime;
using Kuroha.Framework.Updater.RunTime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kuroha.Framework.AsyncLoad.RunTime
{
    /// <summary>
    /// 协程实现异步加载场景
    ///
    /// Load() 方法使用消息系统进行触发
    /// 
    /// </summary>
    public class AsyncLoadScene : IUpdater
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private float timer;

        /// <summary>
        /// 最短加载时长
        /// </summary>
        private float minLoadTime;

        /// <summary>
        /// 正在加载的场景的路径
        /// </summary>
        private string scenePath;

        /// <summary>
        /// 异步加载进程
        /// </summary>
        private AsyncOperation asyncOperation;

        /// <summary>
        /// 初始化
        /// </summary>
        public void OnLaunch()
        {
            MessageSystem.Instance.AddListener<AsyncLoadSceneMessage>(Load);
        }

        /// <summary>
        /// 开始加载
        /// </summary>
        private bool Load(BaseMessage message)
        {
            // 转换消息类型
            if (message is AsyncLoadSceneMessage async)
            {
                ResetAsyncLoad(async.minLoadTime);
                scenePath = async.path;

                asyncOperation = SceneManager.LoadSceneAsync(scenePath);
                asyncOperation.allowSceneActivation = false;
                Updater.RunTime.Updater.Instance.Register(this);
            }

            return true;
        }

        /// <summary>
        /// 帧更新
        /// </summary>
        public bool UpdateEvent(BaseMessage message)
        {
            if (message is UpdateMessage msg)
            {
                if (asyncOperation is { isDone: false })
                {
                    if (timer < minLoadTime)
                    {
                        timer += msg.deltaTime;
                    }
                    else
                    {
                        asyncOperation.allowSceneActivation = true;
                        Clear();
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 清理
        /// </summary>
        private void Clear()
        {
            ResetAsyncLoad(0);
            Updater.RunTime.Updater.Instance.Unregister(this);
        }

        /// <summary>
        /// 重置异步加载器
        /// </summary>
        private void ResetAsyncLoad(float min)
        {
            timer = 0;
            minLoadTime = min;
            scenePath = default;
            asyncOperation = null;
        }
    }
}
