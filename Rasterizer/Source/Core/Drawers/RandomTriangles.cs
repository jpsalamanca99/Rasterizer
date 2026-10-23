using System.Diagnostics;

class RandomTriangles
{
    public int width;
    public int height;
    public int numberOftriangles;
    public Triangle[] triangles;

    public RandomTriangles(int width, int height, int numberOftriangles)
    {
        this.width = width;
        this.height = height;
        this.numberOftriangles = numberOftriangles;
        triangles = new Triangle[numberOftriangles];
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = Triangle.CreateRandom(width / 5, height / 5);
        }
    }

    //Draws random triangles in the hole image, the trinagles have a speed and bum in the corners
    public float3[,] DrawTriangles()
    {
        float3[,] image = new float3[width, height];

        for (int i = 0; i < numberOftriangles; i++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float3[] triangleVectors = GetTriangleVectors(triangles[i].pointA, triangles[i].pointB, triangles[i].pointC);
                    float3[] pointVectors = GetPointVectors(triangles[i].pointA, triangles[i].pointB, triangles[i].pointC, new float3(x, y, 0));
                    float3 ABxAP = VectorMath.CalculateCrossProduct(triangleVectors[0], pointVectors[0]);
                    float3 BCxBP = VectorMath.CalculateCrossProduct(triangleVectors[1], pointVectors[1]);
                    float3 CAxCP = VectorMath.CalculateCrossProduct(triangleVectors[2], pointVectors[2]);
                    bool inside = Math.Sign(ABxAP.z) == Math.Sign(BCxBP.z) && Math.Sign(ABxAP.z) == Math.Sign(CAxCP.z);
                    if (inside)
                    {
                        image[x, y] = triangles[i].color;
                    }
                }
            }
        }
        return image;
    }

    //Uses the speeds of the triengles to move them
    public void AdvanceTriangles()
    {
        for (int i = 0; i < numberOftriangles; i++)
        {
            triangles[i].MoveBySpeed(width, height);
        }
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