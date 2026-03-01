using UnityEngine;

public class Entrance : Things
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ObjectPool _catPool;
    void Update()
    {
        if (_nextThings != null)
        {
            MoveTheCat();
        }
        if(_catPool == null)
        {
            _catPool = FindAnyObjectByType<ObjectPool>();
        }
    }

    public override void MoveTheCat()
    {
        if (_nextThings != null && _nextThings._cat == null)
        {
            Debug.Log("Send the cat");
            _nextThings._cat = _catPool.GetPooledObject().GetComponent<Cat>();
            _nextThings._cat.transform.position = _spawnPoint.position;
            _nextThings._cat.transform.rotation = transform.rotation;
            _nextThings._cat.Initialize();
            AudioManager.Instance.PlaySE(AudioManager.SE.Cat_Appeared);
            _nextThings._cat.SetInThings(_nextThings);
            _nextThings._cat.SetDestination(_nextThings.GetToMovePosition());
            _nextThings._cat.StartMoving();
        }
    }
}
