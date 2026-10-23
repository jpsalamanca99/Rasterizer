class VectorMath
{
    public static float3 EscaleVector(float3 vector, float scalar)
    {
        return new float3(
            vector.x * scalar,
            vector.y * scalar,
            vector.z * scalar
        );
    }

    public static float3 AddVectors(float3 a, float3 b)
    {
        return new float3(
            a.x + b.x,
            a.y + b.y,
            a.z + b.z
        );
    }

    public static float3 CalculateCrossProduct(float3 a, float3 b)
    {
        float x = (a.y * b.z) - (a.z * b.y);
        float y = (a.z * b.x) - (a.x * b.z);
        float z = (a.x * b.y) - (a.y * b.x);
        return new float3(x, y, z);
    }
    
}