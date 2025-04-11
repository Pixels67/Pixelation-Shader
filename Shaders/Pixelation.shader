Shader "Hidden/Pixelation" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            
            sampler2D palette_tex;
            float palette_size;
            
            float2 block_count;
            float2 block_size;

            fixed4 frag(v2f_img i) : SV_Target {
                float2 block_pos = floor(i.uv * block_count) * block_size;
                float4 tex = tex2D(_MainTex, block_pos);

                float min_distance = 99999.0;
                float4 closest_color = float4(0.0, 0.0, 0.0, 1.0);
                
                for (int j = 0; j < palette_size; j++) {
                    float2 uv = float2((j + 0.5) / palette_size, 0.5);
                    float4 palette_color = tex2D(palette_tex, uv);
                    float dist = distance(tex.rgb, palette_color.rgb);
                    
                    if (dist < min_distance) {
                        min_distance = dist;
                        closest_color = palette_color;
                    }
                }

                return closest_color;
            }
            ENDCG
        }
    }
}