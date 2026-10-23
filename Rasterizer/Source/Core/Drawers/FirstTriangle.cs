using System.Diagnostics;

class FirstTriangle
{
    //Draws a random triangle given its vertexes in a 2D plane
    public static float3[,] DrawTriangle(int width, int height)
    {
        float3[,] image = new float3[width, height];

        Random rnd = new();
        float3 pointA = new(rnd.Next(width + 1), rnd.Next(height + 1), 0);
        float3 pointB = new(rnd.Next(width + 1), rnd.Next(height + 1), 0);
        float3 pointC = new(rnd.Next(width + 1), rnd.Next(height + 1), 0);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float3[] triangleVectors = GetTriangleVectors(pointA, pointB, pointC);
                float3[] pointVectors = GetPointVectors(pointA, pointB, pointC, new float3(x, y, 0));
                float3 ABxAP = VectorMath.CalculateCrossProduct(triangleVectors[0], pointVectors[0]);
                float3 BCxBP = VectorMath.CalculateCrossProduct(triangleVectors[1], pointVectors[1]);
                float3 CAxCP = VectorMath.CalculateCrossProduct(triangleVectors[2], pointVectors[2]);
                bool inside = Math.Sign(ABxAP.z) == Math.Sign(BCxBP.z) && Math.Sign(ABxAP.z) == Math.Sign(CAxCP.z);
                if (inside)
                {
                    image[x, y] = new(1, 1, 1);
                }
                else
                {
                    image[x, y] = new(0, 0, 0);

                }
            }
        }
        return image;
    }

    private static float3[] GetTriangleVectors(float3 pointA, float3 pointB, float3 pointC)
    {
        float3[] vectors =
        [
            new float3(pointA.x - pointB.x, pointA.y - pointB.y, 0),
            new float3(pointB.x - pointC.x, pointB.y - pointC.y, 0),
            new float3(pointC.x - pointA.x, pointC.y - pointA.y, 0),
        ];
        return vectors;
    }

    private static float3[] GetPointVectors(float3 pointA, float3 pointB, float3 pointC, float3 pointP)
    {
        float3[] vectors =
        [
            new float3(pointA.x - pointP.x, pointA.y - pointP.y, 0),
            new float3(pointB.x - pointP.x, pointB.y - pointP.y, 0),
            new float3(pointC.x - pointP.x, pointC.y - pointP.y, 0),
        ];
        return vectors;
    }
}