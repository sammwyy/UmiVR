using System.IO;
using UnityEngine;

public class DirectoryUtils
{
    public static string GetAppDirectory()
    {
        return Application.persistentDataPath;
    }

    public static string GetResource(string child)
    {
        string directory = Path.Combine(GetAppDirectory(), child);
        return directory;
    }

    public static string ReadFile(string filename)
    {
        string path = GetResource(filename);
        if (!File.Exists(path))
        {
            return null;
        }
        return File.ReadAllText(path);
    }

    public static void WriteFile(string filename, string content)
    {
        string path = GetResource(filename);
        File.WriteAllText(path, content);
    }
}