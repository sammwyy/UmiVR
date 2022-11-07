using System.Collections;
using UnityEngine;

public class AnimageSpriteController : MonoBehaviour
{
    public AnimageEffects Effects;
    public int MicLevelThreshold;

    private SpriteRenderer _sprite;
    private Vector3 _startingPos;
    private Vector3 _targetPos;

    void Awake()
    {
        this._sprite = GetComponent<SpriteRenderer>();
        this._startingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Start()
    {
        if (Effects.grayscale)
        {
            this._sprite.material.shader = Shader.Find("Thor/Sprites/GreyscaleShader");
        }

        if (Effects.shake > 0)
        {
            InvokeRepeating("Shake", 0, Effects.shakeTick);
        }
    }

    float GetModulusRange(float range)
    {
        range = Mathf.Abs(range);
        return Random.Range(range * -1, range);
    }

    void Shake()
    {
        this._targetPos = new Vector3(this._startingPos.x, this._startingPos.y, this._startingPos.z);

        float range = Effects.shake;
        this._targetPos.x += GetModulusRange(range);
        this._targetPos.y += GetModulusRange(range);
    }

    void Update()
    {
        if (_targetPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * Effects.shakeSpeed);
        }
    }

    public void Show()
    {
        _sprite.color = Color.white;
    }

    public void Hide()
    {
        _sprite.color = new Color(255, 255, 255, 0);
    }
}
