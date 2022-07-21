using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidbody2D;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    public void JumpedOn()
    {
        animator.SetTrigger("death");
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
