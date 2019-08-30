using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim {
    NONE,
    SELF_AIM, // for bow spear
    AIM,
}

public enum WeaponFireType {
    Single,
    Multiple
}

public enum WeaponBulletType{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;
    public WeaponAim weapon_aim;
    [SerializeField]
    private GameObject Flash_shoot;
    [SerializeField]
    private AudioSource ShootSound, ReloadSound;

    public WeaponFireType fireType;

    public WeaponBulletType bullettype;

    public GameObject attack_point;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        anim =GetComponent<Animator>();
    }

    public void ShootAnimation(){
        anim.SetTrigger(AnimationTag.SHOOT_TRIGGER);
    }
    
    public void Aim (bool canAnim){
        anim.SetBool(AnimationTag.AIM_PARAMETER, canAnim);
    }

    void Turn_On_MuzzleFlash(){
        Flash_shoot.SetActive(true);
    }
    void Turn_Off_MuzzleFlash(){
        Flash_shoot.SetActive(false);
    }

    void Play_ShootSound(){
        ShootSound.Play();

    }
    void Play_ReloadSound(){
        ReloadSound.Play();
    }

    void Turn_On_AttackPoint(){
        attack_point.SetActive(true);
    }
    void Turn_Off_AttackPoint(){
        if (attack_point.activeInHierarchy){
            attack_point.SetActive(false);
        }
    }
}
