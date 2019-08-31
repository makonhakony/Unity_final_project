using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;
    private int cur_weapon_index;
    // Start is called before the first frame update
    void Start()
    {
        cur_weapon_index = 0;
        weapons[cur_weapon_index].gameObject.SetActive(true);

    }


    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Alpha1)){
            TurnOnSelectedWeapon(0);
        }
        if( Input.GetKeyDown(KeyCode.Alpha2)){
            TurnOnSelectedWeapon(1);
        }
        if( Input.GetKeyDown(KeyCode.Alpha3)){
            TurnOnSelectedWeapon(2);
        }
        if( Input.GetKeyDown(KeyCode.Alpha4)){
            TurnOnSelectedWeapon(3);
        }
        if( Input.GetKeyDown(KeyCode.Alpha5)){
            TurnOnSelectedWeapon(4);
        }
        if( Input.GetKeyDown(KeyCode.Alpha6)){
            TurnOnSelectedWeapon(5);
        }
    }

    void TurnOnSelectedWeapon(int index){
        if (cur_weapon_index == index)
        return;
        weapons[cur_weapon_index].gameObject.SetActive(false);
        weapons[index].gameObject.SetActive(true);
        cur_weapon_index= index;
    }

    public WeaponHandler GetCurrentSelectedWeapon(){
        return weapons[cur_weapon_index];
    }
}
