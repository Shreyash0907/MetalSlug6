using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingTank : MonoBehaviour
{
    public GameObject rocket,bombPrefab;
    public Transform player;
    private Transform firingPoing;
    private Transform rocketSwapningPosition;
    public Camera mainCamera;
    private float health;

    private bool isLaunched = false;
    private Collider2D colliderBody;
    private Animator animator;
    void Start()
    {   
        health = 350f;
        firingPoing = transform.Find("BombFiringPoint");
        rocketSwapningPosition = transform.Find("RocketFiringPoint");
        animator = GetComponent<Animator>();
        colliderBody = GetComponent<PolygonCollider2D>();
    }


    void Update()
    {
        if(IsInCameraView() && !isLaunched){
            isLaunched = true;
            InvokeRepeating(nameof(LaunchRocket), 0f , 3.4f);
            InvokeRepeating(nameof(FireBomb),1f, 2f);
        }
    }

    private void LaunchRocket(){
        if(rocket != null && rocketSwapningPosition != null){
            GameObject RocketGameObject = Instantiate(rocket, rocketSwapningPosition.position, rocketSwapningPosition.rotation);
            RocketGameObject.tag = "Rocket";
            Rocket rocketObject = RocketGameObject.GetComponent<Rocket>();
            rocketObject.player = player;
        }
        
    }

    private void FireBomb()
    {
        if (bombPrefab != null && firingPoing != null)
        {
            GameObject bomb = Instantiate(bombPrefab, firingPoing.position, firingPoing.rotation);
            bomb.tag = "Bomb";
        }
    }

    private bool IsInCameraView()
    {
        Vector3 cameraView = mainCamera.WorldToViewportPoint(transform.position);
        return cameraView.x >= 0 && cameraView.x <= 1 && cameraView.y >= 0 && cameraView.y <= 1 && cameraView.z > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("PlayerBullet")){
            health -= 15;
        }
        if(collision.gameObject.CompareTag("Grenade")){
            health -= 60;
        }
        if(health <= 0){
            colliderBody.enabled = false;
            ScoreManager.scoreManagerInstance.UpdateScore(200);

            animator.SetTrigger("destroy");
            Destroy(gameObject,1.5f);
        }
    } 
}
