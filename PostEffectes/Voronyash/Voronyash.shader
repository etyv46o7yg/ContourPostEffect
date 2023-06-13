Shader "Hidden/Voronyash"
    {
    Properties
        {
        _MainTex ("Texture", 2D) = "white" {}
        _CellSize("Cell Size", Range(0, 0.5)) = 0.2
        _BorderColor("Border Color", Color) = (0,0,0,1)
        _k_1("K 1", Range(0, 2)) = 1
        _k_2("K 2", Range(0, 2)) = 1
        SIZE("Size", Range(0, 500)) = 100
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
            #include "Assets/libres/Random.cginc"

            #define K 0.142857142857
            //3/7
            #define Ko 0.428571428571
            float SIZE;
            float _CellSize, _k_1, _k_2;
            float4 _BorderColor;
            sampler2D _MainTex;                                       

            #define col1 float3(193.,41.,46.)/255.
            #define col2 float3(241.,211.,2.)/255.
            #define t _Time.z

            float2 ran(float2 uv) 
                {
                uv *= float2(dot(uv, float2(127.1, 311.7)), dot(uv, float2(227.1, 521.7)));
                return 1.0 - frac(tan(cos(uv) * 123.6) * 3533.3) * frac(tan(cos(uv) * 123.6) * 3533.3);
                }
            float2 pt(float2 id) 
                {
                return sin(t * (ran(id + .5) - 0.5) + ran(id - 20.1) * 8.0) * 0.5;
                }


            float4 mainImage(float2 fragCoord)
                {
                float2 uv = fragCoord;//(fragCoord - .5 * _ScreenParams.xy) / _ScreenParams.x;
                float2 off = float2(0, 0);// _Time.xy / float2(3.5f, 4.5f);
                uv += off;
                uv *= SIZE;

                float2 gv = frac(uv) - .5;
                float2 id = floor(uv);

                float mindist = 1e9;
                float2 vorv;
                for (float i = -1.; i <= 1.; i++) {
                    for (float j = -1.; j <= 1.; j++) {
                        float2 offv = float2(i, j);
                        float dist = length(gv + pt(id + offv) - offv);
                        if (dist < mindist) {
                            mindist = dist;
                            vorv = (id + pt(id + offv) + offv) / SIZE - off;
                        }
                    }
                }

                float3 col = lerp(float3(1, 0, 0), float3(0, 1, 0), clamp(vorv.x * 2.2 + vorv.y, -1., 1.) * 0.5 + 0.5);


                /*
                fragColor += float4(float3(smoothstep(0.08,0.05,gv.x+pt(id).x)),0.0);
                fragColor -= float4(float3(smoothstep(0.05,0.03,gv.x+pt(id).x)),0.0);
                */
                return float4(vorv.xy, 0, 1.0);
                }



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

            

            fixed4 frag (v2f i) : SV_Target
                {
                float2 value = i.uv / _CellSize;

                float valueChange = length(fwidth(value)) * 0.5;
                float isBorder = 1 - smoothstep(0.05 - valueChange, 0.05 + valueChange, 0.5f);
                //float3 color = lerp(cellColor, _BorderColor, isBorder);
                //float x = 0, y = 0;
                float4 res = mainImage(i.uv);               
                return res;
                
                return tex2D(_MainTex, res.xy);
                }
            ENDCG
            }
        }
    }
