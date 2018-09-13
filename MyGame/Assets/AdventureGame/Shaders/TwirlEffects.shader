// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Effects/TwirlEffects"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
	uniform float4    _MainTex_TexelSize;
	half4   _MainTex_ST;

	//旋转扭曲的中心
	uniform float4 _CenterRadius;
	//将旋转矩阵传入
	uniform float4x4 _RotationMatrix;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		//将uv坐标变换到center坐标系中
		o.uv = v.uv - _CenterRadius.xy;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{

		float2 offest = i.uv;
		//利用旋转矩阵旋转uv
		float2 distortedOffset = MultiplyUV(_RotationMatrix,offest.xy);

		//计算uv点在旋转圆中的位置
		float2 tmp = offest / _CenterRadius.zw;
		float  t = min(1,length(tmp));

		//根据uv点在圆中的位置插值uv移动的位置
		offest = lerp(distortedOffset,offest,t);

		//将uv坐标返回原坐标系中
		offest += _CenterRadius.xy;

		fixed4 col = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(offest, _MainTex_ST));

		return col;
	}
		ENDCG
	}
	}
}