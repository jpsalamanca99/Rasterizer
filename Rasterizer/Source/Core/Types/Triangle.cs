class Triangle
{
    public float3 pointA;
    public float3 pointB;
    public float3 pointC;
    public float3 color;
    public float3 speed;
    private bool xSpeedChange;
    private bool ySpeedChange;

    public Triangle(float3 pointA, float3 pointB, float3 pointC)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.pointC = pointC;
        Random rnd = new Random();
        color = new(
            rnd.Next(1, 256),
            rnd.Next(1, 256),
            rnd.Next(1, 256)
        );
        speed = new(rnd.Next(0, 10), rnd.Next(0, 10), 0);
    }

    public Triangle(float3 pointA, float3 pointB, float3 pointC, float3 color)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.pointC = pointC;
        this.color = color;
        Random rnd = new Random();
        speed = new(rnd.Next(0, 10), rnd.Next(0, 10), 0);
    }

    public static Triangle CreateRandom(int maxX, int MaxY)
    {
        Random rnd = new Random();
        Triangle triangle = new Triangle(
            new(rnd.Next(maxX + 1), rnd.Next(MaxY + 1), 0),
            new(rnd.Next(maxX + 1), rnd.Next(MaxY + 1), 0),
            new(rnd.Next(maxX + 1), rnd.Next(MaxY + 1), 0)
        );
        return triangle;
    }

    public float3[] GetAsVectors()
    {
        float3[] vectors =
        [
            new float3(pointA.x - pointB.x, pointA.y - pointB.y, 0),
            new float3(pointB.x - pointC.x, pointB.y - pointC.y, 0),
            new float3(pointC.x - pointA.x, pointC.y - pointA.y, 0),
        ];
        return vectors;
    }

    public float3[] GetPointVectors(float3 pointP)
    {
        float3[] vectors =
        [
            new float3(pointA.x - pointP.x, pointA.y - pointP.y, 0),
            new float3(pointB.x - pointP.x, pointB.y - pointP.y, 0),
            new float3(pointC.x - pointP.x, pointC.y - pointP.y, 0),
        ];
        return vectors;
    }

    public Triangle ToVirtualSpace(int scaleFactor, Transform transform)
    {
        return new Triangle(
            VectorMath.AddVectors(VectorMath.EscaleVector(pointA, scaleFactor), transform.Position),
            VectorMath.AddVectors(VectorMath.EscaleVector(pointB, scaleFactor), transform.Position),
            VectorMath.AddVectors(VectorMath.EscaleVector(pointC, scaleFactor), transform.Position),
            color
        );
    }

    public void MoveBySpeed(int xBounds, int yBounds)
    {
        float3 aDisplacement = GetPointDisplacement(pointA, xBounds, yBounds);
        float3 bDisplacement = GetPointDisplacement(pointB, xBounds, yBounds);
        float3 cDisplacement = GetPointDisplacement(pointC, xBounds, yBounds);

        pointA.x += Math.Min(Math.Min(aDisplacement.x, bDisplacement.x), cDisplacement.x);
        pointA.y += Math.Min(Math.Min(aDisplacement.y, bDisplacement.y), cDisplacement.y);
        pointB.x += Math.Min(Math.Min(aDisplacement.x, bDisplacement.x), cDisplacement.x);
        pointB.y += Math.Min(Math.Min(aDisplacement.y, bDisplacement.y), cDisplacement.y);
        pointC.x += Math.Min(Math.Min(aDisplacement.x, bDisplacement.x), cDisplacement.x);
        pointC.y += Math.Min(Math.Min(aDisplacement.y, bDisplacement.y), cDisplacement.y);
        if (xSpeedChange)
        {
            speed.x *= -1;
            xSpeedChange = false;
        }
        if (ySpeedChange)
        {
            speed.y *= -1;
            ySpeedChange = false;
        }
    }

    private float3 GetPointDisplacement(float3 point, int xBounds, int yBounds)
    {
        float3 displacement = new(speed.x, speed.y, 0);
        if (speed.x > 0)
        {
            if (point.x + speed.x >= xBounds)
            {
                displacement.x = point.x - xBounds;
                xSpeedChange = true;
            }
        }
        else
        {
            if (point.x + speed.x <= 0)
            {
                displacement.x = point.x;
                xSpeedChange = true;
            }
        }
        if (speed.y > 0)
        {
            if (point.y + speed.y >= yBounds)
            {
                displacement.y = point.y - yBounds;
                ySpeedChange = true;
            }
        }
        else
        {
            if (point.y + speed.y <= 0)
            {
                displacement.y = point.y;
                ySpeedChange = true;
            }
        }

        return displacement;
    }

    public override string ToString()
    {
        return string.Format("A(x: {0} y: {1} z: {2} ), B(x: {3} y: {4} z: {5} ), C(x: {6} y: {7} z: {8} )",
            pointA.x, pointA.y, pointA.z,
            pointB.x, pointB.y, pointB.z,
            pointC.x, pointC.y, pointC.z
        );
    }
}
