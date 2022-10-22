using UnityEngine;

public class CameraMirror : MonoBehaviour
{
    public CameraController controller;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (controller != null)
        {
            Transform mainCamera = controller.gameObject.transform;
            Transform thisCamera = this.gameObject.transform;

            thisCamera.position = mainCamera.position;
            _camera.orthographicSize = controller.Zoom;
        }
    }
}