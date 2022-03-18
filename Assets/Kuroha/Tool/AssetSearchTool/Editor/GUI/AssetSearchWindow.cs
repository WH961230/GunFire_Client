﻿using System;
using Kuroha.Framework.GUI.Editor;
using Kuroha.Tool.AssetSearchTool.Editor.Data;
using UnityEditor;
using UnityEngine;

namespace Kuroha.Tool.AssetSearchTool.Editor.GUI
{
    public class AssetSearchWindow : EditorWindow
    {
        /// <summary>
        /// 查找类型 (按照什么来查找)
        /// </summary>
        private static int findTypeIndex;

        /// <summary>
        /// 标签页数据
        /// </summary>
        private static Toolbar.ToolbarData toolbarData;

        /// <summary>
        /// 标签页名称
        /// </summary>
        private static string[] toolBarNames;

        /// <summary>
        /// 窗口矩形
        /// </summary>
        public static Rect windowCurrentRect;

        /// <summary>
        /// 打开窗口
        /// </summary>
        public static void Open(int type)
        {
            findTypeIndex = type;
            var window = GetWindow<AssetSearchWindow>("资源查找");
            window.minSize = new Vector2(450, 1220);
            window.maxSize = new Vector2(450, 1220);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void OnEnable()
        {
            toolBarNames = new[] { "String", "Reference", "Dependence" };
            toolbarData = new Toolbar.ToolbarData(1200, 300, toolBarNames);
        }

        /// <summary>
        /// 绘制界面
        /// </summary>
        private void OnGUI()
        {
            // 同步窗口矩形
            windowCurrentRect = position;

            // 标签页
            findTypeIndex = Toolbar.ToolbarAnime(ref toolbarData, this, ref findTypeIndex, GUIStringSearcher.OnGUI, GUIReferenceSearcher.OnGUI, GUIDependenceSearcher.OnGUI);

            // 实现动画
            if (toolbarData.playAnime)
            {
                Repaint();
            }
        }

        /// <summary>
        /// 判断在当前的筛选规则下, 某个资源是否要显示出来
        /// </summary>
        /// <param name="asset">资源</param>
        /// <param name="path">路径</param>
        /// <param name="filter">当前的筛选规则</param>
        /// <returns></returns>
        public static bool IsDisplay(UnityEngine.Object asset, string path, int filter)
        {
            if (filter == -1)
            {
                return true;
            }

            var assetType = AssetData.GetAssetType(asset, path);
            return GetBitValue(filter, (ushort)assetType);
        }

        /// <summary>
        /// 得到位值. Unity 的筛选使用的是位, 比如选中了第一项和第三项, 那么就是: 00000101
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static bool GetBitValue(int input, ushort index)
        {
            if (index > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var val = 1 << index;
            return (input & val) == val;
        }
    }
}
