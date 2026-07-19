using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraSpeed : MonoBehaviour
{
    [SerializeField] private Slider _moveSpeedSlider;
    [SerializeField] private Slider _rotateSpeedSlider;
    [SerializeField] private Slider _zoomSpeedSlider;
    private static float _moveSpeed = 20f; 
    private static float _rotateSpeed = 20f;
    private static float _zoomSpeed = 2f;
    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public float ZoomSpeed => _zoomSpeed;


    private void Start()
    {
        _moveSpeedSlider.value = _moveSpeed;
        _rotateSpeedSlider.value = _rotateSpeed;
        _zoomSpeedSlider.value = _zoomSpeed;
    }

    public static void SetMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public static void SetRotateSpeed(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }

    public static void SetZoomSpeed(float zoomSpeed)
    {
        _zoomSpeed = zoomSpeed;
    }
}
