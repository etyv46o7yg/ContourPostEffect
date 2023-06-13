Shader "Hidden/BlurGorVarShader"
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

            sampler2D _MainTex, _Blured, _Masque;

            fixed4 frag (v2f i) : SV_Target
                {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 blurCol = tex2D(_Blured, i.uv);
                fixed4 masqCol = tex2D(_Masque, i.uv);

                float koeff = masqCol.a;

                fixed4 res = lerp(col, masqCol, koeff);
                return res;
                }
            ENDCG
            }
        }
    }
