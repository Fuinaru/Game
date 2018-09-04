// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unity Shaders Book/Chapter14/ToonShadingMat" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex",2D) = "white"{}
	_Ramp("Ramp",2D) = "white"{}
	_Outline("Outline",Range(0,1)) = 0.1
		_Specular("Specular",Color) = (1,1,1,1)
		_OutlineColor("OutLineColor",Color) = (0,0,0,1)
		_SpecularScale("SpecularScale",Range(0,1)) = 0.1
	}



		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }


		Pass{
		NAME "OUTLINE"
		Cull Front
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		float _Outline;
	fixed4 _OutlineColor;
	struct a2v {
		float4 vertex:POSITION;
		float3 normal:NORMAL;
	};
	struct v2f {
		float4 pos:SV_POSITION;
	};
	v2f vert(a2v v) {
		v2f o;
		float4 pos = mul(UNITY_MATRIX_MV, v.vertex);
		float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		normal.z = -4;
		pos = pos + float4(normalize(normal), 0)*_Outline;
		o.pos = mul(UNITY_MATRIX_P, pos);
		return o;

	}
	fixed4 frag(v2f i) :SV_Target{
		return float4(_OutlineColor.rgb,1);
	}


		ENDCG
	}

		Pass{
	Tags{"LightMode"="ForwardBase"}
	Cull Back
	CGPROGRAM
	
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
#include "UnityShaderVariables.cginc"

		fixed4 _Color;
	sampler2D _MainTex;
	float4 _MainTex_ST;
	sampler2D _Ramp;
	fixed4 _Specular;
	fixed _SpecularScale;

		struct a2v {
	float4 vertex:POSITION;
	float3 normal:NORMAL;
	float4 texcoord:TEXCOORD0;
	float4 tangent:TANGENT;
	
	};
	    struct v2f {
			float4 pos:SV_POSITION;
			float2 uv : TEXCOORD0;
			float3 worldNormal : TEXCOORD1;
			float3 worldPos : TEXCOORD2;
			SHADOW_COORDS(3)
		
		};

		v2f vert(a2v v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.worldNormal = UnityObjectToWorldNormal(v.normal);
			o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

			TRANSFER_SHADOW(o);
			return o;
		
		}

		fixed4 frag(v2f i) :SV_Target{
		fixed3 worldNormal = normalize(i.worldNormal);
		fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
		fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
		fixed3 worldHalfDir = normalize(worldViewDir + worldLightDir);

		UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

		fixed3 albedo = _Color.rgb*tex2D(_MainTex,i.uv).rgb;
		fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
		fixed diff = (dot(worldNormal,worldLightDir)*0.5+0.5)*atten;
		fixed3 diffuse = albedo*_LightColor0.rgb*tex2D(_Ramp, float2(diff, diff)).rgb;

		fixed spec = dot(worldNormal, worldHalfDir);
		fixed w = fwidth(spec)*2;
		fixed3 specular = _Specular.rgb*lerp(0, 1, smoothstep(-w, w, spec - 1 + _SpecularScale))*step(0.0001, _SpecularScale);
		return fixed4(ambient+specular+diffuse,1.0);


		
		
		}

	ENDCG

	}


	}
		FallBack "Diffuse"
}
