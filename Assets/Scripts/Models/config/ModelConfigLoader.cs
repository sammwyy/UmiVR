using System.IO;
using Newtonsoft.Json;

public class ModelConfigLoader
{
    public static ModelConfig LoadFromJson(string json)
    {
        return JsonConvert.DeserializeObject<ModelConfig>(json);
    }

    public static ModelConfig LoadFromFile(string file)
    {
        if (!File.Exists(file))
        {
            return null;
        }

        string json = File.ReadAllText(file);
        return LoadFromJson(json);
    }

    public static ModelConfig LoadFromDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path, "*.umi.json"))
            {
                return LoadFromFile(file);
            }
        }

        return null;
    }
}