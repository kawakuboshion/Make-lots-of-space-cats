using Unity.VisualScripting;
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
    protected GridManager _gridManager;

    protected void Start()
    {
        if (_gridManager == null)
        {
            _gridManager = FindAnyObjectByType<GridManager>();
        }
        FindNextThings();
        FindBackThings();
        FindRightSideThings();
        FindLeftSideThings();
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
                Debug.Log("Next object found: " + next.name);
                _nextThings = next.GetComponent<Things>() != null ? next.GetComponent<Things>() : null;
            }
        }
    }

    public virtual void FindRightSideThings()
    {
        GameObject right = _gridManager.GetObjectAtPosition(transform.position + transform.right);
        if (right != null)
        {
            right.GetComponent<Things>().FindNextThings();
        }
    }

    public virtual void FindLeftSideThings()
    {
        GameObject left = _gridManager.GetObjectAtPosition(transform.position - transform.right);
        if (left != null)
        {
            left.GetComponent<Things>().FindNextThings();
        }
    }

    public virtual void FindBackThings()
    {
        GameObject back = _gridManager.GetObjectAtPosition(transform.position - transform.forward);
        if (back != null)
        {
            if(_gridManager.GetObjectAtPosition(back.transform.position + back.transform.forward) == this.gameObject)
            {
                back.GetComponent<Things>().FindNextThings();
            }
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