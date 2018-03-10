// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteGradient_2D" {
 Properties {
     [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
     _Color ("Start_End Color", Color) = (1,1,1,1)
     _Color2 ("1sixth Color", Color) = (1,1,1,1)
	 _Color3 ("2sixth Color", Color) = (1,1,1,1)
	 _Color4 ("3sixth Color", Color) = (1,1,1,1)
	 _Color5 ("4sixth Color", Color) = (1,1,1,1)
	 _Color6 ("5sixth Color", Color) = (1,1,1,1)
     _six1 ("1sixth",float) = 0.1666
	 _six2 ("2sixth",float) = 0.3333
	 _six3 ("3sixth",float) = 0.5
	 _six4 ("4sixth",float) = 0.6666
	 _six5 ("5sixth",float) = 0.8333
     
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
  
 SubShader {
     Tags {"Queue"="Background"  "IgnoreProjector"="True"}
     LOD 100
  
     ZWrite On
  
     Pass {
         CGPROGRAM
         #pragma vertex vert  
         #pragma fragment frag
         #include "UnityCG.cginc"
  
         fixed4 _Color;
         fixed4 _Color2;
		 fixed4 _Color3;
		 fixed4 _Color4;
		 fixed4 _Color5;
		 fixed4 _Color6;
         float  _six1;
		 float  _six2;
		 float  _six3;
		 float  _six4;
		 float  _six5;
 
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
 
		//Affecte la couleur en fonction de la position x entre 0 et 1
         fixed4 frag (v2f i) : COLOR {
             fixed4 c = lerp(_Color, _Color2, i.texcoord.x / _six1) * step(i.texcoord.x, _six1);
			 //exemple pour color2 à color3 : lerp(x,y,z) : va de x à y avez z en intermédiaire comme paramètre * step(a,b) : seulement si b>=a 
			 c += lerp(_Color2, _Color3, (i.texcoord.x - _six1) / (_six2 - _six1)) * step(_six1, i.texcoord.x) * step(i.texcoord.x,_six2);
			 c += lerp(_Color3, _Color4, (i.texcoord.x - _six2) / (_six3 - _six2)) * step(_six2, i.texcoord.x) * step(i.texcoord.x,_six3);
			 c += lerp(_Color4, _Color5, (i.texcoord.x - _six3) / (_six4 - _six3)) * step(_six3, i.texcoord.x) * step(i.texcoord.x,_six4);
			 c += lerp(_Color5, _Color6, (i.texcoord.x - _six4) / (_six5 - _six4)) * step(_six4, i.texcoord.x) * step(i.texcoord.x,_six5);
			 c += lerp(_Color6, _Color, (i.texcoord.x - _six5) / (1 - _six5)) * step(_six5, i.texcoord.x);
             c.a = 1;
             return c;
         }
         ENDCG
         }
     }
 }
