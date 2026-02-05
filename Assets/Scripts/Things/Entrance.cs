using UnityEngine;

public class Entrance : Things
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _cat;
    [SerializeField] private Conveyor _nextConveyor;
    void Update()
    {
        if (_nextConveyor == null)
        {
            Debug.Log("Searching for next conveyor");
            GameObject next = GridManager.Instance.GetObjectAtPosition(transform.position+transform.forward);
            if (next != null)
            {
                Debug.Log("Found next conveyor");
                _nextConveyor = next.GetComponent<Conveyor>() != null ? next.GetComponent<Conveyor>() : null;
            }
        }
        else
        {
            SendTheCat();
        }
    }

    private void SendTheCat()
    {
        if (_cat != null && _nextConveyor._cat == null && _nextConveyor != null)
        {
            Debug.Log("Send the cat");
            _nextConveyor._cat = Instantiate(_cat, _spawnPoint.transform.position, transform.rotation).GetComponent<Cat>();
            _nextConveyor._cat.SetConveyorBelow(_nextConveyor);
            _nextConveyor._cat.SetDestination(_nextConveyor.GetTopPosition());
        }
    }
}
