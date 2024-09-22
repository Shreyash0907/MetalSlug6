using UnityEngine;

public class cameraPlacement : MonoBehaviour
{

    private Transform player;

    private Vector3 currentPos;
    private float minX = -6f;
    private float maxX = 23.213f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

        currentPos = transform.position;
        if(currentPos.x < player.position.x){
            currentPos.x = player.position.x;
        }
        currentPos.y = -1.25f;
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);

        transform.position = currentPos;

    }
}
