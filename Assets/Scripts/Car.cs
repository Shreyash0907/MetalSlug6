using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed = 5f;
    public float waitTime =1f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        StartCoroutine(MoveCar());
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator MoveCar()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(start.position));
            // StartCoroutine(ChangeSide(1));
            animator.SetTrigger("turn");
            yield return new WaitForSeconds(waitTime);
            spriteRenderer.flipX = true;
            
            // animator.SetTrigger("turn");
            
            yield return StartCoroutine(MoveToPosition(end.position));
            // StartCoroutine(ChangeSide(0));
            animator.SetTrigger("turn");
            yield return new WaitForSeconds(waitTime);
            spriteRenderer.flipX = false;
            // animator.SetTrigger("turn");
            

        }
    }

    IEnumerator ChangeSide(int flag){
        animator.SetTrigger("turn");
        yield return new WaitForSeconds(0.8f);
        if(flag == 1){
            
        }else{
            spriteRenderer.flipX = false;
        }
        
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.1f)
        {
            Vector3 newPosition = new Vector3(Mathf.MoveTowards(transform.position.x, targetPosition.x, speed * Time.deltaTime), transform.position.y, transform.position.z );
            transform.position = newPosition;
            yield return null;
        }
    }
}
