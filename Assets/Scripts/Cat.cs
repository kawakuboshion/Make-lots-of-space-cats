using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    public Conveyor _conveyorBelow;
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

    public void SetConveyorBelow(Conveyor conveyor)
    {
        _conveyorBelow = conveyor;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStopped) { return; }

        _moveTime += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPos, _destination, _moveTime);

        if(_moveTime >= 1.0f)
        {
            _isStopped = true;
            _moveTime = 0.0f;
            if(_conveyorBelow != null)
            {
                _conveyorBelow.MoveTheCat();
            }
        }
    }
}
