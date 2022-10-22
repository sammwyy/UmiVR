using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private CameraControllerConfig _config;

    private float zoom = 4f;
    private float minZoom = 0.25f;
    private float maxZoom = 10;

    public float Zoom
    {
        get
        {
            return this.zoom;
        }
    }

    void HandleMovement()
    {
        if (Input.GetMouseButton(1))
        {
            float speed = (zoom * 4) * Time.deltaTime;

            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            gameObject.transform.position -= new Vector3(x * speed, y * speed, 0);
        }
    }

    void HandleZoom()
    {
        float wheel = Input.mouseScrollDelta.y;
        if (wheel != 0)
        {
            float newSize = zoom - (wheel / 4);
            float newSizeFixed = Mathf.Clamp(newSize, minZoom, maxZoom);
            zoom = newSizeFixed;
        }
        _camera.orthographicSize = zoom;
    }

    void LoadConfig()
    {
        this._config = CameraControllerConfig.Load();
        this.zoom = this._config.zoom;
        this.transform.position = new Vector3(this._config.x, this._config.y, this.transform.position.z);
    }

    void SaveConfig()
    {
        this._config.zoom = this.zoom;
        this._config.x = this.transform.position.x;
        this._config.y = this.transform.position.y;
        this._config.Save();
    }

    void Start()
    {
        _camera = GetComponent<Camera>();
        this.LoadConfig();
        InvokeRepeating("SaveConfig", 3f, 3f);
    }

    void Update()
    {
        this.HandleMovement();
        this.HandleZoom();
    }
}