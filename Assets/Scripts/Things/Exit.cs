using UnityEngine;

public class Exit : Things
{
    [SerializeField] private Transform _removePoint;
    private GameManager _gameManager = GameManager.Instance;
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

    public override void FindBackThings()
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
            AudioManager.Instance.PlaySE(AudioManager.SE.Cat_Disappeared);
            if (_cat._InfoProcessLevel <= 0f)
            {
                _gameManager.AddEnergy(10f);
                _gameManager.AddSpaceCatCounter(1);
            }
            _cat.GetComponent<PooledObject>().Release();
            _cat = null;
        }
    }
}
