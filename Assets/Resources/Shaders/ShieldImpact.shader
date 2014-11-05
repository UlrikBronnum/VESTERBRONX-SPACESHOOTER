Shader "Game/ShieldImpact" {

	Properties {
		_Color ("Color" , Color) = (1.0,1.0,1.0,1.0)
		_ColorR("Color Red" , Color) = (0.0,0.0,0.0,0.0)
		_ColorG ("Color Green" , Color) = (0.0,0.0,0.0,0.0)
		_ColorB ("Color Blue" , Color) = (0.0,0.0,0.0,0.0)
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}
		_BumpDepth ("Bump Depth", Range(-2.0,2.0)) = 1.0
		_ShieldBlend ("Shield" , float) = 0.0
	}
	
	Subshader{
		
		Pass {
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			// user defined variables
			uniform float4 _Color;
			uniform float4 _ColorR;
			uniform float4 _ColorG;
			uniform float4 _ColorB;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform float _BumpDepth;
			uniform float _ShieldBlend;
			
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
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;
				
				return o;
			}
			
			
			float4 frag(vertexOutput i) : COLOR
			{
				float3 finalColor;
			
			
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
				float3 specularRefection = diffuseReflection * _Color.xyz * pow( saturate (dot ( reflect(-lightDirection , normalDirection),viewDirection)), 10);
				
				float3 finalLight =  UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + specularRefection;
				
				
				
				if(tex.x > 0.8 && tex.y < 0.3 && tex.z < 0.3){
					finalColor = _ColorR.xyz * finalLight *_Color.xyz;
					if(_ShieldBlend < 1){
						finalColor = finalColor * (float3(0.0,0.0,1.0) * sin(_Time.x*360));
					}
				}
				else if(tex.x < 0.1 && tex.y > 0.9 && tex.z < 0.1){
					finalColor =  _ColorG.xyz * finalLight *_Color.xyz;
					if(_ShieldBlend < 1){
						finalColor = finalColor * (float3(0.0,0.0,1.0) * sin(_Time.x*360));
					}
				}
				else if(tex.x < 0.1 && tex.y > 0.1 && tex.z < 0.7){
					finalColor =  _ColorB.xyz * finalLight *_Color.xyz;
					if(_ShieldBlend < 1){
						finalColor = finalColor * (float3(0.0,0.0,1.0) * sin(_Time.x*360));
					}
				}
				else{
					finalColor = tex.xyz * finalLight *_Color.xyz;
					if(_ShieldBlend  < 1){
						finalColor = finalColor * (float3(0.0,0.0,1.0) * sin(_Time.x*360));				
					}
				}
				
				return float4(finalColor, 0.0);
			}	
			
			ENDCG
		}
		
		
	}
	//FallBack "Diffuse"
}
