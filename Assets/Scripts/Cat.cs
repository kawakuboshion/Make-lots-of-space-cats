using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] public float InfoProcessLevel = 100f;
    [SerializeField] private float _speed = 1.0f;
    public Things _inThings;
    public bool _isStopped = false;
    private Vector3 _startPos;
    private Vector3 _destination;
    private float _moveTime;

    public void SetDestination(Vector3 destination)
    {
        _isStopped = false;
        _startPos = transform.position;
        _destination = destination;
    }

    public void SetInThings(Things things)
    {
        _inThings = things;
    }

    public void StartMoving()
    {
        _isStopped = false;
        _moveTime = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isStopped) { return; }

        _moveTime += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPos, _destination, _moveTime);

        if(_moveTime >= 1.0f)
        {
            if(_inThings != null)
            {
                _isStopped = true;
                _moveTime = 0.0f;
                _inThings.MoveTheCat();
            }
        }
    }
}
