Shader "Unlit/TransparentGradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TopColour ("Top Gradient Colour", Color) = (1,1,1,1)
        _BottomColour ("Bottom Gradient Colour", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
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
                float4 gradientColor : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _TopColour;
            fixed4 _BottomColour;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.gradientColor = lerp(_BottomColour, _TopColour, v.uv.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 finalColor = texColor * i.gradientColor;
                return finalColor;
            }
            ENDCG
        }
    }
}