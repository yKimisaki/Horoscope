Shader "Custom/ParticleShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { 
			"Queue" = "Background"
		}
		LOD 200
		
        Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

            #include "UnityCG.cginc"

			float4 vert(float4 v:POSITION) : SV_POSITION {
				return UnityObjectToClipPos(v);
			}

			fixed4 _Color;

			fixed4 frag() : COLOR {
				return fixed4(1,1,1,1);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
