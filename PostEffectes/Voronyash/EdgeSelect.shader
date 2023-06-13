Shader "Hidden/EdgeSelect"
    {
    Properties
        {
        _MainTex ("Texture", 2D) = "white" {}
        _Carde("Texture", 2D) = "white" {}
        _BorderColor("Border Color", Color) = (0,0,0,1)
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
            sampler2D _Carde;
            float4  _BorderColor;
            float _SizeBorder = 1.0f;

            fixed4 frag (v2f i) : SV_Target
                {
                float4 origine = tex2D(_Carde, i.uv);               
                float4 colNiveau = origine;
   
                float4 colImage = tex2D(_MainTex, i.uv);
 
                float estBorder = 0.0f;
                float2 uv = i.uv;

                float4 colorResult = float4(0, 0, 0, 0);
                int count = 0;
                
                for (int i = -4; i <= 4; i++)
                    {
                    colNiveau = tex2D(_Carde, uv + float2(0.001f, 0) * i * _SizeBorder);
                    float delta = length(colNiveau - origine);
                    if (delta > 0.001f)
                        {
                        estBorder = 1.0f;
                        }
                    else
                        {
                        count++;
                        colorResult += tex2D(_MainTex, origine.xy + float2(0.001f, 0) * i);
                        }
                    }

                for (int i = -4; i <= 4; i++)
                    {
                    colNiveau = tex2D(_Carde, uv + float2(0, 0.001f) * i * _SizeBorder);
                    float delta = length(colNiveau - origine);
                    if (delta > 0.001f)
                        {
                        estBorder = 1.0f;
                        }
                    else
                        {
                        count++;
                        colorResult += tex2D(_MainTex, origine.xy + float2(0, 0.0001f) * i);
                        }
                    }
                
                return lerp(colorResult / count, _BorderColor, estBorder);
                }
            ENDCG
            }
        }
    }
