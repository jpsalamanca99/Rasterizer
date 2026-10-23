class PerspectiveCubeDrawer
{
    public int width;
    public int height;
    public RenderObject cube;

    public PerspectiveCubeDrawer(int width, int height, RenderObject cube)
    {
        this.width = width;
        this.height = height;
        this.cube = cube;
    }

    public float3[,] DrawCube(Camera camera)
    {
        float3[,] image = new float3[width, height];
        List<Triangle> faces = camera.ProjectObjectToPlane(cube);

        for (int i = 0; i < faces.Count; i++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (CheckPointInFace(faces[i], new float3(x, y, 0)))
                    {
                        image[x, y] = faces[i].color;
                    }
                    else
                    {
                        if (image[x, y] == null) image[x, y] = new float3(0, 0, 0);
                    }
                }
            }
        }
        return image;
    }

    private static bool CheckPointInFace(Triangle face, float3 point)
    {
        float3[] faceAsVectors = face.GetAsVectors();
        float3[] pointVectors = face.GetPointVectors(point);
        float3 ABxAP = VectorMath.CalculateCrossProduct(faceAsVectors[0], pointVectors[0]);
        float3 BCxBP = VectorMath.CalculateCrossProduct(faceAsVectors[1], pointVectors[1]);
        float3 CAxCP = VectorMath.CalculateCrossProduct(faceAsVectors[2], pointVectors[2]);

        if (Math.Sign(ABxAP.z) > 0 && Math.Sign(BCxBP.z) > 0 && Math.Sign(CAxCP.z) > 0) return true;
        return false;
    }
}