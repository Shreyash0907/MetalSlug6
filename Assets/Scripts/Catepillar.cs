using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Catepillat : MonoBehaviour
{
    public GameObject spit;
    public Transform[] upperFiringPoints;
    public Transform[] lowerFiringPoints;
    private Vector3 originalPos;
    private Animator animator,tongueAnimator;
    private bool isTongueOut = false, hasFired = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        tongueAnimator = GetComponentInChildren<Animator>();
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
        InvokeRepeating("FireUpperSpit",0f, 4f);
    }

    IEnumerator BackAndForthMovement()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(originalPos.x - 0.4f));
            yield return new WaitForSeconds(3f);
            if(!isTongueOut && tongueAnimator != null){
                tongueAnimator.SetTrigger("TongueOut");
                isTongueOut = true;
            }
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

    IEnumerator Delay(float duration){
        yield return new WaitForSeconds(duration);
        hasFired = false;
    }
    private void FireUpperSpit(){
        foreach (Transform firingPoint in upperFiringPoints)
        {
            if(spit!= null){
                hasFired = true;
                Debug.Log("hfdsfs");
                Instantiate(spit, firingPoint.position, firingPoint.rotation);
                StartCoroutine(Delay(0.05f));
            }
        }
    }
}
