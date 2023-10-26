using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actioner : MonoBehaviour
{
    public bool activated;
    [SerializeField] GameObject aimedGO;

    public void Activate()
    {
        activated = true;
    }
    public void Deactivate()
    {
        activated = false;
    }

    void Update()
    {
        if (activated)
        {
            //hacer cosa
        }
        else
        {
            //hacer cosa de nuevo
        }
    }
}
