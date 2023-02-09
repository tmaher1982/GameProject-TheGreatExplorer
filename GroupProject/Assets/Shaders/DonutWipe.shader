Shader "Unlit/DonutWipe"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,0)
        _WipeAmount("Wipe Amount",  Range(0.0, 2.0)) = 0.0
        _Sharpness("Sharpness",  Range(0.0, 1.0)) = 0.01
        [Toggle] _Reverse("Reverse", Float) = 0
    }
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        // LOD 100
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 

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

            float4 _Color;
            float _WipeAmount;
            float _Reverse;
            float _Sharpness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Get the aspect of the screen
                float res = _ScreenParams.x/_ScreenParams.y;
               
                // UVs passed from Vertex Shader
                float2 uv = i.uv ;

                // Shift UVs from corners to center (of a Unity plane)
                uv -= 0.5;
               
                // Scale UV.x by screen aspect (Screen will hopefully be wider that it is high)
                uv.x *=res;

                // Finds the distance from current pixel/fragment to the 'center'
                float c = distance(uv,float2(0,0));
               
                // Control the blend 
                float blend = smoothstep(_WipeAmount, _WipeAmount-_Sharpness, c );
               
                // Invert the blend 
                if(_Reverse == 1)
                    blend = 1-blend;

                return float4(_Color.rgb, blend);
            }
            ENDCG
        }
    }
}
