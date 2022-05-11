/*Please do support www.bitshiftprogrammer.com by joining the facebook page : fb.com/BitshiftProgrammer
Legal Stuff:
This code is free to use no restrictions but attribution would be appreciated.
Any damage caused either partly or completly due to usage this stuff is not my responsibility*/
Shader "BitshiftProgrammer/ReflectionProbeAccess"
{
	Properties
	{
		_Roughness("Roughness", Range(0.0, 10.0)) = 0.0
	}

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

			float _Roughness;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

            struct v2f 
			{
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float4 pos : SV_POSITION;
				float3 ref : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                
				fixed3 normal_w = UnityObjectToWorldNormal(v.normal);
				fixed3 view_w = -WorldSpaceViewDir(v.vertex);
				o.ref = reflect(view_w, normal_w);

                return o;
            }
        
            fixed4 frag (v2f i) : SV_Target
            {
                half4 skyData = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, i.ref, _Roughness);
                half3 skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR); // This is done becasue the cubemap is stored HDR
                return half4(skyColor, 1.0);
            }
            ENDCG
        }
    }
}