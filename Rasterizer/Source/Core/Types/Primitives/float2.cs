public struct float2(float x, float y)
{
    public float x = x;
    public float y = y;

    public float r { get => x; set => x = value; }
    public float g { get => y; set => y = value; }
}