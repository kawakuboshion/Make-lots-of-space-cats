using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Conveyor : MonoBehaviour
{
    public bool _IsConnected = false;
    private List<Conveyor> conveyorList = new(2);

    private void OnCollisionEnter(Collision collision)
    {
        if (!_IsConnected && collision.gameObject.GetComponent<Conveyor>() != null)
        {
            conveyorList.Add(collision.gameObject.GetComponent<Conveyor>());
            _IsConnected = true;
        }
        else if(conveyorList.Count < 2 && collision.gameObject.GetComponent<Conveyor>() != null)
        {
            conveyorList.Add(collision.gameObject.GetComponent<Conveyor>());
        }
    }
}
