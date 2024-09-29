// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.InteropServices.WindowsRuntime;
// using Unity.VisualScripting;

// using System.Numerics;
// using System.Collections;
// using System.Diagnostics;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


// using UnityEngine.UIElements;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 0.8f;

    [SerializeField]
    private float jumpForce = 2.5f;

    private float moveX, moveY;

    // private float horizontalMovement;
    private Rigidbody2D playerBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private string WALK_ANIMATION = "walk";
    private bool isOnSlope = false;
    private float slopeAngle; 
    private BoxCollider2D boxCollider2D;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform firingPoint;
    // [Range(0.1f,1f)]
    // [SerializeField] private float firingRate = 0.5f;
    // public Transform capsule;

    public Vector2 standingSize = new Vector2(0.22933f,0.54f); // Size of the collider when standing
    public Vector2 crouchingSize = new Vector2(0.2293333f, 0.2349862f); // Size of the collider when crouching
    public Vector2 standingOffset = new Vector2(-0.08033f, 0); // Offset of the collider when standing
    public Vector2 crouchingOffset = new Vector2(-0.08033f, -0.15250f);
    public Vector3 crouchFiring = new Vector3(0.15f, -0.1f,0);
    public Vector3 standingFiring = new Vector3(0.15f, 0.06f);
    private bool isWalking = false;
    
    private bool isGrounded = true;
    // private float minX = -7.806f;
    private float maxX = 29.781f;
    private bool isLookingUp = false;
    private bool isCrouched = false;
    private bool isLookingLeft = false;
    private float leftConstraint;
    private Camera cam; 

    private float health;
    private Transform swapntransform;
    [SerializeField] private Image healthBar;


    private void Awake(){
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        health = PlayerPrefs.GetFloat("PlayerHealth",800f);
        // ScoreManager.scoreManagerInstance.GetScoreNextLevel();
        cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        leftConstraint = height * cam.aspect;
        leftConstraint /= 2;
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        playerMovementKeyboard();
        playerAnimation();
        jumpPlayer();
        Crouch();
        LookUpward();

        if(Input.GetMouseButtonDown(1)){
            MeeleAttack();

        }
        
        if(Input.GetKeyDown(KeyCode.C)){
            ThrowGrenade();
        }

        if(Input.GetMouseButtonDown(0)){
            Shoot();
        }

        swapntransform = transform;

    }

    private void Shoot(){
        animator.SetTrigger("shoot");
        if(!isWalking){
            animator.SetTrigger("steadyShoot");
        }
        
        if(isLookingUp && isWalking){
            animator.SetTrigger("walkingShootUpward");
        }
        else if(isWalking){
            animator.SetTrigger("walkingShoot");
        }else if(isLookingUp){
            animator.SetTrigger("shootUpward");
        }
        GameObject bullet = Instantiate(bulletPrefab,firingPoint.position, firingPoint.rotation);
        bullet.tag = "PlayerBullet";
        // Get the Bullet component and set the SpriteRenderer reference
        bullet bulletScript = bullet.GetComponent<bullet>();
        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        Vector2 currentOffset = boxCollider2D.offset;
        Transform capsule = bullet.transform;
        Transform bulletTransform = bullet.transform;

        Transform capsulePosition = capsule.GetChild(0);

        if(!isLookingUp && currentOffset.x > 0){
            bulletSpriteRenderer.flipX = true;
            Vector3 currentRotation = capsulePosition.eulerAngles;
            currentRotation.z *= -1;
            capsulePosition.eulerAngles = currentRotation;
        }
        else if(isLookingUp){
            Vector3 bulletSprite =bulletTransform.rotation.eulerAngles;
            bulletSprite.z = 90;
            bulletTransform.rotation =Quaternion.Euler(bulletSprite);
        }
        if (bulletScript != null)
        {
            bulletScript.player = spriteRenderer;
        }
    }

    private void playerMovementKeyboard(){

        moveX = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveX * moveForce, playerBody.velocity.y);

        if (isOnSlope && moveX != 0)
        {
            // Calculate movement along the slope
           float slopeMove = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveForce;
            movement.y = slopeMove;
        }
        Vector2 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x + movement.x * Time.deltaTime, cam.transform.localPosition.x - leftConstraint, maxX);
        // Set the new position while keeping Y position unchanged
        transform.position = new Vector2(clampedPosition.x, transform.position.y);

        playerBody.velocity = new Vector2(movement.x, movement.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("SoldierBullet")){
            health -= 30;
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Slope"))
        {
            isOnSlope = true;
            
        }

        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("Soldier")){
            
        }
        if(collision.gameObject.CompareTag("EnemyMeele")){
            Debug.Log("EEngmey meeleeeee");
        }
        if(collision.gameObject.CompareTag("Bomb")){
            health -= 80;
        }
        if(collision.gameObject.CompareTag("Rocket")){
            health -= 80;
        }
        CheckDying();
        CalculateHealthBar();

    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("EnemyMeele")){
            health -= 20f;
        }
        if(collider.CompareTag("Bomb")){
            health -= 60;
        }
        if(collider.CompareTag("BigConga")){
            health -= 50;
        }
        if(collider.CompareTag("Car")){
            health -= 40;
        }
        if(collider.CompareTag("Fire")){
            health -= 10;
        }
        CheckDying();
        CalculateHealthBar();
    }

    IEnumerator HandleDeath(){
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("GameOver");
    }

    private void CheckDying(){
        if(health <= 0){
            animator.SetTrigger("die");
            
            if(ScoreManager.scoreManagerInstance.lives > 0){
                ScoreManager.scoreManagerInstance.lives--;
                boxCollider2D.enabled = false;
                playerBody.isKinematic = true;
                StartCoroutine(ReSwapn());
                PlayerPrefs.SetFloat("PlayerHealth", 800f);
                PlayerPrefs.Save();
            }else{
                StartCoroutine(HandleDeath());
            }

        }

    }

    IEnumerator ReSwapn(){
        yield return new WaitForSeconds(3f);
        // swapntransform = transform;
        // Destroy(gameObject, 3f);
        // StartCoroutine(HandleSwapn());
        health = 800f;
        CalculateHealthBar();
        animator.SetTrigger("swapn");
        Vector3 postion = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
        transform.position = postion;
        boxCollider2D.enabled = true;
        playerBody.isKinematic = false;
    }

    // IEnumerator HandleSwapn(){
    //     yield return new WaitForSeconds(2f);
    //     Instantiate(gameObject, swapntransform.position, swapntransform.rotation);
    // }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slope"))
        {
            isOnSlope = false;
        }
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }

    void CalculateHealthBar(){
        healthBar.fillAmount = health / 800;
    }

    // private IEnumerator Delay(float duration)
    // {
    //     yield return new WaitForSeconds(duration);
    // }

    private void MeeleAttack(){
        if(isWalking){
            animator.SetTrigger("MeeleWalking");
        }else{
            animator.SetTrigger("Meele");
        }
        
    }

    private void playerAnimation(){
        
        if(moveX > 0 ){
            isWalking = true;
            animator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = false;
            isLookingLeft = false;
            Vector2 currentOffset = boxCollider2D.offset;
            if(currentOffset.x > 0)
                currentOffset.x *= -1;
            boxCollider2D.offset = currentOffset;
            if(firingPoint.localPosition.x < 0){
            Vector2 firingPosition = firingPoint.localPosition;
            firingPosition.x *= -1;
            firingPoint.localPosition = firingPosition;
            }
        }else if ( moveX < 0){
            isWalking = true;
            animator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = true;
            isLookingLeft = true;
            Vector2 currentOffset = boxCollider2D.offset;
            if(currentOffset.x < 0)
                currentOffset.x *= -1;
            boxCollider2D.offset = currentOffset;
            if(firingPoint.localPosition.x > 0){
            Vector2 firingPosition = firingPoint.localPosition;
            firingPosition.x *= -1;
            firingPoint.localPosition = firingPosition;
            }
        }else{
            animator.SetBool(WALK_ANIMATION, false);
            isWalking = false;
        }
        
    }

    private void jumpPlayer(){

        if(Input.GetButtonDown("Jump") && (isGrounded || isOnSlope) && !isCrouched){
            playerBody.AddForce(new UnityEngine.Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Crouch(){
        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouched = true;
            moveForce = 0.5f;
            animator.SetBool("crouch", true);
            SetColliderSize(crouchingSize, crouchingOffset);
            // SetFiringPosition(crouchFiring);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouched = false;
            moveForce = 0.8f;
            animator.SetBool("crouch", false);
            SetColliderSize(standingSize, standingOffset);
            // SetFiringPosition(standingFiring);
        }
    }


    private void SetColliderSize(Vector2 size, Vector2 offset)
    {
        boxCollider2D.size = size;
        boxCollider2D.offset = offset;
    }

    private void LookUpward(){
        if(Input.GetKeyDown(KeyCode.W)){
            isLookingUp = true;
            animator.SetBool("lookUpward",true);
            Vector2 firingPosition = firingPoint.localPosition;
            firingPosition.x = 0;
            firingPosition.y = 0.4f;
            firingPoint.localPosition = firingPosition;
        }
        if(Input.GetKeyUp(KeyCode.W)){
            isLookingUp = false;
            animator.SetBool("lookUpward", false);
            Vector2 firingPostion = firingPoint.localPosition;
            firingPostion = standingFiring;
            firingPoint.localPosition = firingPostion;
        }
    }

    private void ThrowGrenade(){
        if(isWalking){
            return;
        }
        animator.SetTrigger("ThrowGrenade");
        GameObject grenade = Instantiate(grenadePrefab,firingPoint.position, firingPoint.rotation);
        Rigidbody2D body = grenade.GetComponent<Rigidbody2D>();
        Vector2 vec = new Vector2(3f,0.5f);
        if(isLookingLeft){
            vec.x *= -1;
        }
        body.AddForce(vec, ForceMode2D.Impulse);
    }

}
