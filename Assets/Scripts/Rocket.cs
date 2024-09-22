using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public Transform player; // The target the rocket is moving towards
    public float speed = 0.001f; // Speed of the rocket
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 1f);
        InvokeRepeating("Fire", 0f, 0.5f);
    }

    // Update is called once per frame
    void Fire()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1f * Time.deltaTime);
        }
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        // Rotate the rocket to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        // Move the rocket towards the player
        rb.velocity = direction * speed;
    }
    

    // private void UpdateAnimation(Vector3 direction)
    // {
    //     // Check the speed of the rocket to determine the animation state
    //     bool isMoving = direction.magnitude > 0.1f; // Threshold to determine if the rocket is moving

    //     animator.SetBool("RocketLaunched", isMoving); // Set the "IsMoving" parameter in the Animator
        
    //     // Optionally, set the direction for more complex animations
    //     // animator.SetFloat("DirectionX", direction.x);
    //     // animator.SetFloat("DirectionY", direction.y);
    // }
}
