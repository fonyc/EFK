Shader "MilitaryUI/FireSparks"
{
    Properties
    {
        _Mask ("Mask", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Size ("Size", Float) = 0.12
        _Speed ("Speed", Vector) = (3, 3, 0, 0)
        _GridSize ("Grid size", Float) = 0.04
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _Mask;
            float4 _Mask_ST;
            float4 _Color;
            float2 _Speed;
            float _GridSize;
            float _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv2 = TRANSFORM_TEX(v.uv2, _Mask);
                return o;
            }

            float3 mod289(float3 x) {
                return x - floor(x * (1.0 / 289.0)) * 289.0;
            }

            float4 mod289(float4 x) {
                return x - floor(x * (1.0 / 289.0)) * 289.0;
            }

            float4 permute(float4 x) {
                    return mod289(((x*34.0)+1.0)*x);
            }

            float4 taylorInvSqrt(float4 r)
            {
                return 1.79284291400159 - 0.85373472095314 * r;
            }

            float snoise(float3 v)
                { 
                float2	C = float2(1.0/6.0, 1.0/3.0) ;
                float4	D = float4(0.0, 0.5, 1.0, 2.0);

            // First corner
                float3 i	= floor(v + dot(v, C.yyy) );
                float3 x0 =	 v - i + dot(i, C.xxx) ;

            // Other corners
                float3 g = step(x0.yzx, x0.xyz);
                float3 l = 1.0 - g;
                float3 i1 = min( g.xyz, l.zxy );
                float3 i2 = max( g.xyz, l.zxy );

                //	 x0 = x0 - 0.0 + 0.0 * C.xxx;
                //	 x1 = x0 - i1	+ 1.0 * C.xxx;
                //	 x2 = x0 - i2	+ 2.0 * C.xxx;
                //	 x3 = x0 - 1.0 + 3.0 * C.xxx;
                float3 x1 = x0 - i1 + C.xxx;
                float3 x2 = x0 - i2 + C.yyy; // 2.0*C.x = 1/3 = C.y
                float3 x3 = x0 - D.yyy;			// -1.0+3.0*C.x = -0.5 = -D.y

            // Permutations
                i = mod289(i); 
                float4 p = permute( permute( permute( 
                                    i.z + float4(0.0, i1.z, i2.z, 1.0 ))
                                  + i.y + float4(0.0, i1.y, i2.y, 1.0 )) 
                                  + i.x + float4(0.0, i1.x, i2.x, 1.0 ));

            // Gradients: 7x7 points over a square, mapped onto an octahedron.
            // The ring size 17*17 = 289 is close to a multiple of 49 (49*6 = 294)
                float n_ = 0.142857142857; // 1.0/7.0
                float3	ns = n_ * D.wyz - D.xzx;

                float4 j = p - 49.0 * floor(p * ns.z * ns.z);	//	mod(p,7*7)

                float4 x_ = floor(j * ns.z);
                float4 y_ = floor(j - 7.0 * x_ );		// mod(j,N)

                float4 x = x_ *ns.x + ns.yyyy;
                float4 y = y_ *ns.x + ns.yyyy;
                float4 h = 1.0 - abs(x) - abs(y);

                float4 b0 = float4( x.xy, y.xy );
                float4 b1 = float4( x.zw, y.zw );

                //vec4 s0 = vec4(lessThan(b0,0.0))*2.0 - 1.0;
                //vec4 s1 = vec4(lessThan(b1,0.0))*2.0 - 1.0;
                float4 s0 = floor(b0)*2.0 + 1.0;
                float4 s1 = floor(b1)*2.0 + 1.0;
                float4 sh = - step(h, float4(0, 0, 0, 0));

                float4 a0 = b0.xzyw + s0.xzyw*sh.xxyy ;
                float4 a1 = b1.xzyw + s1.xzyw*sh.zzww ;

                float3 p0 = float3(a0.xy,h.x);
                float3 p1 = float3(a0.zw,h.y);
                float3 p2 = float3(a1.xy,h.z);
                float3 p3 = float3(a1.zw,h.w);

            //Normalise gradients
                //vec4 norm = taylorInvSqrt(vec4(dot(p0,p0), dot(p1,p1), dot(p2, p2), dot(p3,p3)));
                float4 norm = rsqrt(float4(dot(p0,p0), dot(p1,p1), dot(p2, p2), dot(p3,p3)));
                p0 *= norm.x;
                p1 *= norm.y;
                p2 *= norm.z;
                p3 *= norm.w;

            // Mix final noise value
                float4 m = max(0.6 - float4(dot(x0,x0), dot(x1,x1), dot(x2,x2), dot(x3,x3)), 0.0);
                m = m * m;
                return 42.0 * dot( m*m, float4( dot(p0,x0), dot(p1,x1), dot(p2,x2), dot(p3,x3) ) );
            }

            float prng(float2 seed) {
                seed = frac (seed * float2 (5.3983, 5.4427));
                seed += dot (seed.yx, seed.xy + float2 (21.5351, 14.3137));
                return frac (seed.x * seed.y * 95.4337);
            }

            float noiseStack(float3 pos, int octaves, float falloff){
                float noise = snoise(float3(pos));
                float off = 1.0;
                if (octaves>1) {
                    pos *= 2.0;
                    off *= falloff;
                    noise = (1.0-off)*noise + off*snoise(float3(pos));
                }
                if (octaves>2) {
                    pos *= 2.0;
                    off *= falloff;
                    noise = (1.0-off)*noise + off*snoise(float3(pos));
                }
                if (octaves>3) {
                    pos *= 2.0;
                    off *= falloff;
                    noise = (1.0-off)*noise + off*snoise(float3(pos));
                }
                return (1.0+noise)/2.0;
            }

            float2 noiseStackUV(float3 pos, int octaves, float falloff, float diff){
                float displaceA = noiseStack(pos,octaves,falloff);
                float displaceB = noiseStack(pos+float3(3984.293,423.21,5235.19),octaves,falloff);
                return float2(displaceA,displaceB);
            }

            float2 unity_gradientNoise_dir(float2 p)
            {
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }

            float unity_gradientNoise(float2 p)
            {
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(unity_gradientNoise_dir(ip), fp);
                float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
            }

            float unity_noise_randomValue (float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float PI = 3.14;
                float gridSize = _GridSize;
                float2 coords = i.uv - _Speed * _Time.x;
                float2 distort = noiseStackUV(4 * float3(coords,2*_Time.x),1,0.4,0.1);
                coords = coords - 0.1* abs(distort);
                float2 cell = floor(coords / gridSize);
                float val = unity_noise_randomValue(cell);

                float life = min(10.0*(1.0-min((cell.y+(7*_Time.x/gridSize))/(24.0-20.0*val),1.0)),1.0);

                float radians = val * 2 * PI - 40 * _Time.x;

                float2 circle = float2(sin(radians), cos(radians));
                float maskValue = tex2D(_Mask, i.uv2).r;
                float size = maskValue * val*_Size;
                float2 offset = (0.5 - size) * gridSize * circle;

                float2 module = fmod(abs(coords) + offset, gridSize) - 0.5 * float2(gridSize, gridSize);

                float radius = length(module);

                

                float intensity = max(0, 1 - radius/(size * gridSize));

                return float4(intensity * (_Color.rgb * 1.8), _Color.a * intensity);
            }
            ENDCG
        }
    }
}
