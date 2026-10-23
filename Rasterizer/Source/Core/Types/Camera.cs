using System.Dynamic;

class Camera(float3 position, float fieldOfView, float pixelsTo3DUnitsProportion, float2 resolution)
{
    private float3 position = position;
    private float fieldOfView = fieldOfView;
    private float pixelsTo3DUnitsProportion = pixelsTo3DUnitsProportion;
    private float2 resolution = resolution;

    public float3 Position { get => position; }
    public float FOV { get => fieldOfView; }
    public float ScaleProportion { get => pixelsTo3DUnitsProportion; }

    public List<Triangle> ProjectObjectToPlane(RenderObject obj)
    {
        List<Triangle> virtualSpaceFaces = obj.GetFacesInVirtualSpace(pixelsTo3DUnitsProportion);
        List<Triangle> projectedFaces = new List<Triangle>();
        float perspectiveScale = (float)Math.Tan(float.DegreesToRadians(fieldOfView / 2f)) * 2f;

        for (int i = 0; i < virtualSpaceFaces.Count; i++)
        {
            projectedFaces.Add(new Triangle(
                VectorMath.AddVectors(VectorMath.EscaleVector(virtualSpaceFaces[i].pointA, resolution.y / perspectiveScale / virtualSpaceFaces[i].pointA.z), obj.GetPosition()),
                VectorMath.AddVectors(VectorMath.EscaleVector(virtualSpaceFaces[i].pointB, resolution.y / perspectiveScale / virtualSpaceFaces[i].pointB.z), obj.GetPosition()),
                VectorMath.AddVectors(VectorMath.EscaleVector(virtualSpaceFaces[i].pointC, resolution.y / perspectiveScale / virtualSpaceFaces[i].pointC.z), obj.GetPosition()),
                virtualSpaceFaces[i].color
            ));
        }

        return projectedFaces;
    }

}