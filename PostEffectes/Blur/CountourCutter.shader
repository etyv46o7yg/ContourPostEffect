Shader "Hidden/CountourCutter"
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
            sampler2D _Original;

            fixed4 frag (v2f i) : SV_Target
                {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 baseCol = tex2D(_Original, i.uv);
                
                fixed4 res = fixed4(0, 0, 0, 0);
                if (col.a > 0.1f && baseCol.a < 0.1f)
                    {
                    res = fixed4(0, 1, 0, 1);
                    }

                return res;
                }
            ENDCG
            }
        }
    }
