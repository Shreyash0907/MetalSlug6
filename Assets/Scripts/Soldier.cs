using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float deathAnimationDuration = 2f; 
    private Animator animator;
    public Transform player;
    public float moveSpeed = 0.2f;
    private float distance = 2f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update(){
        float xDistance = Mathf.Abs(player.position.x - transform.position.x);
        if(xDistance <= 0.5f){
            animator.SetTrigger("Attack");
            MoveTowardsPlayer();
        }
        else if (xDistance <= distance)
        {
            MoveTowardsPlayer();
            animator.SetBool("walk", true);
        }
        if(player.position.x > transform.position.x){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }
    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // private void OnCollisionEnter2D(Collision2D collision){
    //     if(collision.gameObject.tag == "PlayerBullet"){
    //         animator.SetBool("BulletHit", true);
    //         StartCoroutine(HandleDeath());
    //         Destroy(collision.gameObject);
    //     }
    // }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            animator.SetBool("bulletHit", true);
            StartCoroutine(HandleDeath());
            Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Player has entered the trigger area of the child object.");
            
        }
    }
}
