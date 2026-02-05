using UnityEngine;

public class Things : MonoBehaviour
{
    [SerializeField] public Transform _BottomPos;
    [SerializeField] public Transform _ToMovePos;
    [SerializeField] public ThingType _ThingType;
    [SerializeField] protected Things _nextThings;
    [SerializeField] public Cat _cat;

    public virtual void MoveTheCat()
    {
        // Override in derived classes
        Debug.Log("MoveTheCat called in base Things class");
    }

    public virtual Vector3 GetToMovePosition()
    {
        return _ToMovePos.position;
    }

    public enum ThingType
    {
        Conveyor,
        Entrance,
        Exit
    }
}