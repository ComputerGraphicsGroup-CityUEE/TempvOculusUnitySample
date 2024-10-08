﻿// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Hidden/UVFlip" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader{
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = float2(v.uv.x, 1.0 - v.uv.y);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    return tex2D(_MainTex, i.uv);
                }
                ENDCG
            }
    }
}