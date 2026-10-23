class BMPFileBuilder
{
    public static void WriteHeaders(BinaryWriter writer, float3[,] image)
    {

        uint[] ByteCounts = { 14, 40, (uint)image.Length * 4 }; //Header length, header info length and image size

        //Builds file headers
        writer.Write("BM"u8.ToArray());
        writer.Write(ByteCounts[0] + ByteCounts[1] + ByteCounts[2]); //File size in bytes
        writer.Write((uint)0); //Reserved = 0
        writer.Write(ByteCounts[0] + ByteCounts[1]); //Offset from the beggining of the file to the bitmap data (the image it self)
        writer.Write(ByteCounts[1]); //Reserved info header size = 40
        writer.Write((uint)image.GetLength(0)); //Image width in pixels
        writer.Write((uint)image.GetLength(1)); //Image height in pixels
        writer.Write((ushort)1); //Reserved number of planes = 1
        writer.Write((ushort)(8 * 4)); //Bits per pixel 8 = 256 colors (1 per chanel + 1 for alignment)
        writer.Write((uint)0); //Compresion no compresion = 0
        writer.Write((uint)0); //Compresed image size = 0
        writer.Write(new byte[16]);
    }

    public static void WriteData(BinaryWriter writer, float3[,] image)
    {
        for (int y = 0; y < image.GetLength(0); y++)
        {
            for (int x = 0; x < image.GetLength(1); x++)
            {
                float3 color = image[x, y];
                writer.Write((byte)(color.b * 255));
                writer.Write((byte)(color.g * 255));
                writer.Write((byte)(color.r * 255));
                writer.Write((byte)0);
            }
        }
    }
}