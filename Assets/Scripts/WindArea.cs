using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public Vector3 direction;
    Vector3 directionSave;

    public float strenght;

    Rigidbody boxRb;

    public int myId;

    private void OnEnable()
    {
        EventManager.OnPresionClose += Desactivate;
        EventManager.OnPresionOpen += Activate;
    }

    private void OnDisable()
    {
        EventManager.OnPresionClose -= Desactivate;
        EventManager.OnPresionOpen -= Activate;
    }

    void Start()
    {
        directionSave = direction;
    }

    void Desactivate(int id)
    {
        Debug.Log("desactivate");
        if (myId == id)
        {
            direction = Vector3.zero;
        }
    }

    void Activate(int id)
    {
        Debug.Log("activate");
        if (myId == id)
        {
            direction = directionSave;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CajaCoger"))
        {
            //pillo el rb y le aplico la fuerza
            boxRb = other.gameObject.GetComponent<Rigidbody>();
            boxRb.AddForce(direction * strenght, ForceMode.Impulse);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CajaCoger"))
        {
            //reset
            boxRb = other.gameObject.GetComponent<Rigidbody>();
            boxRb.AddForce(Vector3.zero);
        }
    }
}
