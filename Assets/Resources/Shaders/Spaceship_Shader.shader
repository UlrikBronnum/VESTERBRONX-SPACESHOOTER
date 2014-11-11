Shader "Game/Spaceship_Shader" {

	Properties {
		_Texture_Blend_Color ("Blend_Color" , Color) = (1.0,1.0,1.0,1.0)
		_Texture_Override_Color("Overide_Color" , Color) = (1.0,1.0,1.0,1.0)
		
		_Shield_Blend_Color ("Shield_Color" , Color) = (1.0,1.0,1.0,1.0)
		_Shield_Blend ("Shield_Hit" , Range(0.0,1.0)) = 0.0
		_Shield_Left ("Shield_Left" , Range(0.0,1.0)) = 0.0

		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}
		_BumpDepth ("Bump Depth", Range(-2.0,2.0)) = 1.0
		
	}
	
	Subshader{
		Tags {"Queue"="Transparent"}
		
		Pass {
			//Cull Front
			
			//ZWrite Off
			
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			// user defined variables
			uniform float4 _Texture_Blend_Color;
			uniform float4 _Texture_Override_Color;
			
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform float _BumpDepth;
			
			
			
			// unity defined variables
			uniform float4 _LightColor0;
			//float4x4 _Object2World;
			//float4x4 _World2Object;
			//float4 _WorldSpaceLightPos0;
			
			// base input structs
			struct vertexInput{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD;
				float4 tangent : Tangent;
			};
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalWorld : TEXCOORD2;
				float3 tangentWorld : TEXCOORD3;
				float3 binormalWorld : TEXCOORD4;
				
			};
			
			
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				
				o.normalWorld = normalize ( mul ( float4(v.normal, 0.0), _World2Object ).xyz);
				o.tangentWorld = normalize ( mul( _Object2World, v.tangent));
				o.binormalWorld = normalize ( cross(o.normalWorld, o.tangentWorld) * v.tangent.w);
				
				o.posWorld = mul (_Object2World, v.vertex);
			
				o.pos =  mul(UNITY_MATRIX_MVP,v.vertex);
				
				o.tex = v.texcoord;
				
				return o;
			}
			
			
			float4 frag(vertexOutput i) : COLOR
			{
				float4 finalColor;
			
			
				float3 viewDirection = normalize ( _WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 lightDirection;
				float atten;
				
				if (_WorldSpaceLightPos0.w == 0.0){
					atten = 1.0;
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}else{
					float3 fragmentToLightSource = float3(_WorldSpaceLightPos0.xyz - float3(i.posWorld.xyz));
					float distance = length(fragmentToLightSource);
					lightDirection = normalize(lerp(_WorldSpaceLightPos0.w,float3(_WorldSpaceLightPos0.xyz),float3(_WorldSpaceLightPos0.xyz - float3(i.posWorld.xyz))));
					atten = lerp(_WorldSpaceLightPos0.w,1.0,1.0/distance);
					
				}
				
				float4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				float4 texN = tex2D(_BumpMap, i.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);
				
				float3 localCoords = float3(2.0 * texN.ag - float2(1.0,1.0), 0.0);
				localCoords.z = _BumpDepth;
				
				float3x3 local2WorldTranspose = float3x3 (
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
				);
				
				float3 normalDirection = normalize( mul(localCoords, local2WorldTranspose) ) ;
				
				float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection,lightDirection));
				float3 specularRefection = diffuseReflection * tex.xyz * pow( saturate (dot ( reflect(-lightDirection , normalDirection),viewDirection)), 10);
				
				float3 finalLight =  UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + specularRefection;
						
				finalColor =  float4(tex.xyz * finalLight * _Texture_Blend_Color.xyz, 1.0);				
				
				
				return finalColor;
			}	
			
			ENDCG
		}
		Pass {
			
			ZWrite Off
			
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			// user defined variables
			uniform float4 _Shield_Blend_Color;
			uniform float _Shield_Blend;
			uniform float _Shield_Left;

			// unity defined variables
			uniform float4 _LightColor0;
			//float4x4 _Object2World;
			//float4x4 _World2Object;
			//float4 _WorldSpaceLightPos0;
			
			float rand(float2 co){
				return frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453);
			}
			
			
			// base input structs
			struct vertexInput{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD;
			};
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
			};
			
			
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				
				o.pos =  mul(UNITY_MATRIX_MVP,v.vertex);
				o.tex = v.texcoord;			
			
				return o;
			}
			
			
			float4 frag(vertexOutput i) : COLOR
			{
				float4 finalColor;
			
				if(_Shield_Left > 0){
					if(_Shield_Blend == 0){
						finalColor = float4(_Shield_Blend_Color.xyz , 0.8f * _Shield_Left ) ;				
					}else{
						finalColor = float4(normalize(_Shield_Blend_Color.xyz + float3(1.0f,1.0f,1.0f) * (1 + sin (_Time.x * 720) * _Shield_Left)  * rand(i.tex.xy  * _Time.x)) ,rand(i.tex.xy  * _Time.x ) );
					}
				}else{
					finalColor = float4(0f,0f,0f,0f);
				}
				return finalColor;
			}	
			
			ENDCG
		}
		
	}
	//FallBack "Diffuse"
}
