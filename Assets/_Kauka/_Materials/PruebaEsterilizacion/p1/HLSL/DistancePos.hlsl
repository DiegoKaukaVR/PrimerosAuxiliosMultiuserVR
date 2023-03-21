void DistancePos_half(in float3 playerPos, in float3 worldPos, in float radius, in float3 primaryTexture, in float3 secondaryTexture, in float grosorExterior, out float3 Out, out float Alpha, out float dissolveMul)
{
    if(dissolveMul == -1){

    }
    else{
        if(distance(playerPos, worldPos) > radius)
        {
            Out = primaryTexture;
            Alpha = 1;
            //dissolveMul = lerp();
            dissolveMul = -1;
        }
        else if(distance(playerPos.xyz, worldPos.xyz)> radius - grosorExterior)
        {
            //Out = float4(1,1,1,1);
            Out = primaryTexture;
            Alpha = 1;
            dissolveMul = -1;
        }
        else
        {
           Out = primaryTexture;
           Alpha = 1;
        
           dissolveMul = lerp(4,-1, distance(playerPos, worldPos)/radius);
        }

    }
}