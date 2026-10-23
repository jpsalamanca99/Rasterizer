using System.IO;

class FileExtensionChanger
{
    public static void ChangeFilesExtensionInFolder(string targetFolder, string newExtension)
    {
        string[] files = Directory.GetFiles(targetFolder);
        for (int i = 0; i < files.Length; i++)
        {
            File.Move(files[i], Path.ChangeExtension(files[i], newExtension));
        }
    }

}