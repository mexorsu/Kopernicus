// Material wrapper generated by shader translator tool
using System;
using UnityEngine;

namespace Kopernicus
{
    namespace MaterialWrapper
    {
        public class PQSOceanSurfaceQuad : Material
        {
            // Internal property ID tracking object
            protected class Properties
            {
                // Return the shader for this wrapper
                private const String shaderName = "Terrain/PQS/Ocean Surface Quad";
                public static Shader shader
                {
                    get { return Shader.Find (shaderName); }
                }

                // Main Color, default = (1,1,1,1)
                public const String colorKey = "_Color";
                public Int32 colorID { get; private set; }

                // Color From Space, default = (1,1,1,1)
                public const String colorFromSpaceKey = "_ColorFromSpace";
                public Int32 colorFromSpaceID { get; private set; }

                // Specular Color, default = (1,1,1,1)
                public const String specColorKey = "_SpecColor";
                public Int32 specColorID { get; private set; }

                // Shininess, default = 0.078125
                public const String shininessKey = "_Shininess";
                public Int32 shininessID { get; private set; }

                // Gloss, default = 0.078125
                public const String glossKey = "_Gloss";
                public Int32 glossID { get; private set; }

                // Tex Tiling, default = 1
                public const String tilingKey = "_tiling";
                public Int32 tilingID { get; private set; }

                // Tex0, default = "white" { }
                public const String waterTexKey = "_WaterTex";
                public Int32 waterTexID { get; private set; }

                // Tex1, default = "white" { }
                public const String waterTex1Key = "_WaterTex1";
                public Int32 waterTex1ID { get; private set; }

                // Normal Tiling, default = 1
                public const String bTilingKey = "_bTiling";
                public Int32 bTilingID { get; private set; }

                // Normalmap0, default = "bump" { }
                public const String bumpMapKey = "_BumpMap";
                public Int32 bumpMapID { get; private set; }

                // Water Movement, default = 1
                public const String displacementKey = "_displacement";
                public Int32 displacementID { get; private set; }

                // Texture Displacement, default = 1
                public const String texDisplacementKey = "_texDisplacement";
                public Int32 texDisplacementID { get; private set; }

                // Water Freq, default = 1
                public const String dispFreqKey = "_dispFreq";
                public Int32 dispFreqID { get; private set; }

                // Mix, default = 1
                public const String mixKey = "_Mix";
                public Int32 mixID { get; private set; }

                // Opacity, default = 1
                public const String oceanOpacityKey = "_oceanOpacity";
                public Int32 oceanOpacityID { get; private set; }

                // Falloff Power, default = 1
                public const String falloffPowerKey = "_falloffPower";
                public Int32 falloffPowerID { get; private set; }

                // Falloff Exp, default = 2
                public const String falloffExpKey = "_falloffExp";
                public Int32 falloffExpID { get; private set; }

                // AP Fog Color, default = (0,0,1,1)
                public const String fogColorKey = "_fogColor";
                public Int32 fogColorID { get; private set; }

                // AP Height Fall Off, default = 1
                public const String heightFallOffKey = "_heightFallOff";
                public Int32 heightFallOffID { get; private set; }

                // AP Global Density, default = 1
                public const String globalDensityKey = "_globalDensity";
                public Int32 globalDensityID { get; private set; }

                // AP Atmosphere Depth, default = 1
                public const String atmosphereDepthKey = "_atmosphereDepth";
                public Int32 atmosphereDepthID { get; private set; }

                // FogColorRamp, default = "white" { }
                public const String fogColorRampKey = "_fogColorRamp";
                public Int32 fogColorRampID { get; private set; }

                // FadeStart, default = 1
                public const String fadeStartKey = "_fadeStart";
                public Int32 fadeStartID { get; private set; }

                // FadeEnd, default = 1
                public const String fadeEndKey = "_fadeEnd";
                public Int32 fadeEndID { get; private set; }

                // PlanetOpacity, default = 1
                public const String planetOpacityKey = "_PlanetOpacity";
                public Int32 planetOpacityID { get; private set; }

                // NormalXYFudge, default = 0.1
                public const String normalXYFudgeKey = "_NormalXYFudge";
                public Int32 normalXYFudgeID { get; private set; }

                // NormalZFudge, default = 1.1
                public const String normalZFudgeKey = "_NormalZFudge";
                public Int32 normalZFudgeID { get; private set; }

                // Singleton instance
                private static Properties singleton = null;
                public static Properties Instance
                {
                    get
                    {
                        // Construct the singleton if it does not exist
                        if(singleton == null)
                            singleton = new Properties();
            
                        return singleton;
                    }
                }

                private Properties()
                {
                    colorID = Shader.PropertyToID(colorKey);
                    colorFromSpaceID = Shader.PropertyToID(colorFromSpaceKey);
                    specColorID = Shader.PropertyToID(specColorKey);
                    shininessID = Shader.PropertyToID(shininessKey);
                    glossID = Shader.PropertyToID(glossKey);
                    tilingID = Shader.PropertyToID(tilingKey);
                    waterTexID = Shader.PropertyToID(waterTexKey);
                    waterTex1ID = Shader.PropertyToID(waterTex1Key);
                    bTilingID = Shader.PropertyToID(bTilingKey);
                    bumpMapID = Shader.PropertyToID(bumpMapKey);
                    displacementID = Shader.PropertyToID(displacementKey);
                    texDisplacementID = Shader.PropertyToID(texDisplacementKey);
                    dispFreqID = Shader.PropertyToID(dispFreqKey);
                    mixID = Shader.PropertyToID(mixKey);
                    oceanOpacityID = Shader.PropertyToID(oceanOpacityKey);
                    falloffPowerID = Shader.PropertyToID(falloffPowerKey);
                    falloffExpID = Shader.PropertyToID(falloffExpKey);
                    fogColorID = Shader.PropertyToID(fogColorKey);
                    heightFallOffID = Shader.PropertyToID(heightFallOffKey);
                    globalDensityID = Shader.PropertyToID(globalDensityKey);
                    atmosphereDepthID = Shader.PropertyToID(atmosphereDepthKey);
                    fogColorRampID = Shader.PropertyToID(fogColorRampKey);
                    fadeStartID = Shader.PropertyToID(fadeStartKey);
                    fadeEndID = Shader.PropertyToID(fadeEndKey);
                    planetOpacityID = Shader.PropertyToID(planetOpacityKey);
                    normalXYFudgeID = Shader.PropertyToID(normalXYFudgeKey);
                    normalZFudgeID = Shader.PropertyToID(normalZFudgeKey);
                }
            }

            // Is some random material this material 
            public static Boolean UsesSameShader(Material m)
            {
                return m.shader.name == Properties.shader.name;
            }

            // Main Color, default = (1,1,1,1)
            public new Color color
            {
                get { return GetColor (Properties.Instance.colorID); }
                set { SetColor (Properties.Instance.colorID, value); }
            }

            // Color From Space, default = (1,1,1,1)
            public Color colorFromSpace
            {
                get { return GetColor (Properties.Instance.colorFromSpaceID); }
                set { SetColor (Properties.Instance.colorFromSpaceID, value); }
            }

            // Specular Color, default = (1,1,1,1)
            public Color specColor
            {
                get { return GetColor (Properties.Instance.specColorID); }
                set { SetColor (Properties.Instance.specColorID, value); }
            }

            // Shininess, default = 0.078125
            public Single shininess
            {
                get { return GetFloat (Properties.Instance.shininessID); }
                set { SetFloat (Properties.Instance.shininessID, Mathf.Clamp(value,0.01f,1f)); }
            }

            // Gloss, default = 0.078125
            public Single gloss
            {
                get { return GetFloat (Properties.Instance.glossID); }
                set { SetFloat (Properties.Instance.glossID, Mathf.Clamp(value,0.01f,1f)); }
            }

            // Tex Tiling, default = 1
            public Single tiling
            {
                get { return GetFloat (Properties.Instance.tilingID); }
                set { SetFloat (Properties.Instance.tilingID, value); }
            }

            // Tex0, default = "white" { }
            public Texture2D waterTex
            {
                get { return GetTexture (Properties.Instance.waterTexID) as Texture2D; }
                set { SetTexture (Properties.Instance.waterTexID, value); }
            }

            public Vector2 waterTexScale
            {
                get { return GetTextureScale (Properties.waterTexKey); }
                set { SetTextureScale (Properties.waterTexKey, value); }
            }

            public Vector2 waterTexOffset
            {
                get { return GetTextureOffset (Properties.waterTexKey); }
                set { SetTextureOffset (Properties.waterTexKey, value); }
            }

            // Tex1, default = "white" { }
            public Texture2D waterTex1
            {
                get { return GetTexture (Properties.Instance.waterTex1ID) as Texture2D; }
                set { SetTexture (Properties.Instance.waterTex1ID, value); }
            }

            public Vector2 waterTex1Scale
            {
                get { return GetTextureScale (Properties.waterTex1Key); }
                set { SetTextureScale (Properties.waterTex1Key, value); }
            }

            public Vector2 waterTex1Offset
            {
                get { return GetTextureOffset (Properties.waterTex1Key); }
                set { SetTextureOffset (Properties.waterTex1Key, value); }
            }

            // Normal Tiling, default = 1
            public Single bTiling
            {
                get { return GetFloat (Properties.Instance.bTilingID); }
                set { SetFloat (Properties.Instance.bTilingID, value); }
            }

            // Normalmap0, default = "bump" { }
            public Texture2D bumpMap
            {
                get { return GetTexture (Properties.Instance.bumpMapID) as Texture2D; }
                set { SetTexture (Properties.Instance.bumpMapID, value); }
            }

            public Vector2 bumpMapScale
            {
                get { return GetTextureScale (Properties.bumpMapKey); }
                set { SetTextureScale (Properties.bumpMapKey, value); }
            }

            public Vector2 bumpMapOffset
            {
                get { return GetTextureOffset (Properties.bumpMapKey); }
                set { SetTextureOffset (Properties.bumpMapKey, value); }
            }

            // Water Movement, default = 1
            public Single displacement
            {
                get { return GetFloat (Properties.Instance.displacementID); }
                set { SetFloat (Properties.Instance.displacementID, value); }
            }

            // Texture Displacement, default = 1
            public Single texDisplacement
            {
                get { return GetFloat (Properties.Instance.texDisplacementID); }
                set { SetFloat (Properties.Instance.texDisplacementID, value); }
            }

            // Water Freq, default = 1
            public Single dispFreq
            {
                get { return GetFloat (Properties.Instance.dispFreqID); }
                set { SetFloat (Properties.Instance.dispFreqID, value); }
            }

            // Mix, default = 1
            public Single mix
            {
                get { return GetFloat (Properties.Instance.mixID); }
                set { SetFloat (Properties.Instance.mixID, value); }
            }

            // Opacity, default = 1
            public Single oceanOpacity
            {
                get { return GetFloat (Properties.Instance.oceanOpacityID); }
                set { SetFloat (Properties.Instance.oceanOpacityID, value); }
            }

            // Falloff Power, default = 1
            public Single falloffPower
            {
                get { return GetFloat (Properties.Instance.falloffPowerID); }
                set { SetFloat (Properties.Instance.falloffPowerID, value); }
            }

            // Falloff Exp, default = 2
            public Single falloffExp
            {
                get { return GetFloat (Properties.Instance.falloffExpID); }
                set { SetFloat (Properties.Instance.falloffExpID, value); }
            }

            // AP Fog Color, default = (0,0,1,1)
            public Color fogColor
            {
                get { return GetColor (Properties.Instance.fogColorID); }
                set { SetColor (Properties.Instance.fogColorID, value); }
            }

            // AP Height Fall Off, default = 1
            public Single heightFallOff
            {
                get { return GetFloat (Properties.Instance.heightFallOffID); }
                set { SetFloat (Properties.Instance.heightFallOffID, value); }
            }

            // AP Global Density, default = 1
            public Single globalDensity
            {
                get { return GetFloat (Properties.Instance.globalDensityID); }
                set { SetFloat (Properties.Instance.globalDensityID, value); }
            }

            // AP Atmosphere Depth, default = 1
            public Single atmosphereDepth
            {
                get { return GetFloat (Properties.Instance.atmosphereDepthID); }
                set { SetFloat (Properties.Instance.atmosphereDepthID, value); }
            }

            // FogColorRamp, default = "white" { }
            public Texture2D fogColorRamp
            {
                get { return GetTexture (Properties.Instance.fogColorRampID) as Texture2D; }
                set { SetTexture (Properties.Instance.fogColorRampID, value); }
            }

            public Vector2 fogColorRampScale
            {
                get { return GetTextureScale (Properties.fogColorRampKey); }
                set { SetTextureScale (Properties.fogColorRampKey, value); }
            }

            public Vector2 fogColorRampOffset
            {
                get { return GetTextureOffset (Properties.fogColorRampKey); }
                set { SetTextureOffset (Properties.fogColorRampKey, value); }
            }

            // FadeStart, default = 1
            public Single fadeStart
            {
                get { return GetFloat (Properties.Instance.fadeStartID); }
                set { SetFloat (Properties.Instance.fadeStartID, value); }
            }

            // FadeEnd, default = 1
            public Single fadeEnd
            {
                get { return GetFloat (Properties.Instance.fadeEndID); }
                set { SetFloat (Properties.Instance.fadeEndID, value); }
            }

            // PlanetOpacity, default = 1
            public Single planetOpacity
            {
                get { return GetFloat (Properties.Instance.planetOpacityID); }
                set { SetFloat (Properties.Instance.planetOpacityID, value); }
            }

            // NormalXYFudge, default = 0.1
            public Single normalXYFudge
            {
                get { return GetFloat (Properties.Instance.normalXYFudgeID); }
                set { SetFloat (Properties.Instance.normalXYFudgeID, value); }
            }

            // NormalZFudge, default = 1.1
            public Single normalZFudge
            {
                get { return GetFloat (Properties.Instance.normalZFudgeID); }
                set { SetFloat (Properties.Instance.normalZFudgeID, value); }
            }

            public PQSOceanSurfaceQuad() : base(Properties.shader)
            {
            }

            [Obsolete("Creating materials from shader source String is no longer supported. Use Shader assets instead.")]
            public PQSOceanSurfaceQuad(String contents) : base(contents)
            {
                base.shader = Properties.shader;
            }

            public PQSOceanSurfaceQuad(Material material) : base(material)
            {
                // Throw exception if this material was not the proper material
                if (material.shader.name != Properties.shader.name)
                    throw new InvalidOperationException("Type Mismatch: Terrain/PQS/Ocean Surface Quad shader required");
            }

        }
    }
}
