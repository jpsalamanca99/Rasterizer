public class float3(float x, float y, float z)
{
    public float x = x;
    public float y = y;
    public float z = z;

    public float r { get => x; set => x = value; }
    public float g { get => y; set => y = value; }
    public float b { get => z; set => z = value; }

    public void SetX(float val)
    {
        x = val;
    }

    public override string ToString()
    {
        return "x: " + x + " y: " + y + " z: " + z;
    }
}