using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Conga : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer sprite;
    private Animator animator;
    private float health;
    void Start()
    {
        health = 20;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.position.x - transform.position.x) <= 0.5){
            animator.SetBool("Attack", true);
        }else{
            animator.SetBool("Attack",false);
        }
        if(player.position.x > transform.position.x){
            sprite.flipX = true;
        }else{
            sprite.flipX = false;
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Meele")){ 
            health -= 10;
        }
        if(collision.gameObject.CompareTag("Grenade")){
            health = 0;
        }
        if(health <= 0){
            ScoreManager.scoreManagerInstance.UpdateScore(40);
            animator.SetTrigger("Die");
            Destroy(gameObject,2f);
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("PlayerBullet") || collider.CompareTag("Meele")){ 
            health -= 10;
        }
        if(collider.CompareTag("Grenade")){
            health = 0;
        }
        if(health <= 0){
            ScoreManager.scoreManagerInstance.UpdateScore(40);
            animator.SetTrigger("Die");
            Destroy(gameObject,2f);
        }
    }
}
