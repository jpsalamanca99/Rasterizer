class FolderCleaner
{

    public static void CleanFolder(string targetFolder)
    {
        string[] files = Directory.GetFiles(targetFolder);
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }
    }
    
}