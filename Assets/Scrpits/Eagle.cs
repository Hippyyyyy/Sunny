using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    public float TopTran;
    public float BottomTran;
    float speed = 5f;
    float jump = 5f;
    private Collider2D coll;
    public bool isUp = true;
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isUp)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed);
            if(transform.position.y > TopTran)
            {
                isUp = false;
            }

        }
        else
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -speed);
            if (transform.position.y < BottomTran)
            {
                isUp = true;
            }

        }
    }
}
