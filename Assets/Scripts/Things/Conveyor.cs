using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Things
{
    [SerializeField] private Transform _top;
    public float _speed = 1.0f;

    private void Update()
    {
        if(_nextThings == null)
        {
            GameObject next = GridManager.Instance.GetObjectAtPosition(transform.position +transform.forward);
            if(next != null)
            {
                _nextThings = next.GetComponent<Things>() != null ? next.GetComponent<Things>() : null;
            }
        }
    }

    public override void MoveTheCat()
    {
        if (_nextThings != null && _nextThings._cat == null)
        {
            _nextThings._cat = _cat;
            _cat.SetInThings(_nextThings);
            _cat.SetDestination(_nextThings.GetToMovePosition());
            _cat = null;
        }
    }
}
