using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GunSoldier : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;
    public float deathAnimationDuration = 2f; 
    private Animator animator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    private bool hasFired = false;
    private SpriteRenderer spriteRenderer;
    // private Rigidbody2D body;
    private Collider2D colliderBody;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderBody = GetComponent<BoxCollider2D>();
        
    }

    void Update(){
        if(player.position.x < transform.position.x){
            spriteRenderer.flipX = false;
            if(firingPoint.localPosition.x > 0){
            Vector2 firingPosition = firingPoint.localPosition;
            firingPosition.x *= -1;
            firingPoint.localPosition = firingPosition;
            }
        }else{
            spriteRenderer.flipX = true;
            if(firingPoint.localPosition.x < 0){
            Vector2 firingPosition = firingPoint.localPosition;
            firingPosition.x *= -1;
            firingPoint.localPosition = firingPosition;
            }
        }
        if(IsInCameraView() && !hasFired){
            InvokeRepeating("Fire", 0f, 1.5f);
            hasFired = true;
        }
        
    }
   
    private void Fire(){
        GameObject bullet = Instantiate(bulletPrefab,firingPoint.position, firingPoint.rotation);
        bullet.tag = "SoldierBullet";
        AudioSource audio = bullet.GetComponent<AudioSource>();
        audio.volume = 0f;

        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        Transform capsule = bullet.transform;
        Transform capsulePosition = capsule.GetChild(0);

        if(spriteRenderer.flipX == false){
            bulletSpriteRenderer.flipX = true;
            Vector3 currentRotation = capsulePosition.eulerAngles;
            currentRotation.z *= -1;
            capsulePosition.eulerAngles = currentRotation;
        }
    }

    
    // private void OnCollisionEnter2D(Collision2D collision){
    //     if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Grenade")){
    //         animator.SetBool("BulletHit", true);
    //         Destroy(collision.gameObject);
    //         StartCoroutine(HandleDeath());
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Grenade")){
            colliderBody.enabled = false;
            animator.SetBool("BulletHit", true);
            Destroy(collision.gameObject);
            // StartCoroutine(HandleDeath());
            Destroy(gameObject,0.5f);
            ScoreManager.scoreManagerInstance.UpdateScore(30);
        }
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(0.5f);
        
    }

    private bool IsInCameraView()
    {
        Vector3 cameraView = mainCamera.WorldToViewportPoint(transform.position);
        return cameraView.x >= 0 && cameraView.x <= 1 && cameraView.y >= 0 && cameraView.y <= 1 && cameraView.z > 0;
    }
}
