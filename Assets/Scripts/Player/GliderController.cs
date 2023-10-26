using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 rot;

    float tilt;
    float fastSpeed, slowSpeed;
    float hardDrag, softDrag;

    float modDrag;
    float modVel;

    Transform body;

    public static GliderController gliderInstance;

    private void Awake()
    {
        if (gliderInstance == null)
        {
            gliderInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = transform.GetChild(0);

        //rot = body.eulerAngles;
        //rot = transform.eulerAngles;

        fastSpeed = 14;
        slowSpeed = 12;

        hardDrag = 6;
        softDrag = 4;
    }

    public void WindDrag()
    {
        modDrag = (tilt * (softDrag - hardDrag) + hardDrag);
        modVel = (tilt * (fastSpeed - slowSpeed) + slowSpeed);

        rb.drag = modDrag;

        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        localVel.z = modVel;
        rb.velocity = transform.TransformDirection(localVel);

        //Debug.Log("planeando");
    }

    public void Rotation()
    {
        int velRot = 20;

        RigidBodyMov movScr;
        movScr = GetComponent<RigidBodyMov>();

        tilt = rot.x / 35;

        rot.x += movScr.v * velRot * Time.deltaTime;
        rot.x = Mathf.Clamp(rot.x, 0, 35);
        //body.rotation = Quaternion.Euler(rot);
    }
}
