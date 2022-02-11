// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/PaintOnUV"
{
    Properties
    {
        _Radius("Radius of Painter", Float) = 1.0
        _Hardness("Hardness of Painter", Float) = 1.0
        _NormalClampAngle("Normal Clamp Angle", Float) = 90.0
        _TexPaint("Texture to Paint on", 2D) = "blue" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        ZWrite On
        ZTest Always
        Cull Off
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
                float3 normal: NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 posW: POSITION1;
                float2 uv : TEXCOORD0;
                float3 normalW: TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            float _Radius;
            float _Hardness;
            float _NormalClampAngle;
            
            float3 posWPainter;
            float3 dirWPainter;
            float paintStrength;
            float3 paintMask;

            sampler2D _TexPaint;
            float4 _TexPaint_ST;


            float mask(float3 position, float3 center, float radius, float hardness)
            {
                float m = distance(center, position);
                return 1-smoothstep(radius*hardness,radius,m);
            }

            float maskNormalClamped(float3 position, float3 center, float3 normal, float3 normalCenter, float radius, float hardness)
            {
                float m = distance(center, position);
                float mask = 1-smoothstep(radius*hardness,radius,m);
                return mask*step(cos(radians(_NormalClampAngle)), dot(normal, normalCenter));
                //return mask*step(cos(radians(90)), dot(normal, normalCenter));
                //return mask*dot(normal, normalCenter);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.posW = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = float4(0,0,0,1);
                o.vertex.xy = (v.uv*2.0f-1.0f)*float2(1, _ProjectionParams.x);
                o.normalW = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //float m = mask(i.posW, posWPainter, _Radius, _Hardness);
                float3 normalW = normalize(i.normalW);
                float m = maskNormalClamped(i.posW, posWPainter, normalW, -dirWPainter,  _Radius, _Hardness);
                float edge = m * paintStrength;
                float4 clrOrg = tex2D(_TexPaint, i.uv);

                float3 rslt = lerp(clrOrg.xyz, paintMask, edge);
                float sum = rslt.x+rslt.y+rslt.z;
                sum = step(0, sum-1) * (sum-1) + 1;
                rslt = rslt/sum;
                return float4(rslt, 1);
            }
            ENDCG
        }
    }
}
