// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "LinHoweShader/sector"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_Color("Color", Color) = (0.17,0.36,0.81,0.0)
		_Angle("Angle", Range(0, 360)) = 60
		_Gradient("Gradient", Range(0, 1)) = 0
		_Radius("Radius",Range(0, 1)) = 0.5
	}
	SubShader{


		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _Color;
			float _Angle;
			float _Gradient;
			float _Radius;

			struct fragmentInput {
				float4 pos : SV_POSITION;
				float2 uv : TEXTCOORD0;
			};

			fragmentInput vert(appdata_base v)
			{
				fragmentInput o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;

				return o;
			}

			fixed4 frag(fragmentInput i) : SV_Target
			{
				float x = i.uv.x;
				float y = i.uv.y;
				// 离中心点的距离
				float distance = sqrt(pow(x - 0.5, 2) + pow(y - 0.5, 2));
				// 在圆外
				if (distance > _Radius) {
					discard;
				}
				// 根据距离计算透明度渐变
				float grediant = (_Radius*2 - distance -  _Radius * _Gradient) / _Radius;
				// 正常显示的结果
				fixed4 result = tex2D(_MainTex, i.uv) * _Color * fixed4(1,1,1, grediant);
				
				float deg2rad = 0.017453;   // 角度转弧度pi/180
											// 根据角度剔除掉不需要显示的部分
											// 大于180度
				if (_Angle > 180) {
					if (y > 0.5 && abs(0.5 - y) >= abs(0.5 - x) / tan((180 - _Angle / 2) * deg2rad))
						discard;// 剔除
				}
				else    // 180度以内
				{
					if (y > 0.5 || abs(0.5 - y) < abs(0.5 - x) / tan(_Angle / 2 * deg2rad))
						discard;
				}
				return result;
			}

		ENDCG
		}
	}
	FallBack "Diffuse"
}
