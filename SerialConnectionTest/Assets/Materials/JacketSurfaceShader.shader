Shader "Custom/JacketSurfaceShader" {
	Properties {
		_DefaultColor ("Default Color", Color) = (1,1,1,1)
        _HitColor ("Hit Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = .0
	    _Radius ("Separation Radius", Float) = 0.1
        _LeftShoulder ("Left Shoulder", Vector) = (0, 0, 0) 
        // TODO 他のポイントも追加
    }
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
        
        struct appdata {
            float4 vertex : Position;
        };
        
		struct Input {
			float2 uv_MainTex;
            float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _DefaultColor;
        fixed4 _HitColor;
        float _SeparationRadius;
        float3 _LeftShoulder;
        
        
        float dist (float3 part, float3 a) {
            float3 c = a-part;
            return pow(pow(c.x, 2) + pow(c.y, 2) + pow(c.z, 2), 0.5);
        }
        
		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
            // TODO step関数を他のポイントとも複合したものにする
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * lerp(_HitColor, _DefaultColor, step(_SeparationRadius, dist(_LeftShoulder, IN.worldPos)));
            o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
