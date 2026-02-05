using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Things
{
    [SerializeField] private Transform _top;
    private Conveyor _nextConveyor;
    public Cat _cat;
    public float _speed = 1.0f;

    private void Update()
    {
        if(_nextConveyor == null)
        {
            GameObject next = GridManager.Instance.GetObjectAtPosition(transform.position +transform.forward);
            if(next != null)
            {
                _nextConveyor = next.GetComponent<Conveyor>() != null ? next.GetComponent<Conveyor>() : null;
            }
        }
    }

    public void MoveTheCat()
    {
        if (_nextConveyor != null && _nextConveyor._cat == null)
        {
            _nextConveyor._cat = _cat;
            _cat.SetConveyorBelow(_nextConveyor);
            _cat.SetDestination(_nextConveyor.GetTopPosition());
            _cat = null;
        }
    }

    public Vector3 GetTopPosition()
    {
        return _top.position;
    }
}
