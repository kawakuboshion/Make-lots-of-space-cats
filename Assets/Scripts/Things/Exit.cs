using UnityEngine;

public class Exit : Things
{
    [SerializeField] private Transform _removePoint;
    private GameManager _gameManager = GameManager.Instance;

    public void Update()
    {
        if (_cat != null&&_cat._isStopped)
        {
            RemoveTheCat();
        }
    }

    public override void MoveTheCat()
    {
        if (_cat != null)
        {
            _cat.StartMoving();
            RemoveTheCat();
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
