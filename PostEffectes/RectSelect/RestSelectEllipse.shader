Shader "Hidden/RestSelectEllipse"
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
            #include "Assets\libres\bibliotequeShader.cginc"

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
            float2 pointA;
            float2 pointB;

            fixed4 frag(v2f i) : SV_Target
                {
                float2 screen = float2(1.0f / _ScreenParams.x, 1.0f / _ScreenParams.y);
                float2 ramka = screen * 3;
                float2 minPoint, maxPoint;
                minPoint.x = min(pointA.x, pointB.x);
                minPoint.y = min(pointA.y, pointB.y);
                maxPoint.x = max(pointA.x, pointB.x);
                maxPoint.y = max(pointA.y, pointB.y);
                
                bool condOut = estPointInRect(i.uv, minPoint, maxPoint);
                bool condIn  = estPointInRect(i.uv, minPoint + ramka, maxPoint - ramka );

                float condF = condOut? 1.0f: 0.0f;
                if(condIn)
                    {
                    condF = 0.2f;
                    }

                float2 uv = i.uv;
                uv.y = 1 - uv.y;

                float4 col = tex2D(_MainTex, uv);  
                float4 res = lerp(col, float4(0, 0, 1, 1), condF);
                return res;
                }
            ENDCG
            }
        }
    }

