using UnityEngine;

public class Things : MonoBehaviour
{
    [SerializeField] public Transform _BottomPos;
    [SerializeField] public Transform _ToMovePos;
    [SerializeField] public ThingType _ThingType;
    [SerializeField] protected Things _nextThings;
    [SerializeField] public float _Price;
    [SerializeField] public Cat _cat;
    [SerializeField] public string _thingName;
    private GridManager _gridManager = GridManager.Instance;

    void Start()
    {
        if (_gridManager == null)
        {
            _gridManager = GridManager.Instance;
        }
    }
    public virtual void MoveTheCat()
    {
        // Override in derived classes
        Debug.Log("MoveTheCat called in base Things class");
    }

    public virtual void FindNextThings()
    {
        if (_nextThings == null)
        {
            GameObject next = _gridManager.GetObjectAtPosition(transform.position + transform.forward);
            if (next != null)
            {
                _nextThings = next.GetComponent<Things>() != null ? next.GetComponent<Things>() : null;
            }
        }
    }

    public virtual void ConnectBackThings()
    {
        GameObject back = _gridManager.GetObjectAtPosition(transform.position - transform.forward);
        if (back != null)
        {
            back.GetComponent<Things>().FindNextThings();
        }
    }

    public virtual Vector3 GetToMovePosition()
    {
        return _ToMovePos.position;
    }

    public enum ThingType
    {
        Conveyor,
        Entrance,
        Exit,
        ProvideInfoBox,
        ProvideInfoMushroom,
        ProvideInfoMatatabi,
        ProvidInfoMachine,
        ProvideInfoWater
    }
}