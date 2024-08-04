Shader "Custom/SceneTransitions"
{
    Properties
    {
        [PerRendererData]_MainTex ("Sprite Texture", 2D) = "transparent" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _MousePos ("Mouse Position and Radius", Vector) = (0,0,0,0)
    }

    SubShader
    {
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment SampleSpriteTexture
            #pragma target 2.0
            #include "UnitySprites.cginc"

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 _MousePos; // 마우스 포지션과 반지름

            fixed4 SampleSpriteTexture(v2f i) : SV_Target
            {
                float2 uv = i.texcoord;

                // 마우스 포지션과 반지름
                float2 mousePos = _MousePos.xy;
                float radius = _MousePos.z;

                // 화면 비율 계산
                float screenRatio = _ScreenParams.x / _ScreenParams.y;

                // UV 좌표를 화면 비율에 맞게 조정
                float2 adjustedMousePos = mousePos * float2(screenRatio, 1.0);
                float2 adjustedUV = uv * float2(screenRatio, 1.0);

                // 현재 픽셀과 마우스 포지션 간의 거리 계산
                float distance = length(adjustedUV - adjustedMousePos);

                // 원 안에 있는지 확인
                if (distance < radius)
                {
                    // 원 안에서는 특정 색상으로 변환
                    return tex2D(_MainTex, uv);
                }

                // 원 바깥에서는 기본 텍스처 색상
                return _Color;
            }
        ENDCG
        }
    }
}