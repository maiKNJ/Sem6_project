Shader "Unlit/Ripples_2.0"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistanceFactor("DistanceFactoor", float) = 1
        _TimeFactor("Time factor", float) = 2
        _TotalFactor("total factor", float) = 3
        _WaveWidth("Wave with", float) = 4
        _CurWaveDis("Curwave dis", float) = 5
        _StartPos("Start pos", Vector) = (1,1,1,1)
        _MainTex_TexelSize("Maintex_texelsize", vector) = (1,1,1,1)
    }

        subshader{
            Pass {
        CGINCLUDE
        #include "UnityCG.cginc"
        uniform sampler2D _Maintex;
        float4 _MainTex_TexelSize;
        uniform float _DistanceFactor;
        uniform float _TimeFactor;
        uniform float _TotalFactor;
        uniform float _WaveWidth;
        uniform float _CurWaveDis;
        uniform float4 _StartPos;

        struct v2f
        {
            float2 uv : TEXCOORD0;
            //UNITY_FOG_COORDS(1)
            float4 vertex : SV_POSITION;
        };

        fixed4 frag(v2f i) : SV_Target
        {
            _StartPos.y = 1 - _StartPos.y;

            float2 dv = _StartPos.xy - i.uv;
            dv = dv * float2(i.vertex.x / i.vertex.y, 1);

            float dis = sqrt(dv.x * dv.x + dv.y * dv.y);
            float sinFactor = sin(dis * _DistanceFactor + _Time.y * _TimeFactor) * _TotalFactor * 0.01;
            float discardFactor = clamp(_WaveWidth - abs(_CurWaveDis - dis), 0, 1) / _WaveWidth;

            //Normalization
            float2 dv1 = normalize(dv);
            //cal uv offset fr each pixel
            float2 offset = dv1 * sinFactor * discardFactor;
            float2 uv = offset + i.uv;

            return tex2D(_Maintex, uv);
        }
        ENDCG
        }
        }

    /*SubShader
    {
        //Tags { "RenderType"="Opaque" }
        //LOD 100

        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off
            Fog{Mode off}

            CGPROGRAM
            //#pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            // make fog work
            /*#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }*/
    Fallback off
}
