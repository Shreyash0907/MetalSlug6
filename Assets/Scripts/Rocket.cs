using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public Transform player; // The target the rocket is moving towards
    public float speed = 0.001f; // Speed of the rocket
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDestroyed = false;
    void Start()
    {
        StartCoroutine(Delay());
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 1f);
        InvokeRepeating("Fire", 0f, 0.5f);
        Invoke("DestroyRocket", 2.5f);
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

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }
    private void DestroyRocket(){
        if(!isDestroyed)
        {
            rb.isKinematic = true;
            isDestroyed = true;
            animator.SetTrigger("Explode");
            Destroy(gameObject,0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            DestroyRocket();
        }
    }
    

}
