using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_manager;
    public float firerate = 15f;
    private float nextTimetoFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;

    private bool Zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_prefab , spear_prefab;
    [SerializeField]
    private Transform Arrow_spear_startpos;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tag.LOOK_ROOT).transform.Find(Tag.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair =GameObject.FindWithTag(Tag.CROSSHAIR);

        mainCam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAnfOut();
    }

    void WeaponShoot(){
        if (weapon_manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.Multiple){
            if(Input.GetMouseButton(0)&& Time.time >nextTimetoFire){
                nextTimetoFire = Time.time +1f/firerate;

                weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();
            }
        } else {
            if (Input.GetMouseButtonDown(0)){
                if (weapon_manager.GetCurrentSelectedWeapon().tag == Tag.AXE_TAG){
                    weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                if (weapon_manager.GetCurrentSelectedWeapon().bullettype == WeaponBulletType.BULLET){
                    weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
                else {
                    if(Zoomed){
                        weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                        if (weapon_manager.GetCurrentSelectedWeapon().bullettype == WeaponBulletType.ARROW){
                            //throw arrow
                            ThrowArrowOrSpear(true);

                        }
                        else if (weapon_manager.GetCurrentSelectedWeapon().bullettype == WeaponBulletType.SPEAR){
                            //throw spear
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }

    void ZoomInAnfOut(){
        if (weapon_manager.GetCurrentSelectedWeapon().weapon_aim == WeaponAim.AIM){
            if (Input.GetMouseButtonDown(1)){
                zoomCameraAnim.Play(AnimationTag.ZOOM_IN_ANIM);
                crosshair.SetActive(false);

                
            }
            if (Input.GetMouseButtonUp(1)){
                zoomCameraAnim.Play(AnimationTag.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);

                
            }
        }
        if(weapon_manager.GetCurrentSelectedWeapon().weapon_aim == WeaponAim.SELF_AIM){
            if (Input.GetMouseButtonDown(1)){
                weapon_manager.GetCurrentSelectedWeapon().Aim(true);
                Zoomed = true;
            }
        }
        if (Input.GetMouseButtonUp(1)){
                weapon_manager.GetCurrentSelectedWeapon().Aim(false);
                Zoomed = false;
            }
    }

    void ThrowArrowOrSpear (bool throwArrow){
        if (throwArrow){
            GameObject arrow = Instantiate(arrow_prefab);
            arrow.transform.position = Arrow_spear_startpos.position;
            arrow.GetComponent<ArrowAndBow>().Launch(mainCam);
        }
        else {
            GameObject spear = Instantiate(spear_prefab);
            spear.transform.position = Arrow_spear_startpos.position;
            spear.GetComponent<ArrowAndBow>().Launch(mainCam);
        }
    }

    void BulletFired(){
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)){
            print ("we hit: "+ hit.transform.gameObject.name );
            if(hit.transform.tag == Tag.ENEMY_TAG) {
                
                hit.transform.GetComponent<PlayerHealth>().ApplyDamage(damage);
            }

        }
    }
}
