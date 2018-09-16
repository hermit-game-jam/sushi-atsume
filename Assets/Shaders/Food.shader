Shader "Custom/Food"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        [NoScaleOffset]
        _Normal("Normal", 2D) = "normal" {}
        _NormalIntensity("Intensity", Range(0.0, 1.0)) = 0.1
        [Header(Rimlight)]
        _RimlightIntensity("Intensity", Range(0.0, 1.0)) = 0.5
        _RimlightSmoothness("Smoothness", Range(0.1, 1.0)) = 0.5
        [Header(Specular)]
        _SpecularIntensity("Intensity", Range(0.0, 1.0)) = 1.0
        _SpecularSmoothness("Smoothness", Range(0.001, 1.0)) = 0.5
        [Header(Gradation Map)]
        [Toggle(GradationMapEnabled)]
        _IsGradationMapEnabled("Gradation Map", Float) = 0
        [NoScaleOffset]
        _GradationMap("Texture", 2D) = "white" {}
        _GradationTone("Tone", Range(0.1, 1.0)) = 0.5
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
            #pragma shader_feature GradationMapEnabled

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

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
                float3 world_view_dir : WorldViewDir;
                float3x3 tangent_to_world : TangentToWorld;
                float3 ambient : AmbientColor;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _Normal;
            float _NormalIntensity;

            float _RimlightIntensity;
            float _RimlightSmoothness;

            float _SpecularIntensity;
            float _SpecularSmoothness;

            sampler2D _GradationMap;
            float _GradationTone;
            
            v2f vert (appdata v)
            {
                v2f o;
                float3 world_vertex = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)).xyz;
                o.vertex = mul(UNITY_MATRIX_VP, float4(world_vertex, 1));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.world_normal = UnityObjectToWorldNormal(v.normal);
                o.world_light_dir = normalize(UnityWorldSpaceLightDir(world_vertex));
                o.world_view_dir = normalize(UnityWorldSpaceViewDir(world_vertex));

                TANGENT_SPACE_ROTATION;
                o.tangent_to_world = transpose(mul(rotation, unity_WorldToObject));

                o.ambient = ShadeSH9(float4(v.normal, 1));

                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                i.world_light_dir = normalize(i.world_light_dir);
                i.world_view_dir =  normalize(i.world_view_dir);

                half4 dest = tex2D(_MainTex, i.uv);

                #ifdef GradationMapEnabled
                    dest = tex2D(_GradationMap, float2(pow(dest.r, _GradationTone), 0));
                #endif

                float3 normal_base = UnpackNormal(tex2D(_Normal, i.uv));
                float3 world_normal_base = mul(i.tangent_to_world, normal_base);
                float3 world_normal = normalize(lerp(i.world_normal, world_normal_base, _NormalIntensity));

                float lightness = dot(i.world_light_dir, world_normal) * 0.5 + 0.5;
                float3 diffuse = _LightColor0.rgb * lightness;
                float3 specular = pow(saturate(dot(normalize(i.world_light_dir + i.world_view_dir), world_normal)), rcp(_SpecularSmoothness)) * _SpecularIntensity;
                float3 rimlight = pow(1 - dot(i.world_view_dir, world_normal), rcp(_RimlightSmoothness)) * _RimlightIntensity * _LightColor0.rgb;
                dest.rgb = dest.rgb * (diffuse + i.ambient + rimlight) + specular;

                return dest;
            }

            ENDHLSL
        }
    }
}
