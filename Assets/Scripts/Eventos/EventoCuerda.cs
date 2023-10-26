using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoCuerda : MonoBehaviour
{
    public int triggerId;

    //pillo el texto que meter, aqui es "pulsa e para abrir la puerta"
    [TextArea]
    [SerializeField] string textEvent;

    private void OnTriggerEnter (Collider other)
    {
        //al entrar en la zona en la que se puede hacer cosa,
        //se activa la posibilidad de si le das a la E, se activa
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.CuerdaTriggerEnter(triggerId);
            CanvasManager.canvasM.TextPicker(textEvent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.CuerdaTriggerExit(triggerId);
        }
    }
}
