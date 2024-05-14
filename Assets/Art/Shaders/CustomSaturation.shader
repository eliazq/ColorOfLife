Shader "CustomRenderTexture/CustomSaturation"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Emission ("Emission", Color) = (0,0,0,0)
        _Saturation ("Saturation", Range(0, 2)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        
        struct Input
        {
            float2 uv_MainTex;
        };
        
        sampler2D _MainTex;
        fixed4 _Color;
        float _Saturation;
        fixed4 _Emission;
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            float3 color = texColor.rgb * _Color.rgb;
            float gray = dot(color, float3(0.299, 0.587, 0.114));
            float3 finalColor = lerp(gray, color, _Saturation);
            o.Albedo = finalColor;
            o.Alpha = texColor.a * _Color.a;
            o.Emission = _Emission.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}