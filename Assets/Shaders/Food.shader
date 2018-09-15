Shader "Custom/Food"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        [NoScaleOffset]
        _Normal("Normal", 2D) = "normal" {}
        _NormalIntensity("Intensity", Range(0.0, 1.0)) = 0.1
    }

    SubShader
    {
        Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Opaque" }

        Blend One Zero

        Cull Back
        ZWrite True
        ZTest LEqual

        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            HLSLPROGRAM

            #pragma target 3.0

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : Position;
                float2 uv : Texcoord0;
                float3 normal : Normal;
                float4 tangent : Tangent;
            };

            struct v2f
            {
                float4 vertex : SV_Position;
                float2 uv : Texcoord0;
                float3 world_normal : WorldNormal;
                float3 world_light_dir : WorldLightDir;
                float3x3 tangent_to_world : TangentToWorld;
                float3 ambient : Ambient;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _Normal;
            float _NormalIntensity;
            
            v2f vert (appdata v)
            {
                v2f o;
                float3 world_vertex = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)).xyz;
                o.vertex = mul(unity_MatrixVP, float4(world_vertex, 1));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.world_normal = UnityObjectToWorldNormal(v.normal);
                o.world_light_dir = normalize(UnityWorldSpaceLightDir(world_vertex));

                TANGENT_SPACE_ROTATION;
                o.tangent_to_world = mul(transpose(unity_WorldToObject), transpose(rotation));

                o.ambient = ShadeSH9(float4(v.normal, 1));

                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                i.world_light_dir = normalize(i.world_light_dir);

                half4 dest = tex2D(_MainTex, i.uv);

                float3 normal_base = UnpackNormal(tex2D(_Normal, i.uv));
                float3 world_normal_base = mul(i.tangent_to_world, normal_base);
                float3 world_normal = normalize(lerp(i.world_normal, world_normal_base, _NormalIntensity));

                float lightness = dot(i.world_light_dir, world_normal) * 0.5 + 0.5;
                dest.rgb *= lightness + i.ambient;

                return dest;
            }

            ENDHLSL
        }
    }
}
