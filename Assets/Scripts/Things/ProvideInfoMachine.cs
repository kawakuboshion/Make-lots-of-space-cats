using UnityEngine;

public class ProvideInfoMachine : Things
{
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private float _infoAmount = 10f;
    private bool _isProviding = false;

    public override void MoveTheCat()
    {
        if(!_isProviding)
        {
            _isProviding = true;
            _cat.gameObject.SetActive(false);
            _cat.ReduceInfoProcessLevel(_infoAmount);
            _cat.SetDestination(_exitPoint.position);
            return;
        }
        if (_nextThings != null && _nextThings._cat == null)
        {
            _isProviding = false;
            _cat.gameObject.SetActive(true);
            _nextThings._cat = _cat;
            _cat.SetInThings(_nextThings);
            _cat.SetDestination(_nextThings.GetToMovePosition());
            _cat = null;
        }
    }
}
