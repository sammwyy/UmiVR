using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField]
    private List<Model> models;
    private GameObject currentModel;

    public void LoadModel(Model model)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        this.currentModel = model.Spawn();
    }

    public void LoadModel(int index)
    {
        this.LoadModel(models[index]);
    }

    void Awake()
    {
        ModelLoaderRegistry.Register(new AnimageModelLoader());
        ModelLoaderRegistry.Register(new Live2DModelLoader());
    }

    void Start()
    {
        this.models = new List<Model>();
        this.ScanDirectoryForModels();
        this.LoadModel(0);
    }

    public List<Model> Models
    {
        get
        {
            return models;
        }
    }

    public string GetModelDirectory()
    {
        string directory = Path.Combine(Application.persistentDataPath, "models");
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        return directory;
    }

    public void ScanDirectoryForModels()
    {
        foreach (string directory in Directory.GetDirectories(GetModelDirectory()))
        {
            ModelConfig config = ModelConfigLoader.LoadFromDirectory(directory);

            if (config != null)
            {
                ModelLoader loader = ModelLoaderRegistry.GetLoader(config.Loader);
                if (loader == null)
                {
                    Debug.LogError("[ModelManager] Loader " + config.Loader + " isn't registered. Cannot load model " + directory);
                }
                else
                {
                    Model model = loader.LoadFromDirectory(directory, config);
                    this.models.Add(model);

                    Debug.Log("[ModelManager] Loaded " + loader.GetName() + " model from " + directory);
                }
            }
            else
            {
                Debug.LogWarning("[ModelManager] No *.umi.json file found on directory " + directory);
            }
        }
    }
}
