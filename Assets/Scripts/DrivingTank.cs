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
    void Start()
    {   
        health = 150f;
        firingPoing = transform.Find("BombFiringPoint");
        rocketSwapningPosition = transform.Find("RocketFiringPoint");
    }


    void Update()
    {
        if(IsInCameraView() && !isLaunched){
            isLaunched = true;
            InvokeRepeating(nameof(LaunchRocket), 0f , 3.4f);
        }
    }

    private void LaunchRocket(){
        if(rocket != null && rocketSwapningPosition != null){
            GameObject RocketGameObject = Instantiate(rocket, rocketSwapningPosition.position, rocketSwapningPosition.rotation);
            Rocket rocketObject = RocketGameObject.GetComponent<Rocket>();
            rocketObject.player = player;
        }
        
    }

    private void FireBomb()
    {
        if (bombPrefab != null && firingPoing != null)
        {
            GameObject bomb = Instantiate(bombPrefab, firingPoing.position, firingPoing.rotation);
        }
    }

    private bool IsInCameraView()
    {
        Vector3 cameraView = mainCamera.WorldToViewportPoint(transform.position);
        return cameraView.x >= 0 && cameraView.x <= 1 && cameraView.y >= 0 && cameraView.y <= 1 && cameraView.z > 0;
    }
}
