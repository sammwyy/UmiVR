using UnityEngine;

public interface Model
{
    public ModelConfig GetConfig();
    public string GetPath();
    public GameObject Spawn();
}
