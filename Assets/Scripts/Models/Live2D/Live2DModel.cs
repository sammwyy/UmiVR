using System;
using System.IO;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Json;
using UnityEngine;

public class Live2DModel : Model
{
    private string _directory;
    private ModelConfig _config;

    public Live2DModel(string directory, ModelConfig config)
    {
        this._directory = directory;
        this._config = config;
    }

    public ModelConfig GetConfig()
    {
        return this._config;
    }

    static object BuiltinLoadAssetAtPath(Type assetType, string absolutePath)
    {
        if (assetType == typeof(byte[]))
        {
            return File.ReadAllBytes(absolutePath);
        }
        else if (assetType == typeof(string))
        {
            return File.ReadAllText(absolutePath);
        }
        else if (assetType == typeof(Texture2D))
        {
            var texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(absolutePath));

            return texture;
        }

        throw new NotSupportedException();
    }

    public GameObject Spawn()
    {
        string path = Path.Combine(this._directory, this._config.Files.Model);

        CubismModel3Json model3Json = CubismModel3Json.LoadAtPath(path, BuiltinLoadAssetAtPath);
        CubismModel model = model3Json.ToModel();
        return model.gameObject;
    }
}