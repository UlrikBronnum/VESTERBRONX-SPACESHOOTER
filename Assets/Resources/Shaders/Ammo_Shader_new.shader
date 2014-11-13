Shader "Game/Ammonition_Shader_new" {
	Properties {
		_Ammo_Color ("Ammo_Color" , Color) = (1.0,1.0,1.0,1.0)
		_Ammo_Blend_Color ("Ammo_Blend_Color" , Color) = (1.0,1.0,1.0,1.0)
	}
	Subshader{
		Pass {
						
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			// user defined variables
			uniform float4 _Ammo_Color;
			uniform float4 _Ammo_Blend_Color;
			
			// unity defined variables
			uniform float4 _LightColor0;
			//float4x4 _Object2World;
			//float4x4 _World2Object;
			//float4 _WorldSpaceLightPos0;
			
			
	
			// base input structs
			struct vertexInput{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalWorld : TEXCOORD2;

			};
			
			
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.normalWorld = normalize ( mul ( float4(v.normal, 0.0), _World2Object ).xyz);
				o.pos =  mul(UNITY_MATRIX_MVP,v.vertex);
				o.posWorld = mul (_Object2World, v.vertex);
				return o;
			}
			
			
			float4 frag(vertexOutput i) : COLOR
			{
				
				float4 finalColor;
				
				float3 cameraToAmmo = normalize ( _WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float specularRefection = max(0.0, pow( saturate (dot ( reflect(cameraToAmmo , i.normalWorld),-cameraToAmmo)), 0.01));
				float3 lightIntencity = normalize(_Ammo_Blend_Color.xyz * max(0,dot( cameraToAmmo ,i.posWorld.xyz)));
				finalColor = normalize(float4(_Ammo_Color.xyz + _Ammo_Blend_Color.xyz * specularRefection + lightIntencity.xyz * specularRefection + float4(1.0,1.0,1.0,1.0) * specularRefection, 1) );	
				
				return finalColor;
			}	
		
			ENDCG
			
		}
	}
	FallBack "Diffuse"
}