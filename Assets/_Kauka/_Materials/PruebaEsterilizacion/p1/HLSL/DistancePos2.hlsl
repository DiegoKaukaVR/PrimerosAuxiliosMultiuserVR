void DistancePos_half(in float3 playerPos, in float3 worldPos, in float radius, in float3 primaryTexture, in float3 secondaryTexture, in float grosorExterior, out float3 Out, out float Alpha)
{
    if(distance(playerPos.x, worldPos.x) > radius && distance(playerPos.z,worldPos.z) > radius)
    {
         Out = secondaryTexture;
        Alpha = 0;
    }
    else if(distance(playerPos.xz, worldPos.xz)> radius - grosorExterior)
    {
        //Out = float4(1,1,1,1);
        Out = secondaryTexture;
        Alpha = 0;
    }
    else
    {
        Out = primaryTexture;
        Alpha = 1;
    }
}