using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Catepillat : MonoBehaviour
{
    public GameObject spit;
    private Transform tongue;
    public Transform[] upperFiringPoints;
    public Transform[] lowerFiringPoints;
    private Vector3 originalPos;
    private Animator animator,tongueAnimator;
    private float health = 100f;
    void Start()
    {
        animator = GetComponent<Animator>();
        tongue = transform.GetChild(0);
        tongueAnimator = tongue.GetComponent<Animator>();
        originalPos = transform.position;
        StartCoroutine(BackAndForthMovement());
        upperFiringPoints = new Transform[4];
        lowerFiringPoints = new Transform[4];
        upperFiringPoints[0] = transform.Find("UpperSpit/Point1");
        upperFiringPoints[1] = transform.Find("UpperSpit/Point2");
        upperFiringPoints[2] = transform.Find("UpperSpit/Point3");
        upperFiringPoints[3] = transform.Find("UpperSpit/Point4");
        lowerFiringPoints[1] = transform.Find("LowerSpit/Point1");
        lowerFiringPoints[2] = transform.Find("LowerSpit/Point2");
        lowerFiringPoints[3] = transform.Find("LowerSpit/Point3");
        lowerFiringPoints[0] = transform.Find("LowerSpit/Point4");
        InvokeRepeating("FireLowerSpit",3f, 4.5f);
        InvokeRepeating("FireUpperSpit",5f, 4.5f);
        StartCoroutine(tongueOutTrigger());
    }

    IEnumerator tongueOutTrigger(){
        yield return new WaitForSeconds(2.4f);
        if(tongueAnimator != null){
            tongueAnimator.SetTrigger("TongueOut");
        }
    }
    IEnumerator BackAndForthMovement()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(originalPos.x - 0.4f));
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(MoveToPosition(originalPos.x));
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator MoveToPosition(float targetX)
    {
        animator.SetBool("isWalking",true);
        while (Mathf.Abs(transform.position.x - targetX) > 0.01f)
        {
            float step = 0.3f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), step);
            yield return null;
        }
        animator.SetBool("isWalking",false);
    }

    IEnumerator Delaylower(){
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(FireLowerSpitdelay());
    }
    private void FireLowerSpit(){
        tongueAnimator.SetTrigger("lowerSpit");
        StartCoroutine(Delaylower());
    }
    IEnumerator Delayupper(){
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(FireUpperSpitdelay());
    }
    private void FireUpperSpit(){
        tongueAnimator.SetTrigger("upperSpit");
        StartCoroutine(Delayupper());
    }

    private IEnumerator FireLowerSpitdelay()
    {
        
        foreach (Transform firingPoint in lowerFiringPoints)
        {
            if (spit != null)
            {
                Instantiate(spit, firingPoint.position, firingPoint.rotation);
                yield return new WaitForSeconds(0.2f); 
            }
        }

    }

    private IEnumerator FireUpperSpitdelay()
    {
        
        foreach (Transform firingPoint in upperFiringPoints)
        {
            if (spit != null)
            {
                GameObject Spit = Instantiate(spit, firingPoint.position, firingPoint.rotation);
                Rigidbody2D spitBody = Spit.GetComponent<Rigidbody2D>();
                SpitBall spitScript = Spit.GetComponent<SpitBall>();
                if (spitBody != null)
                {
                    spitBody.gravityScale = 0.32f;
                    Vector2 direction = new Vector2(-2.5f, 1f);
                    float forceMagnitude = 1f;
                    spitBody.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
                }
                if (spitScript != null)
                {
                    spitScript.makeUpper();
                }
                yield return new WaitForSeconds(0.2f); 
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("PlayerBullet")){
            health -= 25;
            Destroy(collider);
        }
        if(collider.gameObject.CompareTag("Grenade")){
            health -= 150;
            Destroy(collider,0.1f);
        }
        if(health <= 0){
            animator.SetTrigger("dead");
            tongueAnimator.SetTrigger("dead");
            
            Destroy(gameObject,1.5f);
        }
    }

}
