Shader "heightTest"
{
	Properties
	{
		_MainColor("Main Color", Color) = (0.112362,0.8871241,0.9528302,0)
		_Color0("Color 0", Color) = (0.9056604,0.1153435,0.7765018,0)
		_Color1("Color1", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
		_Color3("Color3", Color) = (1,1,1,1)
		_Color4("Color4", Color) = (1,1,1,1)
	   [HideInInspector] __dirty("", Int) = 1
		_Float("maxDepthInLevel", Float) = 0
	}

		SubShader
	{
	   Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
	   Cull Back
	   CGPROGRAM
	   #pragma target 3.0
	   #pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
	   struct Input
	   {
		  float3 worldPos;
	   };

	   uniform float4 _MainColor;
	   uniform float4 _Color0;
	   uniform float4 _Color1;
	   uniform float4 _Color2;
	   uniform float4 _Color3;
	   uniform float4 _Color4;
	   
	   uniform float4 Colors[6];

	   float currentDepth;

	   float maxDepthInLevel;

	   float increment;

	   float colorNumber = 0.0f;

	   int cf;

	   int cc;


	   inline half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
	   {
		  return half4 (0, 0, 0, s.Alpha);
	   }

	   void surf(Input i , inout SurfaceOutput o)
	   {
		   Colors[0] = _MainColor;
		   Colors[1] = _Color0;
		   Colors[2] = _Color1;
		   Colors[3] = _Color2;
		   Colors[4] = _Color3;
		   Colors[5] = _Color4;

		   //maxDepthInLevel = GroundManager.MaxDepth;

		   increment = 6; //set increment to num of colors

		   //currentDepth = -position.y; //get current depth and flip sign so we can do positive math

		   float3 ase_worldPos = i.worldPos;

		   currentDepth = -ase_worldPos.y;
		   //float4 lerpResult9 = lerp(_MainColor , _Color0 , (max(ase_worldPos.y , 0.0) / 10.0));

		   colorNumber = (increment - 1) * (currentDepth / maxDepthInLevel); //constrain depth to # of colors

		   cf = floor(colorNumber); //color before where we are in the list

		   cc = ceil(colorNumber); //color after where we are in the list
		   
		   
		   
		  

		  if ((cf > -1 && cf < increment) && (cc > -1 && cc < increment))
		  {

			  //col = lerp(Colors[cf], Colors[cc], (colorNumber % 1)); //calculate color between the 2 in list

			  float4 lerpResult9 = lerp(Colors[cf], Colors[cc], (colorNumber % 1)); //calculate color between the 2 in list

			  o.Emission = lerpResult9.rgb;
			  o.Alpha = 1;
		  }
		 
	   }

	   ENDCG
	}
		Fallback "Diffuse"
		   CustomEditor "ASEMaterialInspector"
}
