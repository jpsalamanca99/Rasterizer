using System.Dynamic;

class Camera(float3 position, float fieldOfView, float2 resolution)
{
    private float3 position = position;
    private float fieldOfView = fieldOfView;
    private float2 resolution = resolution;

    public float3 Position { get => position; }
    public float FOV { get => fieldOfView; }

    public List<Triangle> ProjectObjectToPlane(RenderObject obj)
    {
        List<Triangle> virtualSpaceFaces = obj.GetFacesInVirtualSpace();
        List<Triangle> projectedFaces = new List<Triangle>();
        for (int i = 0; i < virtualSpaceFaces.Count; i++)
        {
            projectedFaces.Add(new Triangle(
                VertexToCameraProjection(virtualSpaceFaces[i].pointA),
                VertexToCameraProjection(virtualSpaceFaces[i].pointB),
                VertexToCameraProjection(virtualSpaceFaces[i].pointC),
                virtualSpaceFaces[i].color
            ));
        }
 
        return projectedFaces;
    }

    public float3 VertexToCameraProjection(float3 vertex)
    {
        float perspectiveScale = (float)Math.Tan(float.DegreesToRadians(fieldOfView / 2f)) * 2f;
        float projectionToVirtualSpaceRatio = 5;
        float pixelsPerVirtualSpaceUnit = resolution.y / projectionToVirtualSpaceRatio / vertex.z;

        float3 pixelOffset = new float3(vertex.x * pixelsPerVirtualSpaceUnit, vertex.y * pixelsPerVirtualSpaceUnit, 0);
        return new((resolution.x / 2) + pixelOffset.x, (resolution.y / 2) + pixelOffset.y, 0);
    }

}