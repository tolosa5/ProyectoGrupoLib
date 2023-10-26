using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    bool activated;
    [SerializeField] GameObject aimedGO;
    
    public void Activate()
    {
        activated = true;
    }

    void Update()
    {
        if (activated)
        {
            
        }
    }
}
