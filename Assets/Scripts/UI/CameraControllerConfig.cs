using Newtonsoft.Json;

public class CameraControllerConfig
{
    public float x = 0;
    public float y = 0;
    public float zoom = 4f;

    public void Save(string file)
    {
        string raw = JsonConvert.SerializeObject(this);
        DirectoryUtils.WriteFile(file, raw);
    }

    public void Save()
    {
        this.Save(DirectoryUtils.GetResource("Camera.json"));
    }

    public static CameraControllerConfig LoadFromJson(string json)
    {
        if (json == null)
        {
            return new CameraControllerConfig();
        }

        return JsonConvert.DeserializeObject<CameraControllerConfig>(json);
    }

    public static CameraControllerConfig Load()
    {
        return LoadFromJson(DirectoryUtils.ReadFile("Camera.json"));
    }
}
