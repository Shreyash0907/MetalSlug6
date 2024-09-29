using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IronTank : MonoBehaviour
{
    // Start is called before the first frame update

    private float health;
    public Transform bombFiringPoint;
    public GameObject bombPrefab;
    private Animator animator, flameAnimator;
    private bool isTrasformed = false;
    private Collider2D colliderbody;
    private GameObject flame,upperbody;
    void Start()
    {
        health = 500;
        animator = GetComponent<Animator>();
        flame = transform.Find("FlameFiringPoint")?.gameObject;
        upperbody = transform.Find("1_0")?.gameObject;
        flameAnimator = flame.GetComponent<Animator>();
        colliderbody = GetComponent<PolygonCollider2D>();
        InvokeRepeating("FireBomb", 0f,2f);
    }

    private IEnumerator Flames(){
        yield return new WaitForSeconds(2f);
        if(flameAnimator != null){
            flameAnimator.SetBool("flame",false);
        }
        
    }

    private void FlameThrower(){
        flameAnimator.SetBool("flame", true);
        StartCoroutine("Flames");
    }

    private void FireBomb(){
        if(bombPrefab != null && bombFiringPoint != null){
            GameObject bomb = Instantiate(bombPrefab, bombFiringPoint.position, bombFiringPoint.rotation);
            Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();
            if (bombRigidbody != null)
            {
                Vector2 force = new Vector2(-5,1) ;
                bombRigidbody.AddForce(force, ForceMode2D.Impulse);
                bomb.tag = "Bomb";
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("PlayerBullet") ||  collision.gameObject.CompareTag("Grenade")){
            health -= collision.gameObject.CompareTag("Grenade") ? 60 : 20;
            if(health <= 400 && !isTrasformed){
                animator.SetTrigger("transform");
                isTrasformed = true;
                InvokeRepeating("FlameThrower",2f, 4f);
            }

            if(health <= 0){
                animator.SetTrigger("destroy");
                flameAnimator.SetTrigger("destroyed");
                Destroy(flame);
                Destroy(upperbody);
                Destroy(gameObject,2f);
                ScoreManager.scoreManagerInstance.UpdateScore(300);
                colliderbody.enabled = false;
                CancelInvoke(nameof(FireBomb));
                CancelInvoke(nameof(FlameThrower));
                // flameAnimator.enabled = false;
            }
        }
        
    }
}
