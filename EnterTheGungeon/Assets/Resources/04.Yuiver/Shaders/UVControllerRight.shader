Shader "MyShader/2D/UVSpriteRiht"
{
    Properties
    {
        _MainTex("Diffuse", 2D) = "white" {}
        _MaskTex("Mask", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}

        // Legacy properties. They're here so that materials using this shader can gracefully fallback to the legacy sprite shader.
         [HDR]_Color("Tint", Color) = (1,1,1,1)
         [HDR]_RendererColor("RendererColor", Color) = (1,1,1,1)
         _Flip("Flip", Vector) = (1,1,1,1)
         _AlphaTex("External Alpha", 2D) = "white" {}
         _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }

        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            Pass
            {
                Tags { "LightMode" = "Universal2D" }

                HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                #pragma vertex CombinedShapeLightVertex
                #pragma fragment CombinedShapeLightFragment

                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __
                #pragma multi_compile _ DEBUG_DISPLAY

                struct Attributes
                {
                    float3 positionOS   : POSITION;
                    float4 color        : COLOR;
                    float2  uv          : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4  positionCS  : SV_POSITION;
                    half4   color       : COLOR;
                    float2  uv          : TEXCOORD0;
                    half2   lightingUV  : TEXCOORD1;
                    #if defined(DEBUG_DISPLAY)
                    float3  positionWS  : TEXCOORD2;
                    #endif
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                TEXTURE2D(_MaskTex);
                SAMPLER(sampler_MaskTex);
                half4 _MainTex_ST;
                float4 _Color;
                half4 _RendererColor;

                #if USE_SHAPE_LIGHT_TYPE_0
                SHAPE_LIGHT(0)
                #endif

                #if USE_SHAPE_LIGHT_TYPE_1
                SHAPE_LIGHT(1)
                #endif

                #if USE_SHAPE_LIGHT_TYPE_2
                SHAPE_LIGHT(2)
                #endif

                #if USE_SHAPE_LIGHT_TYPE_3
                SHAPE_LIGHT(3)
                #endif

                Varyings CombinedShapeLightVertex(Attributes v)
                {
                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.positionCS = TransformObjectToHClip(v.positionOS);
                    #if defined(DEBUG_DISPLAY)
                    o.positionWS = TransformObjectToWorld(v.positionOS);
                    #endif
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.lightingUV = half2(ComputeScreenPos(o.positionCS / o.positionCS.w).xy);

                    o.color = v.color * _Color * _RendererColor;
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

                half4 CombinedShapeLightFragment(Varyings i) : SV_Target
                {
                    const half4 main = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.x);
                    const half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);
                    SurfaceData2D surfaceData;
                    InputData2D inputData;

                    InitializeSurfaceData(main.rgb, main.a, mask, surfaceData);
                    InitializeInputData(i.uv, i.lightingUV, inputData);

                    return CombinedShapeLightShared(surfaceData, inputData);
                }
                ENDHLSL
            }

            Pass
            {
                Tags { "LightMode" = "NormalsRendering"}

                HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                #pragma vertex NormalsRenderingVertex
                #pragma fragment NormalsRenderingFragment

                struct Attributes
                {
                    float3 positionOS   : POSITION;
                    float4 color        : COLOR;
                    float2 uv           : TEXCOORD0;
                    float4 tangent      : TANGENT;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4  positionCS      : SV_POSITION;
                    half4   color           : COLOR;
                    float2  uv              : TEXCOORD0;
                    half3   normalWS        : TEXCOORD1;
                    half3   tangentWS       : TEXCOORD2;
                    half3   bitangentWS     : TEXCOORD3;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                TEXTURE2D(_NormalMap);
                SAMPLER(sampler_NormalMap);
                half4 _NormalMap_ST;  // Is this the right way to do this?

                Varyings NormalsRenderingVertex(Attributes attributes)
                {
                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(attributes);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.positionCS = TransformObjectToHClip(attributes.positionOS);
                    o.uv = TRANSFORM_TEX(attributes.uv, _NormalMap);
                    o.color = attributes.color;
                    o.normalWS = -GetViewForwardDir();
                    o.tangentWS = TransformObjectToWorldDir(attributes.tangent.xyz);
                    o.bitangentWS = cross(o.normalWS, o.tangentWS) * attributes.tangent.w;
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"

                half4 NormalsRenderingFragment(Varyings i) : SV_Target
                {
                    const half4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    const half3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv));

                    return NormalsRenderingShared(mainTex, normalTS, i.tangentWS.xyz, i.bitangentWS.xyz, i.normalWS.xyz);
                }
                ENDHLSL
            }

            Pass
            {
                Tags { "LightMode" = "UniversalForward" "Queue" = "Transparent" "RenderType" = "Transparent"}

                HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                #pragma vertex UnlitVertex
                #pragma fragment UnlitFragment

                struct Attributes
                {
                    float3 positionOS   : POSITION;
                    float4 color        : COLOR;
                    float2 uv           : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4  positionCS      : SV_POSITION;
                    float4  color           : COLOR;
                    float2  uv              : TEXCOORD0;
                    float2  rotatedOriginalUV1 : TEXCOORD1; // 추가
                    float2  rotatedOriginalUV2 : TEXCOORD2; // 추가
                    float2  rotatedOriginalUV3 : TEXCOORD3; // 추가
                    float2  rotatedOriginalUV4 : TEXCOORD4; // 추가
                    float2  rotatedOriginalUV5 : TEXCOORD5; // 추가
                    float2  rotatedOriginalUV6 : TEXCOORD6; // 추가
                    float2  rotatedOriginalUV7 : TEXCOORD7; // 추가
                    float2  rotatedOriginalUV8 : TEXCOORD8; // 추가

                    #if defined(DEBUG_DISPLAY)
                    float3  positionWS  : TEXCOORD2;
                    #endif
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                float4 _MainTex_ST;
                float4 _Color;
                half4 _RendererColor;

                float2 OrbitUV(float2 uv, float distance, float angle)
                {
                    float2 center = float2(0.5, 0.8); // 원점 설정 (0.5, 0.5는 텍스처의 중심입니다)
                    uv -= center; // 원점을 중심으로 평행 이동

                    float2 direction = normalize(uv); // 원점에서 UV까지의 방향 벡터
                    float2 offset = direction * distance; // 일정 거리를 유지하기 위한 오프셋 벡터 계산

                    uv += offset; // 일정 거리를 유지하도록 UV 좌표 수정

                    // 회전 적용
                    float2 rotatedUV;
                    rotatedUV.x = uv.x * cos(angle) - uv.y * sin(angle);
                    rotatedUV.y = uv.x * sin(angle) + uv.y * cos(angle);

                    rotatedUV += center; // 원래 위치로 되돌리기
                    return rotatedUV;
                }

                float2 AdjustUV2(float2 uv)
                {
                    uv.x *= 0.5;
                    uv.x += 0.5;
                    return uv;
                }

                Varyings UnlitVertex(Attributes attributes)
                {
                    float speed = 200;
                    float distance = -0.15;
                    float rotationAngle1 = _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle2 = 45 + _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle3 = 90 + _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle4 = 135 + _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle5 = 180 + _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle6 = 225 + _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle7 = 270 + _Time.x * speed * 3.14159265359 / 180.0;
                    float rotationAngle8 = 315 + _Time.x * speed * 3.14159265359 / 180.0;

                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(attributes);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    float2 originalUV = AdjustUV2(attributes.uv);
                    //float2 originalUV = attributes.uv;

                    float2 rotatedOriginalUV1 = OrbitUV(originalUV, distance, rotationAngle1);
                    float2 rotatedOriginalUV2 = OrbitUV(originalUV, distance, rotationAngle2);
                    float2 rotatedOriginalUV3 = OrbitUV(originalUV, distance, rotationAngle3);
                    float2 rotatedOriginalUV4 = OrbitUV(originalUV, distance, rotationAngle4);
                    float2 rotatedOriginalUV5 = OrbitUV(originalUV, distance, rotationAngle5);
                    float2 rotatedOriginalUV6 = OrbitUV(originalUV, distance, rotationAngle6);
                    float2 rotatedOriginalUV7 = OrbitUV(originalUV, distance, rotationAngle7);
                    float2 rotatedOriginalUV8 = OrbitUV(originalUV, distance, rotationAngle8);

                    o.positionCS = TransformObjectToHClip(attributes.positionOS);

                    #if defined(DEBUG_DISPLAY)
                    o.positionWS = TransformObjectToWorld(v.positionOS);

                    #endif
                    o.uv = TRANSFORM_TEX(attributes.uv, _MainTex);
                    o.color = attributes.color * _Color * _RendererColor;
                    o.rotatedOriginalUV1 = rotatedOriginalUV1;
                    o.rotatedOriginalUV2 = rotatedOriginalUV2;
                    o.rotatedOriginalUV3 = rotatedOriginalUV3;
                    o.rotatedOriginalUV4 = rotatedOriginalUV4;
                    o.rotatedOriginalUV5 = rotatedOriginalUV5;
                    o.rotatedOriginalUV6 = rotatedOriginalUV6;
                    o.rotatedOriginalUV7 = rotatedOriginalUV7;
                    o.rotatedOriginalUV8 = rotatedOriginalUV8;

                    return o;
                }

                float4 UnlitFragment(Varyings i) : SV_Target
                {
                    float4 originalColor = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    float4 col1 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV1);
                    float4 col2 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV2);
                    float4 col3 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV3);
                    float4 col4 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV4);
                    float4 col5 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV5);
                    float4 col6 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV6);
                    float4 col7 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV7);
                    float4 col8 = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.rotatedOriginalUV8);

                    float4 blendedCol = col1 * 0.2 + col2 * 0.2 + col3 * 0.2 + col4 * 0.2 + col5 * 0.2 + col6 * 0.2 + col7 * 0.2 + col8 * 0.2;
                    
                    // 알파 값이 아닌 경우에 원본 텍스처의 색상을 사용하는 코드
                    if (blendedCol.a != 0)
                    {
                        blendedCol = originalColor;
                    }

                    // 원본 텍스처 색상과 블렌딩 된 색상을 선형 보간 (lerp) 하는 코드 추가
                    //float4 finalColor = lerp(originalColor, blendedCol, 1);

                    #if defined(DEBUG_DISPLAY)
                    SurfaceData2D surfaceData;
                    InputData2D inputData;
                    half4 debugColor = 0;

                    InitializeSurfaceData(mainTex.rgb, mainTex.a, surfaceData);
                    InitializeInputData(i.uv, inputData);
                    SETUP_DEBUG_DATA_2D(inputData, i.positionWS);

                    if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
                    {
                        return debugColor;
                    }
                    #endif

                    return blendedCol;
                }
                ENDHLSL
            }
        }

            Fallback "Sprites/Default"
}