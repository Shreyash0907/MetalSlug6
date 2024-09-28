using UnityEngine;

public class Van : MonoBehaviour
{
    public GameObject gunSoldier, soldier;
    public Transform player;
    public Camera mainCamera;
    public Transform swapningPosition;
    private bool isSwapning = false;
    private float health;

    private Animator animator;
    private Collider2D colliderBody;
    // Start is called before the first frame update

    void Start(){
        health = 250f;
        animator = GetComponent<Animator>();
        colliderBody = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if(!isSwapning && IsInCameraView() ){
            isSwapning = true;
            InvokeRepeating(nameof(SwapnSoldier), 1f, 4f);
            InvokeRepeating(nameof(SwapnGunSoldier), 2f, 5f);
        }   
        
    }

    // Update is called once per frame
    void SwapnSoldier()
    {
        GameObject Soldier = Instantiate(soldier, swapningPosition.position, swapningPosition.rotation);
        Animator soldierAnimator = Soldier.GetComponent<Animator>();

        Soldier soldierObject = Soldier.GetComponent<Soldier>();

        soldierObject.player = player;

        soldierAnimator.SetTrigger("Swapn");
    }

    void SwapnGunSoldier(){
        GameObject GunSoldier = Instantiate(gunSoldier, swapningPosition.position, swapningPosition.rotation);
        Animator gunSoldierAnimator = GunSoldier.GetComponent<Animator>();
        GunSoldier gunSoldierObject = GunSoldier.GetComponent<GunSoldier>();
        gunSoldierObject.player = player;
        gunSoldierObject.mainCamera = mainCamera;

        gunSoldierAnimator.SetTrigger("Swapn");
    }


    private bool IsInCameraView()
    {
        Vector3 cameraView = mainCamera.WorldToViewportPoint(transform.position);
        return cameraView.x >= 0 && cameraView.x <= 1 && cameraView.y >= 0 && cameraView.y <= 1 && cameraView.z > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        colliderBody.enabled = true;
        if(collision.gameObject.CompareTag("PlayerBullet")){
            health -= 20;
        }
        if(collision.gameObject.CompareTag("Grenade")){
            health -= 60;
        }
        if(health < 0){
            colliderBody.enabled = false;
            animator.SetTrigger("Destroy");
            Destroy(gameObject,1.5f);
        }
    }
    
}
