// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible colored shader.
// - no lighting
// - no lightmap support
// - no texture

Shader "Unlit/TEST" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _Color1 ("Color1", Color) = (1,1,1,1)
    _Color2 ("Color2", Color) = (1,1,1,1)
    _Color3 ("Color3", Color) = (1,1,1,1)
    _Color4 ("Color4", Color) = (1,1,1,1)
    _Color5 ("Color5", Color) = (1,1,1,1)

}

SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(0)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            //VARIABLES
            fixed4 _Color;
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            fixed4 _Color4;
            fixed4 _Color5;

            fixed4 Colors[6];

            float currentDepth;

            float maxDepthInLevel;

            float increment;

            float colorNumber = 0.0f;

            int cf;

            int cc;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : COLOR
            {
                Colors[0] = _Color;
                Colors[1] = _Color1;
                Colors[2] = _Color2;
                Colors[3] = _Color3;
                Colors[4] = _Color4;
                Colors[5] = _Color5;

                //maxDepthInLevel = GroundManager.MaxDepth;

                increment = 6; //set increment to num of colors
                

                

                


                fixed4 col = _Color * _Color1;
                UNITY_APPLY_FOG(i.fogCoord, col);
                UNITY_OPAQUE_ALPHA(col.a);
                return col;
            }
        ENDCG
    }
}

}