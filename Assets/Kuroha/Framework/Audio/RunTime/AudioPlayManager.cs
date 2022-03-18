﻿using System.Threading.Tasks;
using Kuroha.Framework.Singleton.RunTime;
using Kuroha.Framework.Utility.RunTime;
using UnityEngine;

namespace Kuroha.Framework.Audio.RunTime
{
    /// <summary>
    /// 音频播放管理器
    /// 用于控制音频的播放, 暂停, 停止
    /// 依赖于 AudioSourceManager 和 AudioClipManager
    /// </summary>
    public class AudioPlayManager : Singleton<AudioPlayManager>
    {
        #region 编辑器 API
        
        #if KUROHA_DEBUG_MODE

        private void OnGUI()
        {
            audioClipManager?.InspectorUpdate();
        }
        
        #endif
        
        #endregion
        
        /// <summary>
        /// 单例
        /// </summary>
        public static AudioPlayManager Instance => InstanceBase as AudioPlayManager;

        /// <summary>
        /// 音频播放器
        /// </summary>
        [SerializeField]
        private AudioSourceManager audioSourceManager;
        
        /// <summary>
        /// 音频资源
        /// </summary>
        [SerializeField]
        private AudioClipManager audioClipManager;

        /// <summary>
        /// 初始化
        /// </summary>
        public override async Task InitAsync()
        {
            if (audioClipManager == null || audioSourceManager == null)
            {
                audioSourceManager = new AudioSourceManager(gameObject);
                audioClipManager = new AudioClipManager();
                await audioClipManager.InitAsync();
            }
        }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play(string clipID, bool isLoop = false)
        {
            var clip = audioClipManager.Get(clipID);
            var source = audioSourceManager.Get();

            if (clip == null)
            {
                DebugUtil.LogError("找不到指定 ID 的音频资源, 请检查音频数据库!", null, "red");
            }
            else
            {
                clip.Play(source, isLoop);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop(string clipID)
        {
            var clip = audioClipManager.Get(clipID);
            
            if (clip == null)
            {
                DebugUtil.LogError("找不到指定 ID 的音频资源, 请检查音频数据库!", null, "red");
            }
            else
            {
                audioSourceManager.Stop(clip.GetClipName());
            }
        }

        /// <summary>
        /// 内存卸载
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            audioSourceManager = null;
            audioClipManager = null;
        }
    }
}
