using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : MonoBehaviour
{
    public float deathAnimationDuration = 2f; 
    private Animator animator;
    public Transform player;
    public float moveSpeed = 0.4f;
    private float distance = 2f;
    private SpriteRenderer spriteRenderer;
    private Collider2D colliderbody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderbody = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
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

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        Destroy(gameObject);
    }

    public void Die(){
        animator.SetTrigger("bulletHit");
        StartCoroutine(HandleDeath());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if(collision.gameObject.CompareTag("Meele")){
            Debug.Log("player meele");
            colliderbody.enabled = false;
            Die();
        }
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Grenade"))
        {
            colliderbody.enabled = false;
            moveSpeed = 0f;
            Die();
            Destroy(collision.gameObject);
        }
        
        
    }

}
