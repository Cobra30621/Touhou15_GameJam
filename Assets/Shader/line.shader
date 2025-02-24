Shader "Custom/DashedLine"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _DashSize ("Dash Size", Float) = 5.0
        _SpaceSize ("Space Size", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _DashSize;
            float _SpaceSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // 計算當前片段在虛線上的位置
                float linePos = i.uv.x * _MainTex_ST.x * _DashSize;
                float dashIndex = floor(linePos / (_DashSize + _SpaceSize));
                float dashOffset = fmod(linePos, (_DashSize + _SpaceSize));

                // 判斷當前片段是否在線段上
                if (dashOffset > _DashSize)
                {
                    discard; // 處於間隔部分，不渲染
                }

                // 渲染線段部分
                return _Color;
            }
            ENDCG
        }
    }
}
