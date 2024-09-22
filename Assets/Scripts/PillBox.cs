using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBox : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject bulletPrefab;
    public Camera mainCamera;
    public Transform firePoint1, firePoint2;
    private Animator animator;
    private float health;
    private bool hasFired1 = false, hasFired2 = false;
    private Collider2D colliderbody;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        colliderbody = GetComponent<PolygonCollider2D>();
        health = 100f;
    }
    void Update(){
        if(IsInCameraView() && !hasFired1){
            InvokeRepeating("FireBomb", 0f, 4.5f);
            hasFired1 = true;
        }
        if(IsInCameraView() && !hasFired2){
            InvokeRepeating("FireBullets", 0f, 2f);
            hasFired2 = true;
        }
    }

    private void FireBomb()
    {
        if (bombPrefab != null && firePoint1 != null)
        {
            GameObject bomb = Instantiate(bombPrefab, firePoint1.position, firePoint1.rotation);
        }
    }

    private void FireBullets(){
        GameObject bullet = Instantiate(bulletPrefab,firePoint2.position, firePoint2.rotation);

        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        Transform capsule = bullet.transform;
        Transform capsulePosition = capsule.GetChild(0);

        bulletSpriteRenderer.flipX = true;
        Vector3 currentRotation = capsulePosition.eulerAngles;
        currentRotation.z *= -1;
        capsulePosition.eulerAngles = currentRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(health > 0 ){
            if(collision.gameObject.CompareTag("PlayerBullet")){
                health -= 10f;
            }else if(collision.gameObject.CompareTag("Grenade")){
                health -= 50f;
            }
        }
        if(health <= 0){
            colliderbody.enabled = false;
            animator.SetTrigger("Destroyed");
            Destroy(gameObject, 1f);
        }
    }
    private bool IsInCameraView()
    {
        Vector3 cameraView = mainCamera.WorldToViewportPoint(transform.position);
        return cameraView.x >= 0 && cameraView.x <= 1 && cameraView.y >= 0 && cameraView.y <= 1 && cameraView.z > 0;
    }
}
