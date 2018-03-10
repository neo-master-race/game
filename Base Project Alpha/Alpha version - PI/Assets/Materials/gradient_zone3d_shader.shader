// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Schneberger Maxime
//10-03-18

//crée un nouveau shader afin d'affecter des matériaux en gradient, non possible en unity 'basique'
Shader "Custom/SpriteGradient_3D" {
 Properties {
     [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
     _Color_Left ("Left Color", Color) = (1,1,1,1)
     _Color_TopR ("Top-Right Color", Color) = (1,1,1,1)
	 _Color_BottomR ("Bottom-Right Color", Color) = (1,1,1,1)
     _Scale ("Scale", Float) = 1
     
 // these six unused properties are required when a shader
 // is used in the UI system, or you get a warning.
 // look to UI-Default.shader to see these.
 _StencilComp ("Stencil Comparison", Float) = 8
 _Stencil ("Stencil ID", Float) = 0
 _StencilOp ("Stencil Operation", Float) = 0
 _StencilWriteMask ("Stencil Write Mask", Float) = 255
 _StencilReadMask ("Stencil Read Mask", Float) = 255
 _ColorMask ("Color Mask", Float) = 15
 // see for example
 // http://answers.unity3d.com/questions/980924/ui-mask-with-shader.html
 
 }
  

//création du mécanisme du shader
 SubShader {
     Tags {"Queue"="Background"  "IgnoreProjector"="True"}
     LOD 100
  
     ZWrite On
  
     Pass {
         CGPROGRAM
         #pragma vertex vert  
         #pragma fragment frag
         #include "UnityCG.cginc"
  
         fixed4 _Color_Left;
         fixed4 _Color_TopR;
		 fixed4 _Color_BottomR;
         fixed  _Scale;
  
         struct v2f {
             float4 pos : SV_POSITION;
             float4 texcoord : TEXCOORD0;
         };
  
         v2f vert (appdata_full v) {
             v2f o;
             o.pos = UnityObjectToClipPos (v.vertex);
             o.texcoord = v.texcoord;
             return o;
         }
        
  
         fixed4 frag (v2f i) : COLOR {
			float x = i.texcoord.x;
            float y = i.texcoord.y;
            float x_value=x*(_Color_TopR.x+(1-y)*(_Color_BottomR.x-_Color_TopR.x));
			float y_value=x*(_Color_TopR.y+(1-y)*(_Color_BottomR.y-_Color_TopR.y));
			float z_value=x*(_Color_TopR.z+(1-y)*(_Color_BottomR.z-_Color_TopR.z));
			return fixed4(x_value,y_value,z_value, 1);
         }
             ENDCG
         }
     }
 }
