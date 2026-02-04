using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    Dictionary<(float,float), GameObject> _placedObjectsAndVectors = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanPlaceObjectAtPosition(Vector3 position)
    {
        Debug.Log($"Can Place Object is {!_placedObjectsAndVectors.ContainsKey((position.x,position.z))}");
        return !_placedObjectsAndVectors.ContainsKey((position.x, position.z));
    }

    public void RegisterPlacedObject(Vector3 position, GameObject placedObject)
    {
        if (!_placedObjectsAndVectors.ContainsKey((position.x, position.z)))
        {
            _placedObjectsAndVectors[(position.x, position.z)] = placedObject;
        }
        else
        {
            Debug.LogWarning($"Position {position} is already occupied.");
        }
    }

    public void UnregisterPlacedObject(Vector3 position)
    {
        if (_placedObjectsAndVectors.ContainsKey((position.x, position.z)))
        {
            _placedObjectsAndVectors.Remove((position.x, position.z));
        }
        else
        {
            Debug.LogWarning($"No object found at position {position} to unregister.");
        }
    }

    public GameObject GetObjectAtPosition(Vector3 position)
    {
        _placedObjectsAndVectors.TryGetValue((position.x, position.z), out GameObject placedObject);
        return placedObject;
    }

    public void ClearAllPlacedObjects()
    {
        _placedObjectsAndVectors.Clear();
    }

    public int GetPlacedObjectCount()
    {
        return _placedObjectsAndVectors.Count;
    }
}
