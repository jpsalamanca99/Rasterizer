using static System.MathF;

class Transform(float3 virtualSpaceInitialPosition)
{
    private float3 virtualSpacePosition = virtualSpaceInitialPosition;
    private float3 virtualSpaceRotation = new(0f, 0f, 0f);
    private float3 iHat = new(0f, 0f, 0f);
    private float3 jHat = new(0f, 0f, 0f);
    private float3 kHat = new(0f, 0f, 0f);

    public (float3 ihat, float3 jhat, float3 khat) GetUnitVectors()
    {
        float[,] matrixX = MatrixMath.VectorArrayToMatrix(RotateAlongX(virtualSpaceRotation.x));
        float[,] matrixY = MatrixMath.VectorArrayToMatrix(RotateAlongY(virtualSpaceRotation.y));
        float[,] matrixZ = MatrixMath.VectorArrayToMatrix(RotateAlongZ(virtualSpaceRotation.z));
        float3[] newVectors = MatrixMath.MatrixToVectorArray(MatrixMath.Multiply(MatrixMath.Multiply(matrixX, matrixY), matrixZ));
        iHat = newVectors[0];
        jHat = newVectors[1];
        kHat = newVectors[2];

        return (iHat, jHat, kHat);
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

    public void Move(float3 displacement)
    {
        virtualSpacePosition = VectorMath.AddVectors(virtualSpacePosition, displacement);
    }

    public void Rotate(float3 incrementDegree)
    {
        virtualSpaceRotation = VectorMath.AddVectors(virtualSpaceRotation, incrementDegree);
    }

    public float3 Rotation { get => virtualSpaceRotation; set => virtualSpaceRotation = value; }
    public float3 Position { get => virtualSpacePosition; set => virtualSpacePosition = value; }
    public float3 IHat { get => iHat; }
    public float3 JHat { get => jHat; }
    public float3 KHat { get => kHat; }
}