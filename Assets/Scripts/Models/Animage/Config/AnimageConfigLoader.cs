using System.IO;
using Newtonsoft.Json;

public class AnimageConfigLoader
{
    public static AnimageConfig LoadFromJson(string json)
    {
        return JsonConvert.DeserializeObject<AnimageConfig>(json);
    }

    public static AnimageConfig LoadFromFile(string file)
    {
        if (!File.Exists(file))
        {
            return null;
        }

        string json = File.ReadAllText(file);
        return LoadFromJson(json);
    }
}