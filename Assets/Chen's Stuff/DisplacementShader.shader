Shader "Custom/FixedDisplacementShader" {
    Properties {
        _MainTex ("Base Texture", 2D) = "white" {}
        _HeightMap ("Height Map", 2D) = "black" {}
        _Displacement ("Displacement Strength", Range(0, 1)) = 0.1
        _Tiling ("Tiling", Vector) = (1, 1, 0, 0)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _HeightMap;
            float _Displacement;
            float4 _Tiling;

            struct appdata_t {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata_t v) {
                v2f o;
                // Simplify height sampling
                float height = tex2Dlod(_HeightMap, float4(v.uv * _Tiling.xy, 0, 0)).r;
                // Apply displacement
                v.vertex.xyz += v.normal * height * _Displacement;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Sample the base texture
                return tex2D(_MainTex, i.uv * _Tiling.xy);
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
