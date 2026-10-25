class ObjParser
{
    public static RenderObject ToRenderObject(string objFilePath)
    {
        List<float3> vertexes = new List<float3>();
        List<Triangle> faces = new List<Triangle>();

        var lines = File.ReadAllLines(objFilePath);
        for (var i = 0; i < lines.Length; i += 1)
        {
            string[] lineInfo = lines[i].Split(" ");
            if (lineInfo[0].CompareTo("v") == 0)
            {
                vertexes.Add(ReadVertex(lineInfo));
            }
            if (lineInfo[0].CompareTo("f") == 0)
            {
                ReadFaces(lineInfo, faces, vertexes);
            }
        }
        RenderObject obj = new RenderObject(vertexes, faces, new(0f, 0f, 0f));

        return obj;
    }

    private static float3 ReadVertex(string[] lineInfo)
    {
        return new float3(
            float.Parse(lineInfo[1]),
            float.Parse(lineInfo[2]),
            float.Parse(lineInfo[3]));
    }

    private static void ReadFaces(string[] lineInfo, List<Triangle> faces, List<float3> vertexes)
    {
        int firstVertexIndex = int.Parse(lineInfo[1].Split("/")[0]) - 1;
        int lastAddedVertexIndex = int.Parse(lineInfo[2].Split("/")[0]) - 1;
        for (int i = 3; i < lineInfo.Length; i++)
        {
            int thirdVertexIndex = int.Parse(lineInfo[i].Split("/")[0]) - 1;
            faces.Add(
                new Triangle(
                    vertexes[firstVertexIndex],
                    vertexes[lastAddedVertexIndex],
                    vertexes[thirdVertexIndex]
                )
            );
            lastAddedVertexIndex = thirdVertexIndex;
        }
    }

}