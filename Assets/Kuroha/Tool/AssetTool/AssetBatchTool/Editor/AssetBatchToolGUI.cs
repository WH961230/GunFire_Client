﻿using System;
using Kuroha.Framework.GUI.Editor.Splitter;
using Kuroha.Framework.Utility.RunTime;
using UnityEditor;
using UnityEngine;

namespace Kuroha.Tool.AssetTool.AssetBatchTool.Editor
{
    public static class AssetBatchToolGUI
    {
        /// <summary>
        /// 批处理工具类型
        /// </summary>
        public enum BatchType
        {
            AssetMoveTool,
            AssetDeleteTool,
            UnusedAssetChecker,
            AnimationClipCompress,
            MaterialShaderChecker,
            BundleAssetCounter,
            RedundantTextureReferencesCleaner,
            CheckSubEmitterInAllScene,
            SetTextureImportSettings
        }

        /// <summary>
        /// 批处理工具类型
        /// </summary>
        public static readonly string[] batches =
        {
            "资源批量移动工具",
            "资源批量删除工具",
            "废弃资源检测工具",
            "动画片段压缩工具",
            "着色器引用检测工具",
            "资源包内数量统计工具",
            "材质冗余纹理引用清除工具",
            "粒子 Sub-Emitter 检测工具",
            "批量修改纹理导入设置工具"
        };

        /// <summary>
        /// 当前的 Batch
        /// </summary>
        private static BatchType currentBatch;

        /// <summary>
        /// 滑动条
        /// </summary>
        private static Vector2 scrollView;

        /// <summary>
        /// 垂直分割布局
        /// </summary>
        private static VerticalSplitter splitter;

        /// <summary>
        /// 按钮风格
        /// </summary>
        private static GUIStyle buttonStyle;

        /// <summary>
        /// 绘制界面
        /// </summary>
        /// <param name="window"></param>
        public static void OnGUI(in EditorWindow window)
        {
            // splitter
            splitter ??= new VerticalSplitter(window, 210, 210, false);
            splitter.OnGUI(window.position, MainRect, SubRect);

            // buttonStyle
            buttonStyle ??= new GUIStyle("Button");
            buttonStyle.alignment = TextAnchor.MiddleLeft;
            buttonStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
        }

        /// <summary>
        /// 主区域
        /// </summary>
        /// <param name="rect"></param>
        private static void MainRect(Rect rect)
        {
            GUILayout.BeginArea(rect);
            {
                scrollView = GUILayout.BeginScrollView(scrollView);
                {
                    GUILayout.Space(5);
                    
                    for (var index = 0; index < batches.Length; index++)
                    {
                        var oldColor = UnityEngine.GUI.backgroundColor;
                        if (currentBatch == (BatchType)index)
                        {
                            UnityEngine.GUI.backgroundColor = new Color(92 / 255f, 223 / 255f, 240 / 255f);
                        }
                        
                        if (GUILayout.Button(batches[index], buttonStyle, GUILayout.Width(196), GUILayout.Height(30)))
                        {
                            currentBatch = (BatchType)index;
                        }
                        
                        UnityEngine.GUI.backgroundColor = oldColor;

                        if (index + 1 < batches.Length)
                        {
                            GUILayout.Space(10);
                        }
                    }
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }

        /// <summary>
        /// 子区域
        /// </summary>
        /// <param name="rect"></param>
        private static void SubRect(Rect rect)
        {
            GUILayout.BeginArea(rect);
            {
                switch (currentBatch)
                {
                    case BatchType.RedundantTextureReferencesCleaner:
                        RedundantTextureReferencesCleaner.OnGUI();
                        break;

                    case BatchType.BundleAssetCounter:
                        BundleAssetCounter.OnGUI();
                        break;

                    case BatchType.AssetDeleteTool:
                        AssetDeleteTool.OnGUI();
                        break;
                    
                    case BatchType.AssetMoveTool:
                        AssetMoveTool.OnGUI();
                        break;

                    case BatchType.MaterialShaderChecker:
                        ShaderChecker.OnGUI();
                        break;
                    
                    case BatchType.UnusedAssetChecker:
                        UnusedAssetCleaner.OnGUI();
                        break;

                    case BatchType.CheckSubEmitterInAllScene:
                        CheckSubEmitterInAllScene.OnGUI();
                        break;

                    case BatchType.AnimationClipCompress:
                        AnimationClipCompress.OnGUI();
                        break;
                    
                    case BatchType.SetTextureImportSettings:
                        SetTextureImportSettings.OnGUI();
                        break;

                    default:
                        DebugUtil.LogError("忘记注册 OnGUI 事件了!");
                        throw new ArgumentOutOfRangeException();
                }
            }
            GUILayout.EndArea();
        }
    }
}