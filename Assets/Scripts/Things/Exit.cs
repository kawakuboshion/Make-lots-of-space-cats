using UnityEngine;

public class Exit : Things
{
    [SerializeField] private Transform _removePoint;
    private GameManager _gameManager = GameManager.Instance;
    private GridManager _gridManager = GridManager.Instance;
    public override void MoveTheCat()
    {
        if (_cat != null)
        {
            _cat.StartMoving();
            _cat.SetInThings(this);
            _cat.SetDestination(_removePoint.position);
            RemoveTheCat();
        }
    }

    public override void ConnectBackThings()
    {
        GameObject back = _gridManager.GetObjectAtPosition(transform.position + transform.forward);
        if (back != null)
        {
            back.GetComponent<Things>().FindNextThings();
        }
    }

    public void RemoveTheCat()
    {
        if (_cat != null)
        {
            if(_cat._InfoProcessLevel <= 0f)
            {
                _gameManager.AddEnergy(10f);
            }
            Destroy(_cat.gameObject);
            _cat = null;
        }
    }
}
