using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Things
{
    [SerializeField] private Transform _top;
    public float _speed = 1.0f;

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
