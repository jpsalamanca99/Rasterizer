class ObjParser
{
    public static RenderObject ToRenderObject(string objFilePath)
    {
        RenderObject obj = new RenderObject();
        var lines = File.ReadAllLines(objFilePath);
        for (var i = 0; i < lines.Length; i += 1)
        {
            string[] lineInfo = lines[i].Split(" ");
            if (lineInfo[0].CompareTo("v") == 0) ReadVertex(lineInfo, obj);
            if (lineInfo[0].CompareTo("f") == 0) ReadFace(lineInfo, obj);
        }
        return obj;
    }

    private static void ReadVertex(string[] lineInfo, RenderObject obj)
    {
        obj.AddVertex(new float3(
            float.Parse(lineInfo[1]),
            float.Parse(lineInfo[2]),
            float.Parse(lineInfo[3]))
        );
    }

    private static void ReadFace(string[] lineInfo, RenderObject obj)
    {
        int firstVertexIndex = int.Parse(lineInfo[1].Split("/")[0]) - 1;
        int lastAddedVertexIndex = int.Parse(lineInfo[2].Split("/")[0]) - 1;
        for (int i = 3; i < lineInfo.Length; i++)
        {
            int thirdVertexIndex = int.Parse(lineInfo[i].Split("/")[0]) - 1;
            obj.AddFace(firstVertexIndex, lastAddedVertexIndex, thirdVertexIndex);
            lastAddedVertexIndex = thirdVertexIndex;
        }
    }

}