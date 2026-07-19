using Unity.Cinemachine;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private ChangeCameraSpeed _changeCameraSpeed;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private float _zoomMax = 40;
    [SerializeField] private float _zoomMin = 1;
    [SerializeField] private float _zoomCurrent = 1;
    private Vector3 _targetPos = Vector3.one;
    private Vector3 _startPos = Vector3.zero;
    private Quaternion _startRot = Quaternion.identity;
    private float _startZoomCurrent = 1;
    private float moveSpeed = 1;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _startPos = transform.position;
        _startRot = transform.rotation;
        _startZoomCurrent = _zoomCurrent;
    }

    void Update()
    {
        Rotate();
        Move();
        Zoom();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }
    }

    public void SetTarget(Vector3 target)
    {
        _targetPos = target;
    }

    public void ResetCamera()
    {
        _targetPos = Vector3.zero;
        transform.position = _startPos;
        transform.rotation = _startRot;
        _zoomCurrent = _startZoomCurrent;
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * _changeCameraSpeed.MoveSpeed;
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        Vector3 direction = _targetPos - transform.position;
        
        // スクロール量に速度設定を掛け合わせる
        float zoomAmount = scroll * _changeCameraSpeed.ZoomSpeed * moveSpeed;

        if (scroll > 0) // 【ズームイン】ホイールを奥に回したとき（注視点に近づく）
        {
            if (_zoomCurrent < _zoomMax)
            {
                _zoomCurrent += Mathf.Abs(zoomAmount); // 現在のズーム度（倍率）を増やす

                // 注視点の方向（direction）に向かって前進させるためプラスにする
                transform.position += direction.normalized * Mathf.Abs(zoomAmount);
            }
        }
        else if (scroll < 0) // 【ズームアウト】ホイールを手前に回したとき（注視点から離れる）
        {
            if (_zoomCurrent > _zoomMin)
            {
                _zoomCurrent -= Mathf.Abs(zoomAmount); // 現在のズーム度（倍率）を減らす

                // 注視点とは逆の方向に向かって後退させるためマイナスにする
                transform.position -= direction.normalized * Mathf.Abs(zoomAmount);
            }
        }

        // 値が限界値を超えないように制限
        _zoomCurrent = Mathf.Clamp(_zoomCurrent, _zoomMin, _zoomMax);
        moveSpeed = 1 / _zoomCurrent;
    }

    private void Rotate()
    {
        float rotateInput = 0;
        if (Input.GetKey(KeyCode.E)) { rotateInput = _changeCameraSpeed.RotateSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.Q)) { rotateInput = -_changeCameraSpeed.RotateSpeed * Time.deltaTime; }

        transform.RotateAround(_targetPos, Vector3.up, rotateInput * moveSpeed);
    }

    private void Move()
    {
        Vector3 right = transform.right * Input.GetAxis("Horizontal") * _changeCameraSpeed.MoveSpeed;
        Vector3 forward = transform.forward * Input.GetAxis("Vertical") * _changeCameraSpeed.MoveSpeed;
        right.y = 0;
        forward.y = 0;
        transform.position += moveSpeed * Time.deltaTime * (right + forward);

        _targetPos += moveSpeed * Time.deltaTime * (right + forward);
    }

    public void ShakeCamera()
    {
        _impulseSource.GenerateImpulse();
    }
}
