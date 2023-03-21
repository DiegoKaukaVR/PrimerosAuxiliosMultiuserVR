void CorteTransversalPos_half( in float3 objectPos, in float3 primaryTexture, in float3 secondaryTexture, out float3 Out, out float Alpha)
{
    if(objectPos.x < .001 && objectPos.x > 0)
    {
        Out = secondaryTexture;
        Alpha = 1;
        //float3 ray_origin = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1));
        //float3 ray_direction = normalize(objectPos.x, ray_origin);
        //float t = sphereCasting(ray_origin, ray_direction);
        //float3 p = ray_origin + ray_direction * t;
        //float2 uv_p = p.xz;
        //float l = pow(-abs(0), 2) + pow(-abs(0) - 1,2);
        //planeCol = tex2D(secondaryTexture, (uv_p(1-abs(pow(0 * l, 2))))-0.5);
    }
    else if(objectPos.x < 0)
    {
        Alpha = 0;
    }
    else 
    {
        Out = primaryTexture;
        Alpha = 1;
    }
}