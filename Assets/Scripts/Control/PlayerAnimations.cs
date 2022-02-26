using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Idle()
    {

    }

    public void Walk(Vector2 moveVector)
    {
        if(moveVector == Vector2.zero)
        {
            anim.SetBool("Move",false);
        }
        else
        {
            anim.SetBool("Move", true);
        }
    }

    public void ResetAnim()
    {
        
    }
}
