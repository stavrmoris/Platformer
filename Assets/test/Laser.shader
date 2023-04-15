﻿Shader "Particles/Laser" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
		_Speed("Speed", Float) = 1.0
	}

		Category{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
			Blend SrcAlpha One
			ColorMask RGB
			Cull Off Lighting Off ZWrite Off

			SubShader {
				Pass {

					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma target 2.0
					#pragma multi_compile_particles
					#pragma multi_compile_fog

					#include "UnityCG.cginc"

					sampler2D _MainTex;
					fixed4 _TintColor;

					struct appdata_t {
						float4 vertex : POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					struct v2f {
						float4 vertex : SV_POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						UNITY_FOG_COORDS(1)
						#ifdef SOFTPARTICLES_ON
						float4 projPos : TEXCOORD2;
						#endif
						UNITY_VERTEX_OUTPUT_STEREO
					};

					float4 _MainTex_ST;

					v2f vert(appdata_t v)
					{
						v2f o;
						UNITY_SETUP_INSTANCE_ID(v);
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
						o.vertex = UnityObjectToClipPos(v.vertex);
						#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos(o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
						#endif
						o.color = v.color;
						o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
						UNITY_TRANSFER_FOG(o,o.vertex);
						return o;
					}

					UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
					float _Speed;

					fixed4 frag(v2f i) : SV_Target
					{
						fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord + float2(- _Speed * _Time.x, 0));
						return col;
					}
					ENDCG
				}
			}
		}
}
