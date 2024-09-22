using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSoldier : MonoBehaviour
{
    public float deathAnimationDuration = 2f; 
    private Animator animator;
    public Transform player;
    public float moveSpeed = 0.5f;
    private float distance = 2f;
    private SpriteRenderer spriteRenderer;
    private bool wait = false;
    private float xDistance;

    // Start is called before the first frame update
    void Start()
    {  
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update(){
        xDistance = Mathf.Abs(player.position.x - transform.position.x);
        
        if (xDistance <= distance)
        {
            MoveTowardsPlayer();
            animator.SetBool("Walk", true);
        }
        if(player.position.x > transform.position.x){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }
    void FixedUpdate(){
        if(xDistance <= 0.5f && !wait){
            animator.SetTrigger("Attack");
            DelayUpdate();
            MoveTowardsPlayer();
        }
    }
    private IEnumerator DelayUpdate()
    {
        wait = true;

        yield return new WaitForSeconds(2f);
        wait = false;
    }

    public void Die()
    {
        if (animator != null)
        {
            animator.SetBool("bulletHit", true);
        }
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            // animator.SetBool("bulletHit", true);
            // StartCoroutine(HandleDeath());
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Grenade"))
        {
            animator.SetBool("bulletHit", true);
            StartCoroutine(HandleDeath());
            Destroy(collision.gameObject);
            // Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Player has entered the trigger area of the child object.");
            
        }
    }
}
