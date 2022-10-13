public interface ModelLoader
{
    public string GetName();
    public Model LoadFromDirectory(string path, ModelConfig config);
}
