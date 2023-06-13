Shader "Hidden/TooniColor"
    {
    Properties
        {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
                {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
                }

            sampler2D _MainTex;
			float _koeff;

            fixed4 frag (v2f i) : SV_Target
                {
				float koeff = _koeff;
				
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 colMult = col.rgb * koeff;
				
                float r = round( colMult.r) / koeff;
				float g = round( colMult.g) / koeff;
				float b = round( colMult.b) / koeff;
                // just invert the colors
                col.rgb = float3(r, g, b);
                return col;
                }
            ENDCG
            }
        }
    }
