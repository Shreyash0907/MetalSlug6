using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody2D body;
    private CircleCollider2D circleCollider;
    private Animator animator;
    void Start()
    {
       
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Sleep());
        
    }
    IEnumerator Sleep()
    {
        Debug.Log("sleeps");
        yield return new WaitForSeconds(10f);
    }
    void OnCollisionEnter2D(Collision2D collision){
        // if(!collision.gameObject.CompareTag("player")){
            Debug.Log("grenade");
            circleCollider.radius = 0.4f;
            body.isKinematic = true;
            circleCollider.isTrigger = true;
            animator.SetTrigger("explode");
            Destroy(gameObject,0.1f);
        // }

    }

    
}
