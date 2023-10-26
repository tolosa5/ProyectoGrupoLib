using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlavePresion : MonoBehaviour
{
    [SerializeField] bool open; 
    [SerializeField] int myId;

    //[SerializeField] GameObject wind;

    public void Alternate(int id)
    {
        if (myId == id)
        {
            if (open)
            {
                open = false;
                EventManager.PresionClose(id);
            }
            else
            {
                open = true;
                EventManager.PresionOpen(id);
            }
        }
    }

    /*
    void Close(int id)
    {
        WindArea windScr = wind.GetComponent<WindArea>();
        windScr.enabled = false;
    }

    void Open(int id)
    {
        WindArea windScr = wind.GetComponent<WindArea>();
        windScr.enabled = true;
    }
    */
}
