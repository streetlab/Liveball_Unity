﻿// Compiled shader for Android, uncompressed size: 3.3KB

// Skipping shader variants that would not be included into build of current scene.

Shader "Particles/Additive2" {
Properties {
 _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
 _MainTex ("Particle Texture", 2D) = "white" { }
 _InvFade ("Soft Particles Factor", Range(0.01,3)) = 1
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }


 // Stats for Vertex shader:
 //        gles : 6 avg math (3..10), 1 avg texture (1..2)
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Blend SrcAlpha One
  AlphaTest Greater 0.01
  ColorMask RGB
  GpuProgramID 23090
Program "vp" {
SubProgram "gles " {
// Stats: 3 math, 1 textures
Keywords { "SOFTPARTICLES_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying lowp vec4 xlv_COLOR;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_COLOR = _glesColor;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _TintColor;
varying lowp vec4 xlv_COLOR;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = (((2.0 * xlv_COLOR) * _TintColor) * texture2D (_MainTex, xlv_TEXCOORD0));
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
// Stats: 10 math, 2 textures
Keywords { "SOFTPARTICLES_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
varying lowp vec4 xlv_COLOR;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD2;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 o_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_2 * 0.5);
  highp vec2 tmpvar_5;
  tmpvar_5.x = tmpvar_4.x;
  tmpvar_5.y = (tmpvar_4.y * _ProjectionParams.x);
  o_3.xy = (tmpvar_5 + tmpvar_4.w);
  o_3.zw = tmpvar_2.zw;
  tmpvar_1.xyw = o_3.xyw;
  tmpvar_1.z = -((glstate_matrix_modelview0 * _glesVertex).z);
  gl_Position = tmpvar_2;
  xlv_COLOR = _glesColor;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD2 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

uniform highp vec4 _ZBufferParams;
uniform sampler2D _MainTex;
uniform lowp vec4 _TintColor;
uniform highp sampler2D _CameraDepthTexture;
uniform highp float _InvFade;
varying lowp vec4 xlv_COLOR;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD2;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1.xyz = xlv_COLOR.xyz;
  highp vec4 tmpvar_2;
  tmpvar_2 = texture2DProj (_CameraDepthTexture, xlv_TEXCOORD2);
  highp float tmpvar_3;
  tmpvar_3 = (xlv_COLOR.w * clamp ((_InvFade * 
    ((1.0/(((_ZBufferParams.z * tmpvar_2.x) + _ZBufferParams.w))) - xlv_TEXCOORD2.z)
  ), 0.0, 1.0));
  tmpvar_1.w = tmpvar_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = (((2.0 * tmpvar_1) * _TintColor) * texture2D (_MainTex, xlv_TEXCOORD0));
  gl_FragData[0] = tmpvar_4;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "SOFTPARTICLES_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SOFTPARTICLES_ON" }
"!!GLES"
}
}
 }
}
}