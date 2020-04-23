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

	_Float ("maxDepthInLevel", Float) = 0
}

SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100

    Pass {
        CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members wpos)
			#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

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

            struct appdata_t {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };


			struct v2f{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldSpacePos : TEXCOORD1;
			}

            

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
				o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex).xyz;
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

				float4 position = i.worldSpacePos;
                
				currentDepth = -position.y; //get current depth and flip sign so we can do positive math

				colorNumber = (increment - 1) * (currentDepth / maxDepthInLevel); //constrain depth to # of colors

				cf = floor(colorNumber); //color before where we are in the list
				
				cc = ceil(colorNumber); //color after where we are in the list

				fixed4 col;
				
				if ((cf > -1 && cf < increment) && (cc > -1 && cc < increment))
				{

					col = lerp(Colors[cf], Colors[cc], (colorNumber % 1)); //calculate color between the 2 in list

					

					UNITY_APPLY_FOG(i.fogCoord, col);
					UNITY_OPAQUE_ALPHA(col.a);
					
				}
				
				return col;
				//fixed4 col = _Color * _Color1 * maxDepthInLevel / 1000;
				//col.g = maxDepthInLevel / 200;
				//UNITY_APPLY_FOG(i.fogCoord, col);
				//UNITY_OPAQUE_ALPHA(col.a);
               //return col;
            }
        ENDCG
    }
}

}