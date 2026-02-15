using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraSpeed : MonoBehaviour
{
    [SerializeField] private Slider _moveSpeedSlider;
    [SerializeField] private Slider _rotateSpeedSlider;
    [SerializeField] private static float _moveSpeed = 1f;
    [SerializeField] private static float _rotateSpeed = 1f;

    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;

    private void Start()
    {
        _moveSpeedSlider.value = _moveSpeed;
        _rotateSpeedSlider.value = _rotateSpeed;
    }

    public static void SetMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public static void SetRotateSpeed(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }
}
