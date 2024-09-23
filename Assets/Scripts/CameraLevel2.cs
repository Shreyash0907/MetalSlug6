using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevel2 : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
        // transform.position = player.position - offset;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        transform.position = newPosition;
    }
}
