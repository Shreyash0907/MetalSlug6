using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillboxBomb : MonoBehaviour
{
    private Rigidbody2D body;
    public Transform capsule;
    private float speed = 2f;
    private float life = 10f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Destroy(gameObject, life);
    }

    void FixedUpdate()
    {
        if(capsule)
        {
            Vector2 direction = capsule.up; 
            body.velocity = direction * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collided with a soldier
        animator.SetTrigger("BombHit");
        Destroy(gameObject,1f);
        
    }
}
