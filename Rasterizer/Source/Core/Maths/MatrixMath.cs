class MatrixMath
{
    // Converts a set of 3 vectors to a 3x3 matrix
    public static float[,] VectorArrayToMatrix(float3[] a)
    {
        float[,] result = new float[3, 3];
        for (int i = 0; i < a.Length; i++)
        {
            result[i, 0] = a[i].x;
            result[i, 1] = a[i].y;
            result[i, 2] = a[i].z;
        }
        return result;
    }
    // Multiplies two 3x3 matrixes
    public static float[,] Multiply(float[,] a, float[,] b)
    {
        float[,] result = new float[3, 3];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(0); j++)
            {
                result[i, j] = (a[i, 0] * b[0, j]) + (a[i, 1] * b[1, j]) + (a[i, 2] * b[2, j]);
            }
        }

        return result;
    }

    // Converts a 3x3 matrix to a set of 3 vectors
    public static float3[] MatrixToVectorArray(float[,] a)
    {
        return [
            new float3(a[0,0], a[0,1], a[0,2]),
            new float3(a[1,0], a[1,1], a[1,2]),
            new float3(a[2,0], a[2,1], a[2,2])
        ];
    }
}
