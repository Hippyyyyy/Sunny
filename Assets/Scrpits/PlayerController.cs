using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private Collider2D coll;
    SpriteRenderer sprite;
    [SerializeField] private LayerMask ground;
    int speed = 5;
    float jump = 6;
    public int cherries = 0;
    public Text cherryText;
    private float hurtForce = 10f;
    public int healthcount;
    public Text health;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {


        Move();
        
        
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            sprite.flipX = true;
            animator.SetInteger("status", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            sprite.flipX = false;
            /*animator.SetBool("running", false);*/
            animator.SetInteger("status", 1);
        }
        else
        {
            animator.SetInteger("status", 0);
        }

        if (Input.GetButtonDown("Jump")  && coll.IsTouchingLayers(ground))
        {
            /*rigidbody2D.AddForce(Vector3.up * jump, ForceMode2D.Impulse);*/
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 7f);
            
        }
        setAnim();
    }

   


    private void setAnim()
    {
        if(rigidbody2D.velocity.y == 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }
        
        if(rigidbody2D.velocity.y > 0)
        {
            animator.SetBool("jumping", true);
        }
        if (rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }
        if (coll.IsTouchingLayers(ground))
        {
            animator.SetBool("idle", true);
            
        }
    }


    private void Jumpon()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 8f);
    }
 



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("falling"))
            {

                enemy.JumpedOn();
                Jumpon();
            }

            else {
                HandleHealth();
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                rigidbody2D.velocity = new Vector2(-hurtForce, rigidbody2D.velocity.y);
                    animator.SetBool("hurting", true);
                }
            else if (transform.position.x < collision.gameObject.transform.position.x)
                {
                rigidbody2D.velocity = new Vector2(hurtForce, rigidbody2D.velocity.y);
                    animator.SetBool("hurting", true);
                }
            }
        }
        
    }

    private void HandleHealth()
    {
        healthcount -= 1;
        health.text = healthcount.ToString();
        if (healthcount <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

   



}

