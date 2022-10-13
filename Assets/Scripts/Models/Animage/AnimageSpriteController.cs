using System.Collections;
using UnityEngine;

public class AnimageSpriteController : MonoBehaviour
{
    public AnimageEffects Effects;
    public int MicLevelThreshold;

    private SpriteRenderer _sprite;
    private Vector3 _startingPos;
    private Vector3 _targetPos;

    void Start()
    {
        this._sprite = GetComponent<SpriteRenderer>();
        this._startingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Effects.grayscale)
        {
            this._sprite.material.shader = Shader.Find("Thor/Sprites/GreyscaleShader");
        }

        if (Effects.shake)
        {
            InvokeRepeating("Shake", 0, .05f);
        }
    }

    void Shake()
    {
        this._targetPos = new Vector3(this._startingPos.x, this._startingPos.y, this._startingPos.z);
        this._targetPos.x += Random.Range(-0.2f, 0.2f);
        this._targetPos.y += Random.Range(-0.2f, 0.2f);
    }

    void Update()
    {
        if (_targetPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * 15);
        }
    }
}
