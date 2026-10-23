class TestImage
{
    // Iterates over the image matrix assiggning colors to every pixel depending the distance from the origin
    public static float3[,] DrawImage(int width, int height)
    {
        float3[,] image = new float3[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float r = x / (width - 1f);
                float g = y / (height - 1f);
                image[x, y] = new float3(r, g, 0);
            }
        }

        return image;
    }
}