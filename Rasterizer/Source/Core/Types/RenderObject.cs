class RenderObject(List<float3> vertexes, List<Triangle> faces, float3 position)
{
    private float3[] vertexes = [.. vertexes];
    private Triangle[] faces = [.. faces];
    private Transform transform = new(position);



    public void UpdateObjectPosition(float3 translation)
    {
        transform.Position = translation;
    }

    public void UpdateObjectRotaion(float3 rotationDegrees)
    {
        transform.Rotation = rotationDegrees;
    }

    public void TranslateObject(float3 translation)
    {
        transform.Move(translation);
        // for (int i = 0; i < vertexes.Length; i++)
        // {
        //     float3 vertex = vertexes[i];
        //     float3 sum = VectorMath.AddVectors(vertex, transform.Position);
        //     vertex.x = sum.x;
        //     vertex.y = sum.y;
        //     vertex.z = sum.z;
        //     vertexes[i] = vertex;
        // }
    }

    public void RotateObject(float3 rotation)
    {
        transform.Rotate(rotation);
        // for (int i = 0; i < vertexes.Length; i++)
        // {
        //     float3 vertex = vertexes[i];
        //     (float3 ihat, float3 jhat, float3 khat) = transform.GetUnitVectors();
        //     float3 scaledIHat = VectorMath.EscaleVector(ihat, vertexes[i].x);
        //     float3 scaledJHat = VectorMath.EscaleVector(jhat, vertexes[i].y);
        //     float3 scaledKHat = VectorMath.EscaleVector(khat, vertexes[i].z);
        //     float3 sum = VectorMath.AddVectors(VectorMath.AddVectors(scaledIHat, scaledKHat), scaledJHat);
        //     vertex.x = sum.x;
        //     vertex.y = sum.y;
        //     vertex.z = sum.z;
        //     vertexes[i] = vertex;
        // }
    }

    public List<Triangle> GetFacesInVirtualSpace()
    {
        List<Triangle> transformedFaces = new List<Triangle>();
        (float3 ihat, float3 jhat, float3 khat) = transform.GetUnitVectors();
        for (int i = 0; i < faces.Length; i++)
        {
            transformedFaces.Add(new Triangle(
                TransformVertexToVirtualSpace(faces[i].pointA, ihat, jhat, khat),
                TransformVertexToVirtualSpace(faces[i].pointB, ihat, jhat, khat),
                TransformVertexToVirtualSpace(faces[i].pointC, ihat, jhat, khat),
                faces[i].color
            ));
        }

        return transformedFaces;
    }

    private float3 TransformVertexToVirtualSpace(float3 vertex, float3 ihat, float3 jhat, float3 khat)
    {
        float3 transformedVertex = new(0, 0, 0);
        float3 scaledIHat = VectorMath.EscaleVector(ihat, vertex.x);
        float3 scaledJHat = VectorMath.EscaleVector(jhat, vertex.y);
        float3 scaledKHat = VectorMath.EscaleVector(khat, vertex.z);
        float3 sum = VectorMath.AddVectors(VectorMath.AddVectors(scaledIHat, scaledKHat), scaledJHat);
        transformedVertex.x = sum.x;
        transformedVertex.y = sum.y;
        transformedVertex.z = sum.z;

        transformedVertex = VectorMath.AddVectors(transformedVertex, transform.Position);

        return transformedVertex;
    }

    public float3 GetPosition()
    {
        return transform.Position;
    }


}