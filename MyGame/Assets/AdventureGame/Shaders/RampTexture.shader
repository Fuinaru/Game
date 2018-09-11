// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unity Shaders Book/Chapter7/RampTexture"{
	Properties{
	_Color("Color",Color) = (1,1,1,1)
	_Gloss("Gloss",Range(8.0,256)) = 20
	_Specular("Specular", Color) = (1, 1, 1, 1)
	_RampTex("RampTex", 2D) = "white"{}
	_MainTex("Main", 2D) = "white"{}
	_SpecularMask("SpecularMask",2D) = "white"{}
	_SpecularScale("SpecularScale",Float)=1.0
	}
	SubShader{
		Pass{
Tags{"LightMode" = "ForwardBase"}
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "Lighting.cginc"

fixed4 _Color;
float _Gloss;
fixed4 _Specular;
sampler2D _RampTex;
float4 _RampTex_ST;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _SpecularMask;
float _SpecularScale;


struct a2v {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float4 texcoord : TEXCOORD0;
};
struct v2f {
	float4 pos : SV_POSITION;
	float3 worldNormal:TEXCOORD0;
	float4 worldPos:TEXCOORD1;
	float2 uv:TEXCOORD2;
	float4 worldTangent:TEXCOORD3;
	float4 worldBinormal:TEXCOORD4;

};

v2f vert(a2v v) {
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.worldNormal = UnityObjectToWorldNormal(v.normal);
	o.worldPos = mul(unity_ObjectToWorld, v.vertex);
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

	return o;

}

fixed4 frag(v2f i) :SV_Target{
fixed3 worldNormal = normalize(i.worldNormal);
fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
fixed halfLambert = 0.5*dot(worldNormal, worldLightDir) + 0.5;

fixed3 albedo = tex2D(_MainTex, i.uv)*_Color.rgb;
fixed specularMask = tex2D(_SpecularMask, i.uv).r*_SpecularScale;

fixed3 diffuseColor = tex2D(_RampTex, fixed2(halfLambert, halfLambert))*albedo;
fixed3 diffuse = diffuseColor * _LightColor0.rgb;


fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;

fixed3 viewDir = UnityWorldSpaceViewDir(i.worldPos);
fixed3 halfDir = normalize(viewDir + worldLightDir);



fixed3 specular = _LightColor0.rgb*_Specular.rgb*pow(max(0, dot(worldNormal, halfDir)), _Gloss)*specularMask;

return fixed4(ambient + specular + diffuse, 1.0);



}

ENDCG
	
	}

	}
		FallBack "Specular"
}