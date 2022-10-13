using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimageController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    public int MicLevel;

    [SerializeField]
    private List<AnimageSpriteController> sprites;

    private AnimageSpriteController _current;
    private MicrophoneLevel microphoneLevel;

    public AnimageSpriteController GetDesiredSprite(int threshold)
    {
        AnimageSpriteController desired = null;

        foreach (AnimageSpriteController sprite in sprites)
        {
            if (sprite.MicLevelThreshold <= threshold)
            {
                desired = sprite;
            }
        }

        return desired == null ? sprites[0] : desired;
    }

    public AnimageSpriteController GetDesiredSprite()
    {
        return this.GetDesiredSprite(this.MicLevel);
    }


    void Awake()
    {
        this.sprites = new List<AnimageSpriteController>();
    }

    void Start()
    {
        this.microphoneLevel = MicrophoneLevel.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        AnimageSpriteController sprite = GetDesiredSprite();
        if (sprite != this._current)
        {
            if (this._current != null)
            {
                this._current.gameObject.SetActive(false);
            }

            this._current = sprite;
            sprite.gameObject.SetActive(true);
        }

        this.MicLevel = this.microphoneLevel.Percent;
    }

    public void AddSprite(AnimageSpriteController sprite)
    {
        sprites.Add(sprite);
        sprite.gameObject.SetActive(false);
    }
}
