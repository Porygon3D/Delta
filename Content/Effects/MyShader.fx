//content
// This File Declares parameteres TextureSample and WorldViewProj 
//Defines a vertex shader (VS) and Pixel shader (PS)
//Bundles them into a technique named basic


//Texture sampler bound to slot 0
sampler2D TextureSampler : register(s0);

//worldview-projection matrix provided by game code
float4x4 WorldViewProj;

//vertex input structure
struct VertexIput
{
    float4 position : SV_Position;
    float2 TexCoord : TEXCOORD0;
};

//Vertex output passed to pixel shader
struct PixelInput
{
    float4 Position : SV_Position;
    float2 TexCoord : TEXCOORD0;
};
//Transforms vertices by worldViewProjection
PixelInput VS(VertexIput input)
{
    PixelInput output;
    output.Position = mul(input.position, WorldViewProj);
    output.TexCoord = input.TexCoord;
    return output;
}

//Sample the texture and return its color
float4 PS(PixelInput input) : SV_Target
{
    return tex2D(TextureSampler, input.TexCoord);

}

//Single pass technique using shader model 4.1
technique Basic
{
    pass P0
    {
        VertexShader = compile vs_4_1 VS();
        PixelShader = compile ps_4_1 PS();
    }
}
