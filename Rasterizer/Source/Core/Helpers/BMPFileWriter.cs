class BMPFileWriter
{
    public static void BuildSingleFile(float3[,] image, string targetFolderPath, string targetFilePath = "img.txt", int frame = 1)
    {
        using BinaryWriter writer = new(File.Open(targetFolderPath + targetFilePath, FileMode.Create));
        BMPFileBuilder.WriteHeaders(writer, image);
        BMPFileBuilder.WriteData(writer, image);
        
    }
}