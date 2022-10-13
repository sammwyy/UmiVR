using System.IO;

public class Live2DModelLoader : ModelLoader
{
    public string GetName()
    {
        return "Live2D";
    }

    public Model LoadFromDirectory(string path, ModelConfig config)
    {
        Live2DModel model = new Live2DModel(path, config);
        return model;
    }
}