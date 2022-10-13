using UnityEngine;

public interface Model
{
    public ModelConfig GetConfig();
    public GameObject Spawn();
}
