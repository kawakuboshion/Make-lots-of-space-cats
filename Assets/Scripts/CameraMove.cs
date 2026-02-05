using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotateSpeed = 1f;   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {

            transform.eulerAngles += new Vector3(_rotateSpeed * -Input.GetAxis("Mouse Y"), _rotateSpeed * Input.GetAxis("Mouse X"), 0f);

            Vector3 forward = transform.right * Input.GetAxis("Horizontal");
            Vector3 right = transform.forward * Input.GetAxis("Vertical");
            transform.position += forward + right + new Vector3(0, -Input.GetAxis("UpDown") * _moveSpeed, 0);
        }
    }
}
