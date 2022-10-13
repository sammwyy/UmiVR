using System.IO;
using UnityEngine;

public class AnimageModel : Model
{
    private string _path;
    private ModelConfig _config;
    private AnimageSprite[] _sprites;

    public AnimageModel(string path, ModelConfig config, AnimageSprite[] sprites)
    {
        this._path = path;
        this._config = config;
        this._sprites = sprites;
    }

    public ModelConfig GetConfig()
    {
        return this._config;
    }

    public GameObject Spawn()
    {
        GameObject container = new GameObject(this._config.Name);
        AnimageController controller = container.AddComponent<AnimageController>();

        int spriteIndex = 0;

        foreach (AnimageSprite sprite in _sprites)
        {
            string file = Path.Combine(_path, sprite.file);
            GameObject spriteObject = new GameObject("sprite_" + spriteIndex);
            spriteObject.transform.SetParent(container.transform);
            spriteIndex++;

            SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
            renderer.sprite = IMG2Sprite.instance.LoadNewSprite(file, sprite.pixelsPerUnit);

            AnimageSpriteController spriteController = spriteObject.AddComponent<AnimageSpriteController>();
            spriteController.Effects = sprite.effects;
            spriteController.MicLevelThreshold = sprite.mic_level;

            controller.AddSprite(spriteController);
        }
        return container;
    }
}
