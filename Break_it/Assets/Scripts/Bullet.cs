using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;    
    public int bounceLimit;
    
    private Rigidbody2D rb2D;
    private CircleCollider2D bulletCollider;
    private int bounces;

    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<CircleCollider2D>();

        rb2D.velocity = transform.right * speed;
        bounces = 0;
    }


	void Update () {

        rb2D.velocity = rb2D.velocity.normalized * speed;
        if (bounces >= bounceLimit)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player1"))
        {
            bulletCollider.isTrigger = false;
        }
    }
}
