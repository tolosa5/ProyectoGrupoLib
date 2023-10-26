using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoPuerta : MonoBehaviour
{
    Vector3 myTransform;
    public int doorId;

    void OnEnable()
    {
        myTransform = transform.position;
        EventManager.OnDoorTriggerEnter += Open;
        EventManager.OnDoorTriggerExit += Close;

    }

    public void Open(int triggerId)
    {
        if (triggerId == doorId)
        {
            Debug.Log("puerta");
            transform.Translate(Vector3.up * 3f);
        }
    }
    void Close(int triggerId)
    {
        if (triggerId == doorId)
        {
            transform.position = myTransform;
        }
    }

    void OnDisable()
    {
        EventManager.OnDoorTriggerEnter -= Open;
        EventManager.OnDoorTriggerExit -= Close;

    }
}
