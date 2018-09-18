﻿Shader "Unlit/WaterfallShader"
{
	Properties
	{
		_NoiseTex("Noise texture", 2D) = "white" {}
	_DisplGuide("Displacement guide", 2D) = "white" {}
	_DisplAmount("Displacement amount", float) = 0
		_AlphaFix("AlphaFix", float) = 1
		[HDR]_ColorBottomDark("Color bottom dark", color) = (1,1,1,1)
		[HDR]_ColorTopDark("Color top dark", color) = (1,1,1,1)
		[HDR]_ColorBottomLight("Color bottom light", color) = (1,1,1,1)
		[HDR]_ColorTopLight("Color top light", color) = (1,1,1,1)
		_BottomFromThreshold("Bottom from threshold", Range(0,1)) = 0.1
		_Speed("Speed",Float) = 1.0
		_DisplSpeed("DisplSpeed",Float) = 1.0
	}
		SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		float3 normal:NORMAL;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 noiseUV : TEXCOORD1;
		float2 displUV : TEXCOORD2;
		float3 worldNormal : TEXCOORD3;
		float3 worldPos : TEXCOORD4;
		UNITY_FOG_COORDS(5)
	};

	sampler2D _NoiseTex;
	float4 _NoiseTex_ST;
	sampler2D _DisplGuide;
	float4 _DisplGuide_ST;
	fixed4 _ColorBottomDark;
	fixed4 _ColorTopDark;
	fixed4 _ColorBottomLight;
	fixed4 _ColorTopLight;
	half _DisplAmount;
	half _BottomFromThreshold;
	float _Speed;
	float _DisplSpeed;
	float _AlphaFix;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.noiseUV = TRANSFORM_TEX(v.uv, _NoiseTex);
		o.displUV = TRANSFORM_TEX(v.uv, _DisplGuide);
		o.uv = v.uv;

		o.worldNormal = UnityObjectToWorldNormal(v.normal);
		o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed3 worldNormal = normalize(i.worldNormal);
	//fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
	fixed3 worldViewDir = normalize(fixed3(0,0,-1));
	fixed alpha = abs( dot(worldNormal, worldViewDir));
	alpha = lerp(-5, 1, alpha*_AlphaFix);
		//Displacement
		half2 displ = tex2D(_DisplGuide, i.displUV + _DisplSpeed*_Time.y / 5).xy;
		displ = ((displ * 2) - 1) * _DisplAmount;

		//Noise
		half noise = tex2D(_NoiseTex, float2(i.noiseUV.x, i.noiseUV.y + _Speed*_Time.y / 5) + displ).x;
		noise = round(noise * 5.0) / 5.0;

		fixed4 col = lerp(lerp(_ColorBottomDark, _ColorTopDark, i.uv.y), lerp(_ColorBottomLight, _ColorTopLight, i.uv.y), noise);
		col = lerp(fixed4(1,1,1,1), col, step(_BottomFromThreshold, i.uv.y + displ.y));
		col= fixed4(col.xyz,alpha)
		UNITY_APPLY_FOG(i.fogCoord, col);
		return col;
	}
		ENDCG
	}
	}
		Fallback "VertexLit"
}