using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBall : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody2D body;
    public GameObject capsuleObject;
    private Transform capsule;
    private Animator animator;
    private Collider2D colliderBody;
    private bool isUpper = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliderBody = GetComponent<CircleCollider2D>();
        capsule = capsuleObject.transform;
    }
    public void makeUpper(){
        isUpper = true;
    }
    void Update()
    {
        if(capsule && !isUpper)
        {   
            Vector2 direction = capsule.up; 
            body.velocity = direction * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("explode");
        body.velocity = Vector2.zero;
        body.isKinematic = true;
        if(isUpper){
            body.AddForce(new Vector2(0,0), ForceMode2D.Impulse);
        }
        colliderBody.enabled = false;
        Destroy(capsuleObject);
        Destroy(gameObject,1f);
    }

}
