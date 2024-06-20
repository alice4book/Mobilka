Shader "Unlit/Gradient"
{
    Properties
    {
        _LeftColour("Left Gradient Colour: ", Color) = (1,1,1,1)
        _RightColour("Right Gradient Colour: ", Color) = (0,0,0,1)
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
                float4 colour : TEXCOORD0;

                float4 vertex : SV_POSITION;
            };

            fixed4 _LeftColour;
            fixed4 _RightColour;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.colour = lerp(_LeftColour, _RightColour, v.uv.x);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.colour;
            }
            ENDCG
        }
    }
}
