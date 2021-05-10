﻿Shader "DynamicFog/Opaque/Standard Shader BumpMap Occlusion Map" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_OcclusionMap ("Occlusion Map (G)", 2D) = "white" {}
		_BumpMap ("BumpMap", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard finalcolor:fogColor exclude_path:deferred exclude_path:prepass fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex, _BumpMap, _OcclusionMap;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_OcclusionMap;
			float3 worldPos;
		};

		#define SURFACE_STRUCT SurfaceOutputStandard
		#include "DFMSurfaceShaderCommon.cginc"

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
	 		o.Albedo = c.rgb;
	 		o.Occlusion = tex2D (_OcclusionMap, IN.uv_OcclusionMap).g;
			o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uv_BumpMap));
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
