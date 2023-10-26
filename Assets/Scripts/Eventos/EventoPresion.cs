using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoPresion : MonoBehaviour
{
    public int triggerId;

    [TextArea]
    [SerializeField] string textEvent;

    private void OnTriggerEnter(Collider other)
    {
        //al entrar en la zona en la que se puede hacer cosa,
        //se activa la posibilidad de si le das a la E, se activa
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.PresionTriggerEnter(triggerId);
            CanvasManager.canvasM.TextPicker(textEvent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.PresionTriggerExit(triggerId);
        }
    }
}
