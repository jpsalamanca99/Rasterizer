class RenderObject
{
    private List<float3> vertexes;
    private List<Triangle> faces;
    private Transform transform;

    public RenderObject()
    {
        vertexes = [];
        faces = [];
        transform = new(new float3(0, 0, 0));
    }

    public void AddVertex(float3 vertex)
    {
        vertexes.Add(vertex);
    }

    public void AddFace(int vertexAIndex, int vertexBIndex, int vertexCIndex)
    {
        faces.Add(new Triangle(
            vertexes[vertexAIndex],
            vertexes[vertexBIndex],
            vertexes[vertexCIndex]
        ));
    }

    public void RotateObject(float3 rotation)
    {
        transform.Rotate(rotation);
        for (int i = 0; i < vertexes.Count; i++)
        {
            float3 vertex = vertexes[i];
            float3 scaledVectorX = VectorMath.EscaleVector(transform.UnitVectorX, vertex.x);
            float3 scaledVectorY = VectorMath.EscaleVector(transform.UnitVectorY, vertex.y);
            float3 scaledVectorZ = VectorMath.EscaleVector(transform.UnitVectorZ, vertex.z);
            float3 sum = VectorMath.AddVectors(VectorMath.AddVectors(scaledVectorX, scaledVectorZ), scaledVectorY);
            vertex.x = sum.x;
            vertex.y = sum.y;
            vertex.z = sum.z;
            vertexes[i] = vertex;
        }
    }

    public List<Triangle> GetFacesInVirtualSpace(float pixelsTo3DUnitsProportion)
    {
        List<Triangle> transformedFaces = new List<Triangle>();
        for (int i = 0; i < faces.Count; i++)
        {
            transformedFaces.Add(new Triangle(
                VectorMath.EscaleVector(faces[i].pointA, pixelsTo3DUnitsProportion),
                VectorMath.EscaleVector(faces[i].pointB, pixelsTo3DUnitsProportion),
                VectorMath.EscaleVector(faces[i].pointC, pixelsTo3DUnitsProportion),
                faces[i].color
            ));
        }

        return transformedFaces;
    }

    public float3 GetPosition()
    {
        return transform.Position;
    }

    public void SetPosition(float3 newPosition)
    {
        transform.Position = newPosition;
    }

}