using System.Collections.Generic;

public class ModelLoaderRegistry
{
    private static Dictionary<string, ModelLoader> loaders = new Dictionary<string, ModelLoader>();

    public static void Register(ModelLoader loader)
    {
        loaders.Add(loader.GetName().ToLower(), loader);
    }

    public static ModelLoader GetLoader(string id)
    {
        return loaders.GetValueOrDefault(id.ToLower());
    }
}