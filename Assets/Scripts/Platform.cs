using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxHeight = -5.58f;
    public float minHeight = -6.64f;

    private float direction = 1f;
    private float currentHeight;

    void Start() {
        currentHeight = transform.position.y;
    }

    void Update() {

        currentHeight += speed * direction * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);


        if (currentHeight >= maxHeight) {
            direction = -1f;
        } else if (currentHeight <= minHeight) {
            direction = 1f;
        }
    }
}
