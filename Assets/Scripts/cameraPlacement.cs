using UnityEngine;

public class cameraPlacement : MonoBehaviour
{

    private Transform player;

    private Vector3 currentPos;
    private float minX = -5.272f;
    private float maxX = 23.313f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

        currentPos = transform.position;
        currentPos.x = player.position.x;
        currentPos.y = -1.25f;
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);

        transform.position = currentPos;

    }
}
