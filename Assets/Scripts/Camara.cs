using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    RigidBodyMov playerScr;
    public GameObject playerGO;
    Vector3 movimiento = Vector3.forward;

    bool dirMov;

    public Vector3 tope;
    Vector3 aux;

    

    // Start is called before the first frame update
    void Start()
    {
        playerScr = playerGO.GetComponent<RigidBodyMov>();
        tope = transform.position;
        aux = transform.localPosition;
        Debug.Log(aux);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScr.h == 1)
        {
            dirMov = true;
            //Vector3.Lerp(transform.localPosition, movimiento, 3);
            //float hClamp = Mathf.Clamp(transform.localPosition.z, transform.localPosition.z, transform.localPosition.z + movimiento.z);
            //transform.localPosition.z = hClamp;
            if (transform.localPosition.z < 1)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, aux + 
                    new Vector3(0, 0, 2f), 4 * Time.deltaTime);
            }
        }
        else if (playerScr.h == -1)
        {
            dirMov = false;
            //Vector3.Lerp(transform.localPosition, -movimiento, 3);
            //transform.localPosition -= movimiento;
            if (transform.localPosition.z > aux.z - 1)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, aux + 
                    new Vector3(0, 0, -2f), 4 * Time.deltaTime);
            }
        }
        else
        {
            
            if (transform.localPosition != aux)
            {
                Debug.LogWarning("SSS");
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, 
                    aux, 8* Time.deltaTime);//new Vector3(transform.localPosition.x, transform.localPosition.y, 0), 8 * Time.deltaTime);
            }else
            {
                
            }
        }

    }
}
