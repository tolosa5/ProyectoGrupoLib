using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject heldObject;
    [SerializeField] Transform holder;

    void Update()
    {
        Debug.Log(heldObject);
        if (heldObject != null)
        {
            MovePickObject();
            Invoke("DropObjects", .1f);
        }
    }

    public void PickUpObjects(GameObject pickObject)
    {
        //pilla el rb del objeto que coge
        Rigidbody pickRB = pickObject.GetComponent<Rigidbody>();
        pickRB.isKinematic = true;
        //le hago hijo del holder, donde ira al ser cogido, para que asi se mueva respecto a la camara
        pickObject.transform.parent = holder;
        //pickObject.transform.position = holder.position;
        //le doy valor al objeto para saber que tengo uno cogido, cual es y operar con el
        heldObject = pickObject;
        Debug.Log("Fin pick");
    }

    public void MovePickObject()
    {
        heldObject.transform.position = holder.position;
    }

    public void DropObjects()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //reset de todo
            Rigidbody pickRB = heldObject.GetComponent<Rigidbody>();
            pickRB.isKinematic = false;

            heldObject.transform.parent = null;
            heldObject = null;
        }
    }
}
