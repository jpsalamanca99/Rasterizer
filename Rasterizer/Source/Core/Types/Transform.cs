using static System.MathF;

class Transform(float3 virtualSpaceInitialPosition)
{
    private float3 virtualSpacePosition = virtualSpaceInitialPosition;
    private float3 ihat = new float3(0, 0, 0);
    private float3 jhat = new float3(0, 0, 0);
    private float3 khat = new float3(0, 0, 0);

    public void Rotate(float3 rotationDegreesComponents)
    {
        float[,] matrixX = MatrixMath.VectorArrayToMatrix(RotateAlongX(rotationDegreesComponents.x));
        float[,] matrixY = MatrixMath.VectorArrayToMatrix(RotateAlongY(rotationDegreesComponents.y));
        float[,] matrixZ = MatrixMath.VectorArrayToMatrix(RotateAlongZ(rotationDegreesComponents.z));
        float3[] newVectors = MatrixMath.MatrixToVectorArray(MatrixMath.Multiply(MatrixMath.Multiply(matrixX, matrixY), matrixZ));
        ihat = newVectors[0];
        jhat = newVectors[1];
        khat = newVectors[2];
    }

    //Rotates the unit vectors X and Z by the given degrees
    private float3[] RotateAlongX(float rotationAngle)
    {
        float3 ihat = new float3(1f, 0f, 0f);
        float3 jhat = new float3(0f, Cos(float.DegreesToRadians(rotationAngle)), -Sin(float.DegreesToRadians(rotationAngle)));
        float3 khat = new float3(0f, Sin(float.DegreesToRadians(rotationAngle)), Cos(float.DegreesToRadians(rotationAngle)));
        return [ihat, jhat, khat];
    }

    private float3[] RotateAlongY(float rotationAngle)
    {
        float3 ihat = new float3(Cos(float.DegreesToRadians(rotationAngle)), 0f, Sin(float.DegreesToRadians(rotationAngle)));
        float3 jhat = new float3(0f, 1f, 0f);
        float3 khat = new float3(-Sin(float.DegreesToRadians(rotationAngle)), 0f, Cos(float.DegreesToRadians(rotationAngle)));
        return [ihat, jhat, khat];
    }

    private float3[] RotateAlongZ(float rotationAngle)
    {
        float3 ihat = new float3(Cos(float.DegreesToRadians(rotationAngle)), Sin(float.DegreesToRadians(rotationAngle)), 0f);
        float3 jhat = new float3(-Sin(float.DegreesToRadians(rotationAngle)), Cos(float.DegreesToRadians(rotationAngle)), 0f);
        float3 khat = new float3(0f, 0f, 1f);
        return [ihat, jhat, khat];
    }

    public float3 Position { get => virtualSpacePosition; set => virtualSpacePosition = value; }
    public float3 UnitVectorX { get => ihat; }
    public float3 UnitVectorY { get => jhat; }
    public float3 UnitVectorZ { get => khat; }
}