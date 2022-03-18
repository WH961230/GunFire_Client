﻿using Kuroha.Tool.AssetTool.EffectCheckTool.Editor.GUI;
using UnityEngine;

namespace Kuroha.Tool.AssetTool.EffectCheckTool.Editor.Report
{
    /// <summary>
    /// 问题报告类
    /// </summary>
    public class EffectCheckReportInfo
    {
        /// <summary>
        /// 问题类型
        /// </summary>
        public enum EffectCheckReportType
        {
            // 1
            MeshUV,

            // 5
            FBXReadWriteEnable,
            FBXNormals,
            FBXOptimizeMesh,
            FBXMeshCompression,
            FBXWeldVertices,
            
            // 9
            ParticlePrewarm,
            ParticleCastShadows,
            ParticleRendererMode,
            ParticleMeshTrisLimit,
            ParticleMeshVertexData,
            ParticleReceiveShadows,
            ParticleSubEmittersError,
            ParticleCollisionAndTrigger,
            ParticleZeroSurface,
            
            // 12
            PrefabName,
            PrefabForbidCollider,
            PrefabTextureSize,
            PrefabDisableObject,
            PrefabMotionVectors,
            PrefabDynamicOcclusion,
            PrefabForbidParticleSystem,
            PrefabCastShadows,
            PrefabLightProbes,
            PrefabReflectionProbes,
            PrefabAnimatorCullMode,
            PrefabLODGroupRenderers,
            
            // 4
            TextureSize,
            TextureMipMaps,
            TextureReadWriteEnable,
            TextureCompressFormat,
            
            // 2
            AssetName,
            FolderName,
        }

        /// <summary>
        /// 检查的资源类型
        /// </summary>
        public EffectToolData.AssetsType assetType;

        /// <summary>
        /// 问题所指向的游戏物体
        /// 用于自动修复
        /// </summary>
        public Object asset;

        /// <summary>
        /// 问题所指向的游戏物体的资源路径
        /// </summary>
        public string assetPath;

        /// <summary>
        /// 检查项的启用情况
        /// </summary>
        public bool isEnable = true;

        /// <summary>
        /// 报告类型
        /// </summary>
        public EffectCheckReportType effectCheckReportType;

        /// <summary>
        /// 子检查项类型
        /// </summary>
        public int modeType;

        /// <summary>
        /// 子检查项参数
        /// </summary>
        public string parameter;

        /// <summary>
        /// 问题的描述
        /// </summary>
        public string content;

        /// <summary>
        /// 危险等级
        /// </summary>
        public int dangerLevel;
    }
}
