Shader "Hidden/EncharcherColor"
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
			float _size = 1.0f;
			float4 _dir;

            fixed4 frag (v2f i) : SV_Target
                {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 colNiveau;
				float estDraw = 0.0f;
				float2 uv = i.uv;
				
				for (int i = -4; i <= 4; i++)
                    {
					colNiveau = tex2D(_MainTex, uv + float2(0.0001f * _size, 0.0001f * _size) * i * _dir.xy * float2(_ScreenParams.y/ _ScreenParams.x, 1) ); 
					if(colNiveau.a > 0.1f)
						{
						estDraw = 1.0f;
						}
						
					}
					
					
                // just invert the colors
                col = lerp(col, fixed4(1, 1, 1, 1), estDraw);
                return col;
                }
            ENDCG
            }
        }
    }
