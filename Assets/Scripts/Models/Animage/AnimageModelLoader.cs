using System.IO;

public class AnimageModelLoader : ModelLoader
{
    public string GetName()
    {
        return "Animage";
    }

    public Model LoadFromDirectory(string path, ModelConfig config)
    {
        AnimageConfig animageConfig = AnimageConfigLoader.LoadFromFile(Path.Combine(path, config.Files.Model));
        return new AnimageModel(path, config, animageConfig.sprites);
    }
}
