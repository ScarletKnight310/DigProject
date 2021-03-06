﻿// Made with Amplify Shader Editor

// Available at the Unity Asset Store - http://u3d.as/y3X 

Shader "StoneShader"

{

	Properties

	{

		_MainColor("Main Color", Color) = (0.112362,0.8871241,0.9528302,0)

		_Color0("Color 0", Color) = (0.9056604,0.1153435,0.7765018,0)

		_Color1("Color1", Color) = (1,1,1,1)

		_Color2("Color2", Color) = (1,1,1,1)

		_Color3("Color3", Color) = (1,1,1,1)

		_Color4("Color4", Color) = (1,1,1,1)

		_Texture("Texture", 2D) = "white" {}

		[HideInInspector] _texcoord("", 2D) = "white" {}

		[HideInInspector] __dirty("", Int) = 1

		_maxDepth("maxDepthInLevel", Float) = 20

		_verticalBandOffset("verticalBandOffset", Float) = 0

		_textureScale("textureScale", Float) = 1

		_brightness("brightness", Float) = 1

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

		float2 uv_texcoord;

	};



	uniform float4 _MainColor;

	uniform float4 _Color0;

	uniform float4 _Color1;

	uniform float4 _Color2;

	uniform float4 _Color3;

	uniform float4 _Color4;



	uniform sampler2D _Texture;

	uniform float4 _Texture_ST;



	uniform float _brightness;

	uniform float _maxDepth;

	uniform float _verticalBandOffset;

	uniform float _textureScale;

	float currentDepth;

	float increment;

	float colorNumber = 0.0f;

	int cf;

	int cc;

	uniform float4 Colors[6];

	inline half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)

	{

		return half4 (0, 0, 0, s.Alpha);

	}



	void surf(Input i, inout SurfaceOutput o)

	{

		Colors[0] = _MainColor;

		Colors[1] = _Color0;

		Colors[2] = _Color1;

		Colors[3] = _Color2;

		Colors[4] = _Color3;

		Colors[5] = _Color4;

		increment = 6; //set increment to num of colors

		float3 ase_worldPos = i.worldPos + _verticalBandOffset;

		currentDepth = -ase_worldPos.y;

		colorNumber = (increment - 1) * (currentDepth / (_maxDepth + _verticalBandOffset)); //constrain depth to # of colors

		cf = floor(colorNumber); //color before where we are in the list

		cc = ceil(colorNumber); //color after where we are in the list



		float brightness = _brightness * (1 - (ase_worldPos.z / 5));



		if (brightness < 0.2)

			brightness = 0.2;

		if (brightness > 1)

			brightness = 1;



		if (_textureScale == 0)

			_textureScale = 1.0;



		float2 uv_Texture = (i.uv_texcoord * (1.0 / _textureScale)) * _Texture_ST.xy + _Texture_ST.zw;





		if (cf <= -1)

		{

			o.Emission = (Colors[0] * tex2D(_Texture, uv_Texture)).rgb;

		}

		if (cc <= -1)

		{

			o.Emission = (Colors[5] * tex2D(_Texture, uv_Texture)).rgb;

		}



		if ((cf > -1 && cf < increment) && (cc > -1 && cc < increment))

		{

			float4 lerpResult9 = lerp(Colors[cf], Colors[cc], fmod(colorNumber, 1)); //calculate color between the 2 in list

			lerpResult9 *= brightness;

			o.Emission = (lerpResult9 * tex2D(_Texture, uv_Texture)).rgb;

		}



		o.Alpha = 1;

	}



	ENDCG

}

	Fallback "Diffuse"

		CustomEditor "ASEMaterialInspector"

}