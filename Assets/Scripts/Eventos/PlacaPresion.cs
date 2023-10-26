using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaPresion : MonoBehaviour
{
    public int triggerId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Caja") || other.gameObject.CompareTag("CajaCoger"))
        {
            EventManager.DoorTriggerEnter(triggerId);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Caja") || other.gameObject.CompareTag("CajaCoger"))
        {
            EventManager.DoorTriggerExit(triggerId);
        }
    }
}
