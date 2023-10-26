using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo animState;

    public static PlayerAnimHandler animHandler;

    void Awake()
    {
        if (animHandler == null)
        {
            animHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }
}
