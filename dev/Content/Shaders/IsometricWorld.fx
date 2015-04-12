float4x4 ProjectionMatrix;
float4x4 WorldMatrix;
float2	 Viewport;

float3 lightDirection;
float lightIntensity;

bool DrawLighting;

sampler textureSampler;
sampler hueSampler0;
sampler hueSampler1;

const float HuesPerTexture = 2048;
const float ToGrayScale = 3; // (3.0f * HuesPerRow)

struct VS_INPUT
{
	float4 Position : POSITION0;
	float3 Normal	: NORMAL0;
	float3 TexCoord : TEXCOORD0;
	float2 Hue		: TEXCOORD1;
};

struct PS_INPUT
{
	float4 Position : POSITION0;
	float3 TexCoord : TEXCOORD0;
	float3 Normal	: TEXCOORD1;
	float2 Hue		: TEXCOORD2;
};

PS_INPUT VertexShaderFunction(VS_INPUT IN)
{
	PS_INPUT OUT;
	
	OUT.Position = mul(mul(IN.Position, WorldMatrix), ProjectionMatrix);
	
	// Half pixel offset for correct texel centering.
	OUT.Position.x -= 0.5 / Viewport.x;
	OUT.Position.y += 0.5 / Viewport.y;

	OUT.TexCoord = IN.TexCoord; 
	OUT.Normal = IN.Normal;
	OUT.Hue = IN.Hue;
	
    return OUT;
}

float4 PixelShaderFunction(PS_INPUT IN) : COLOR0
{	
	// Get the initial pixel and discard it if the alpha == 0
	float4 color = tex2D(textureSampler, IN.TexCoord);

	if (color.a == 0)
	{
		discard;
	}

	// Hue the color if the hue vector y component is greater than 0.
	if (IN.Hue.y > 0)
	{
		// Hue.Y is a bit flag:
		// 0x01 = completely hued 
		// 0x02 = partially hued
		// 0x04 = 50% transparent.
		// 0x01 & 0x02 should be mutually exclusive.

		float inHueIndex = ((color.r + color.g + color.b) / 3.0f);
		float4 hueColor;
		if (IN.Hue.x >= HuesPerTexture)
		{
			hueColor = tex2D(hueSampler1, float2(inHueIndex, (IN.Hue.x - HuesPerTexture) / HuesPerTexture));
		}
		else
		{
			hueColor = tex2D(hueSampler0, float2(inHueIndex, IN.Hue.x / HuesPerTexture));
		}
		hueColor.a = color.a;

		if (IN.Hue.y >= 4) 
		{
			// 50% transparent
			IN.Hue.y %= 4;
			color *= 0.5f;
		}

		if (IN.Hue.y >= 2) 
		{
			// partial hue - map any grayscale pixels to the hue. Colored pixels remain colored.
			if ((color.r == color.g) && (color.r == color.b))
				color = hueColor;
		}
		else if (IN.Hue.y >= 1) 
		{
			// normal hue - map the hue to the grayscale.
			color = hueColor;
		}
	}

	// Darken the color based on the ambient lighting and the normal.
	if (DrawLighting)
	{
		float3 light = normalize(lightDirection);
		float3 normal = normalize(IN.Normal);
		float3 nDotL = min(saturate(dot(light, normal)), 1.0f);

		color.rgb = saturate((color.rgb * nDotL * lightIntensity * 0.2f + color.rgb * lightIntensity * 0.8f));
	}

	return color;
}


technique StandardEffect
{
	pass p0
	{
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}