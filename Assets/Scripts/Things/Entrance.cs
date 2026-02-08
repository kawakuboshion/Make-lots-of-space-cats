using UnityEngine;

public class Entrance : Things
{
    [SerializeField] private Transform _spawnPoint;
    void Update()
    {
        if (_nextThings != null)
        {
            MoveTheCat();
        }
    }

    public override void MoveTheCat()
    {
        if (_cat != null && _nextThings != null && _nextThings._cat == null)
        {
            Debug.Log("Send the cat");
            _nextThings._cat = Instantiate(_cat, _spawnPoint.transform.position, transform.rotation).GetComponent<Cat>();
            _nextThings._cat.SetInThings(_nextThings);
            _nextThings._cat.SetDestination(_nextThings.GetToMovePosition());
            _cat.StartMoving();
        }
    }
}
