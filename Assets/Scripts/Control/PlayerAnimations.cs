using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private Vector2 lastVector = Vector2.zero;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Idle()
    {

    }

    public void Walk(Vector2 moveVector)
    {
        if (moveVector == Vector2.zero && anim.GetBool("Move")) anim.SetBool("Move", false);
        else if (moveVector != Vector2.zero) anim.SetBool("Move", true);
    }

    public void ResetAnim()
    {
        
    }
}
