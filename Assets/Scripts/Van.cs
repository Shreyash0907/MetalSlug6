using UnityEngine;

public class Van : MonoBehaviour
{
    public GameObject gunSoldier, soldier;
    public Transform player;
    public Camera mainCamera;
    public Transform swapningPosition;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SwapnSoldier", 1f, 4f);
        InvokeRepeating("SwapnGunSoldier", 2f, 5f);
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
}
