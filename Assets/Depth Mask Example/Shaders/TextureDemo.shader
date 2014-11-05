Shader "My Shaders/CHANGE DA SHIZZLE" {
 Properties {
     _NewTex ("NewTex", 2D) = "white" {}
     _OrigTex ("OrigTex(RGB)", 2D) = "white" {}
     _RedColor ("RedColor", Color) = (1,1,1,1)
     _GreenColor ("GreenColor", Color) = (1,0,0,1)
     _BlueColor ("BlueColor", Color) = (0,1,0,1)
     _AlphaColor ("AlphaColor", Color) = (0,1,0,1)
 }
 
 SubShader {
     Tags { "RenderType"="Opaque" }
     LOD 100
     
     Pass {  
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             
             #include "UnityCG.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
             };
 
             sampler2D _NewTex;
             float4 _NewTex_ST;
             sampler2D _OrigTex;
             float4 _OrigTex_ST;
             float4 _RedColor;
             float4 _GreenColor;
             float4 _BlueColor;
             float4 _AlphaColor;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _NewTex);
                 return o;
             }
             
             fixed4 frag (v2f i) : COLOR
             {
                 fixed4 col = tex2D(_NewTex, i.texcoord);
                 fixed4 col2 = tex2D (_OrigTex, i.texcoord);
 
                 //change code blow if need any detail in _MainTex
                 col = lerp(col, _RedColor, col2.r);//change area in red to _ShirtColor
                 col = lerp(col, _GreenColor, col2.g);//change area in green to _NameColor
                 col = lerp(col, _BlueColor, col2.b);//change area in blue to _NumColor
 				 col = lerp(_AlphaColor,col, col2.a);//change Alpha area to _NumColor
 
                 return col;
             }
 
         ENDCG
     }
 }
 
 }
