using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    public float leftTran;
    public float rightTran;
  
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    float speed = 5f;
    float jump = 5f;

    private bool facingleft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = gameObject.GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        setAnim();
        Move();
        SwitAnim();
    }

    private void setAnim()
    {
        if (rigidbody2D.velocity.y == 0)
        {
            animator.SetBool("jumping_frog", false);
            animator.SetBool("falling_frog", false);
        }

        if (rigidbody2D.velocity.y > 0)
        {
            animator.SetBool("jumping_frog", true);
        }
        if (rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("jumping_frog", false);
            animator.SetBool("falling_frog", true);
        }
        if (coll.IsTouchingLayers(ground))
        {
            animator.SetBool("idle", true);

        }

        
    }

    private void SwitAnim()
    {
        if (animator.GetBool("jumping_frog"))
        {
            if (rigidbody2D.velocity.y < 0)
            {
                animator.SetBool("jumping_frog", false);
                animator.SetBool("falling_frog", true);
            }
        }
        else if (coll.IsTouchingLayers(ground) && animator.GetBool("falling_frog"))
        {
            animator.SetBool("falling_frog", false);
        }
    }
    private void Move()
    {
        if (facingleft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                animator.SetBool("jumping_frog", true);
                rigidbody2D.velocity = new Vector2(-speed, jump);
            }

            if(transform.position.x < leftTran)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                facingleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                animator.SetBool("jumping_frog", true);
                rigidbody2D.velocity = new Vector2(speed, jump);
            }

            if (transform.position.x > rightTran)
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingleft = true;
            }
        }
    }


    
}
