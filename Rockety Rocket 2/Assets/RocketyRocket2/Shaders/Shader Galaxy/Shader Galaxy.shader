Shader "Custom/Shader Galaxy"
{
    Properties
    {
        _StarAmount("Star Amount", Range(0,1)) = 0.5
        _StarSize("Star Size", Range(0.001, 0.1)) = 0.015
        _MainColor ("Main Color", Color) = (0.2,0.4,1,1)
        _SecondaryColor ("Secondary Color", Color) = (1,0.3,0.8,1)
        _TertiaryColor ("Tertiary Color", Color) = (0.3,1,0.2,1)
        _BackgroundColor ("Background Color", Color) = (0.05,0.05,0.1,1)
        _Intensity ("Nebula Intensity", Range(0,3)) = 1.5
        _Speed ("Animation Speed", Range(0,2)) = 0.5
        _Alpha ("Overall Alpha", Range(0,1)) = 0.7
        _ColorVariation ("Color Variation", Range(0,2)) = 1.0
        _StarDensity ("Star Density", Range(0,1)) = 0.5
        _GalaxyRadius ("Galaxy Radius", Range(0.1, 2)) = 1.0
        _EdgeBlurStrength ("Edge Blur Strength", Range(0, 5)) = 2.0
        _EdgeBlurDistance ("Edge Blur Distance", Range(0, 0.5)) = 0.2
        _BackgroundIntensity ("Background Intensity", Range(0,1)) = 0.3
    }

    SubShader
    {
        Tags { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent" 
            "RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            Name "Unlit"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            float hash(float2 p)
            {
                return frac(sin(dot(p, float2(12.9898,78.233))) * 43758.5453);
            }

            float2 hash2D(float2 p)
            {
                return frac(sin(float2(dot(p,float2(127.1,311.7)), dot(p,float2(269.5,183.3)))) * 43758.5453);
            }

            float noise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);

                float a = hash(i);
                float b = hash(i + float2(1,0));
                float c = hash(i + float2(0,1));
                float d = hash(i + float2(1,1));

                float2 u = f*f*(3.0 - 2.0*f);

                return lerp(lerp(a,b,u.x), lerp(c,d,u.x), u.y);
            }

            float fbm(float2 p, int octaves)
            {
                float value = 0.0;
                float amplitude = 0.5;
                float frequency = 1.0;
                
                for (int i = 0; i < octaves; i++)
                {
                    value += amplitude * noise(p * frequency);
                    amplitude *= 0.5;
                    frequency *= 2.0;
                }
                return value;
            }

            float4 _MainColor;
            float4 _SecondaryColor;
            float4 _TertiaryColor;
            float4 _BackgroundColor;
            float _Intensity;
            float _Speed;
            float _Alpha;
            float _ColorVariation;
            float _StarDensity;
            float _GalaxyRadius;
            float _StarAmount;
            float _StarSize;
            float _EdgeBlurStrength;
            float _EdgeBlurDistance;
            float _BackgroundIntensity;

            // Improved random star function
            float randomStars(float2 uv, float starAmount, float starSize)
            {
                float stars = 0.0;
                
                // Layer 1: High frequency random points
                float2 seed1 = uv * 500.0;
                float2 randomPos1 = hash2D(floor(seed1));
                float2 localUV1 = frac(seed1);
                float dist1 = length(localUV1 - randomPos1);
                float star1 = 1.0 - smoothstep(0.0, starSize, dist1);
                stars += star1 * step(randomPos1.x, starAmount * 0.3);
                
                // Layer 2: Medium frequency random points  
                float2 seed2 = uv * 200.0;
                float2 randomPos2 = hash2D(floor(seed2));
                float2 localUV2 = frac(seed2);
                float dist2 = length(localUV2 - randomPos2);
                float star2 = 1.0 - smoothstep(0.0, starSize * 1.5, dist2);
                stars += star2 * step(randomPos2.x, starAmount * 0.5);
                
                // Layer 3: Low frequency random points (bigger stars)
                float2 seed3 = uv * 80.0;
                float2 randomPos3 = hash2D(floor(seed3));
                float2 localUV3 = frac(seed3);
                float dist3 = length(localUV3 - randomPos3);
                float star3 = 1.0 - smoothstep(0.0, starSize * 2.0, dist3);
                stars += star3 * step(randomPos3.x, starAmount * 0.2);
                
                return saturate(stars) * _StarDensity;
            }

            // Edge blur function
            float calculateEdgeBlur(float2 uv)
            {
                // Calculate distance to each edge
                float distToLeft = uv.x;
                float distToRight = 1.0 - uv.x;
                float distToBottom = uv.y;
                float distToTop = 1.0 - uv.y;
                
                // Find the minimum distance to any edge
                float minDistToEdge = min(min(distToLeft, distToRight), min(distToBottom, distToTop));
                
                // Calculate blur factor based on distance to edge
                float blurFactor = smoothstep(0.0, _EdgeBlurDistance, minDistToEdge);
                
                // Invert so edges have more blur (0 at edges, 1 in center)
                blurFactor = 1.0 - blurFactor;
                
                return blurFactor * _EdgeBlurStrength;
            }

            float4 frag (Varyings i) : SV_Target
            {
                float2 uv = i.uv * 2.0 - 1.0; // center
                float t = _Time.y * _Speed;

                float r = length(uv);
                float angle = atan2(uv.y, uv.x);

                // Calculate edge blur
                float edgeBlur = calculateEdgeBlur(i.uv);

                // Spiral-like motion (your preferred style)
                float spiral = sin(angle*3.0 - r*6.0 + t*0.5) * 0.5 + 0.5;

                // Nebula layers with your animation style
                float2 distortedUV = uv;
                
                // Apply edge-based distortion for blur effect
                if (edgeBlur > 0.0)
                {
                    float2 noiseOffset = float2(
                        noise(uv * 10.0 + t) - 0.5,
                        noise(uv * 10.0 + t + 100.0) - 0.5
                    ) * edgeBlur * 0.1;
                    
                    distortedUV += noiseOffset;
                }

                float n1 = fbm(distortedUV*3.0 + t*0.3, 4);
                float n2 = fbm(distortedUV*6.0 - t*0.5, 3);
                float n3 = fbm(distortedUV*12.0 + t*0.8, 2);

                float n = (n1*0.5 + n2*0.3 + n3*0.2) * _Intensity;

                // Radial falloff for galaxy shape
                float falloff = smoothstep(_GalaxyRadius, 0.0, r);
                n *= falloff;

                // Color blending with your animation
                float cMask1 = fbm(distortedUV*2.0 + t*0.7, 3);
                float cMask2 = fbm(distortedUV*1.3 - t*0.4, 3);

                float3 nebula = lerp(_MainColor.rgb, _SecondaryColor.rgb, cMask1*_ColorVariation);
                nebula = lerp(nebula, _TertiaryColor.rgb, cMask2*_ColorVariation*0.7);
                nebula *= n;

                // Use the improved random stars with your properties
                // Apply edge blur to star density
                float starDensityAtEdge = _StarDensity * (1.0 - edgeBlur * 0.7);
                float stars = randomStars(uv * 5.0, _StarAmount, _StarSize) * starDensityAtEdge;

                // Combine background color with nebula and stars
                float3 finalColor = lerp(_BackgroundColor.rgb * _BackgroundIntensity, nebula + stars, saturate(n + stars));
                
                // Reduce alpha towards edges based on blur factor
                float baseAlpha = saturate(n * 0.8 + stars*0.5) * _Alpha;
                float finalAlpha = baseAlpha * (1.0 - edgeBlur * 0.5);
                finalAlpha = max(finalAlpha, 0.1);

                return float4(finalColor, finalAlpha);
            }
            ENDHLSL
        }
    }
}