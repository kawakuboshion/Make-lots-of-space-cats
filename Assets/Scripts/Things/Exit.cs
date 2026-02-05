using UnityEngine;

public class Exit : Things
{
    [SerializeField] private Transform _removePoint;

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
            if(_cat.InfoProcessLevel <= 0f)
            {
                GameManager.Instance.AddEnergy(10f);
            }
            Destroy(_cat.gameObject);
            _cat = null;
        }
    }
}
