using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBomb : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collided with a soldier
        Debug.Log("Explode");
        animator.SetTrigger("Explode");
        if (body != null)
        {
            body.isKinematic = true;
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
        }

        Destroy(gameObject,1f);
        
    }
}
