using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private float zoom = 4f;
    private float minZoom = 0.25f;
    private float maxZoom = 10;

    void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            float speed = (zoom * 4) * Time.deltaTime;

            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            gameObject.transform.position -= new Vector3(x * speed, y * speed, 0);
        }
    }

    void Zoom()
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

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        this.Movement();
        this.Zoom();
    }
}