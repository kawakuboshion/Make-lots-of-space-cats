using Unity.Cinemachine;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private ChangeCameraSpeed _changeCameraSpeed;
    [SerializeField] private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {

            transform.eulerAngles += new Vector3(_changeCameraSpeed.RotateSpeed * -Input.GetAxis("Mouse Y"), 
                _changeCameraSpeed.RotateSpeed * Input.GetAxis("Mouse X"), 0f);

            Vector3 forward = transform.right * Input.GetAxis("Horizontal");
            Vector3 right = transform.forward * Input.GetAxis("Vertical");
            transform.position += forward + right + new Vector3(0, -Input.GetAxis("UpDown") * _changeCameraSpeed.MoveSpeed, 0);
        }
    }

    public void ShakeCamera()
    {
        _impulseSource.GenerateImpulse();
    }
}
