Shader "Hidden/MixTexture"
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
            sampler2D _Mask;
            sampler2D _Accum;
            float4    _Color;

            fixed4 frag (v2f i) : SV_Target
                {
                fixed4 col   = tex2D(_MainTex, i.uv) * _Color;
                fixed4 mask  = tex2D(_Mask,    i.uv);
                fixed4 accum = tex2D(_Accum,   i.uv);

                if (mask.a > 0.1f)
                    {
                    col = fixed4(0, 0, 0, 0);
                    }

                if (accum.a < 0.5f)
                    {
                    //col = col + accum;
                    }
                col = col + accum;
                return col;
                }
            ENDCG
            }
        }
    }
