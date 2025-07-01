Shader "Custom/TVPuchun"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Progress("Progress", Range(0, 1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha
                Cull Off
                ZWrite Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc" // ← これが必要！

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Progress;

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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex); // ← これを使うなら include が必要
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv;

                    // Y方向に潰れる
                    float yCenter = 0.5;
                    float yDelta = abs(uv.y - yCenter);
                    uv.y = yCenter + yDelta * (1.0 - _Progress);

                    // X方向にも潰す（後半）
                    float xCenter = 0.5;
                    float xDelta = abs(uv.x - xCenter);
                    uv.x = xCenter + xDelta * lerp(1.0, 0.0, saturate(_Progress * 2 - 1.0));

                    // 画面外 → 透明
                    if (uv.y < 0 || uv.y > 1 || uv.x < 0 || uv.x > 1)
                        return float4(0,0,0,0);

                    return tex2D(_MainTex, uv);
                }
                ENDCG
            }
        }
}
