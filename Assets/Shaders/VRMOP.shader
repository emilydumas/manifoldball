Shader "VRMOP"
{
	Properties
	{
		_Albedo("Albedo", Color) = (1,0,0,1)

		// Other matrices we can update with SetMatrix.
		// But for PerRendererData we are stuck with vectors
		[PerRendererData] _followerpose1("FollowerPose1", Vector) = (1,0,0,0)
		[PerRendererData] _followerpose2("FollowerPose2", Vector) = (0,1,0,0)
		[PerRendererData] _followerpose3("FollowerPose3", Vector) = (0,0,1,0)
		[PerRendererData] _followerpose4("FollowerPose4", Vector) = (0,0,0,1)
	}
		
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

		    // "vertex to fragment"
			struct v2f
			{
				half diffuse : COLOR0; // diffuse lighting amount
				float4 vertex : SV_POSITION;
			};

			fixed4 _Albedo;

			// Follower pose columns
			// Typically set only ONCE and not updated
			float4 _followerpose1, _followerpose2, _followerpose3, _followerpose4;

			// Object pose
			// The position and orientation of the object
			float4x4 _objectpose = float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

			// Inverse camera pose
			float4x4 _invcamerapose = float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

			// Fixed VP
			float4x4 _VP0 = float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

			// Whether to use stored, fixed VP
			int _locked = 0;

			// Target eye
			int _eye = 0;

			//static const float4x4 _eye0shift = float4x4(1,0,0,0,  0,1,0,0,  0,0,1,0,  0,0,0,1);
			//static const float4x4 _eye1shift = float4x4(1,0,0,-0.06,  0,1,0,0,  0,0,1,0,  0,0,0,1);
			float4x4 _eye0shift = float4x4(1,0,0,0,  0,1,0,0,  0,0,1,0,  0,0,0,1);
			float4x4 _eye1shift = float4x4(1,0,0,0,  0,1,0,0,  0,0,1,0,  0,0,0,1);

			v2f vert(appdata v)
			{
				float4x4 _followerpose = float4x4(_followerpose1, _followerpose2, _followerpose3, _followerpose4);
				v2f o;

				o.vertex = mul(_invcamerapose,
							   mul(_followerpose,
							     mul(_objectpose,
							       mul(UNITY_MATRIX_M,v.vertex))));

				if (_eye == 0) {
					o.vertex = mul(_eye0shift,o.vertex);
				} else {
					o.vertex = mul(_eye1shift,o.vertex);
				}

				if (_locked) {
					o.vertex = mul(_VP0,o.vertex);
				} else {
					o.vertex = mul(UNITY_MATRIX_VP,o.vertex);
				}

				// Lambert lighting model; based in part on:
			    // https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html
				half3 worldNormal =   mul(_followerpose,
									    mul(_objectpose,
									      mul(UNITY_MATRIX_M, v.normal)));
				o.diffuse = 0.4 + 0.8*max(0, dot(normalize(worldNormal), _WorldSpaceLightPos0.xyz));

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _Albedo * i.diffuse;
			}
			ENDCG
		}
	}
}
