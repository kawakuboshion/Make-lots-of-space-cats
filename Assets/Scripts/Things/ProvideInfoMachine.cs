using UnityEngine;

public class ProvideInfoMachine : Things
{
    [SerializeField] private float _infoAmount = 10f;

    public override void MoveTheCat()
    {
        if (_nextThings != null && _nextThings._cat == null)
        {
            _cat.ReduceInfoProcessLevel(_infoAmount);
            _nextThings._cat = _cat;
            _cat.SetInThings(_nextThings);
            _cat.SetDestination(_nextThings.GetToMovePosition());
            _cat = null;
        }
    }
}
