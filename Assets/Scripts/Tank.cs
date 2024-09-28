using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject bombPrefab;
    public Camera mainCamera;
    public Transform player;
    public Transform firePoint;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float health;
    private bool hasFired = false;

    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = 100f;
    }

    void Update(){
        if(IsInCameraView() && !hasFired){
            InvokeRepeating("FireBomb", 0f, 3f);
            hasFired = true;
        }
    }


    private void FireBomb()
    {
        if (bombPrefab != null && firePoint != null)
        {
            if(player.position.x > transform.position.x){
                spriteRenderer.flipX = false;
            }else{
                spriteRenderer.flipX = true;
            }
            animator.SetTrigger("Fire");
            GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
            bomb.tag = "Bomb";

            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            if(spriteRenderer.flipX == true){
                Vector2 force = firePoint.right * -1.5f + firePoint.up * 1.5f;
                rb.AddForce(force, ForceMode2D.Impulse);
            }else if(spriteRenderer.flipX == false){
                Vector2 force = firePoint.right * 1.5f + firePoint.up * 1.5f;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if( !collision.gameObject.CompareTag("PlayerBullet")){
            return;
        }
        if(health > 0){
            health -= 10f;
        }
        if(health <= 0){
            animator.SetTrigger("Destroyed");
            spriteRenderer.sortingOrder = 3;
            Destroy(gameObject, 4f);
        }
    }

    private bool IsInCameraView()
    {
        Vector3 cameraView = mainCamera.WorldToViewportPoint(transform.position);
        return cameraView.x >= 0 && cameraView.x <= 1 && cameraView.y >= 0 && cameraView.y <= 1 && cameraView.z > 0;
    }
}
