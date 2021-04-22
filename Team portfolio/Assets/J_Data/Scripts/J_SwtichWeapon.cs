using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SwtichWeapon : MonoBehaviour
{
    public enum HOLDING_WEAPON
    {
        FIST,
        AXE,
        GUN,
        GRENADE
    }

    [SerializeField]
    public HOLDING_WEAPON myWeapon = HOLDING_WEAPON.AXE;

    [SerializeField]
    GameObject weaponIcon;

    yPlayerInput playerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInParent<yPlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //StateProcess();

        if (playerInput.swap0)
        {
            Debug.Log("FIST");
            GetComponent<yPlayerAxe>().enabled = false;
            GetComponent<yPlayerShooter>().enabled = false;
            GetComponent<yPlayerGrenade>().enabled = false;
            ChangeState(HOLDING_WEAPON.FIST);
        }
        if (playerInput.swap1)
        {
            Debug.Log("AXE");
            GetComponent<yPlayerShooter>().enabled = false;
            GetComponent<yPlayerGrenade>().enabled = false;
            ChangeState(HOLDING_WEAPON.AXE);
        }
        if (playerInput.swap2)
        {
            Debug.Log("GUN");
            GetComponent<yPlayerAxe>().enabled = false;
            GetComponent<yPlayerGrenade>().enabled = false;
            ChangeState(HOLDING_WEAPON.GUN);
        }
        if (playerInput.swap3)
        {
            Debug.Log("GRENADE");
            GetComponent<yPlayerAxe>().enabled = false;
            GetComponent<yPlayerShooter>().enabled = false;
            ChangeState(HOLDING_WEAPON.GRENADE);
        }

    }

    void ChangeState(HOLDING_WEAPON s)
    {
        if (myWeapon == s) return;
        myWeapon = s;
        switch(myWeapon)
        {
            case HOLDING_WEAPON.FIST:
                break;
            case HOLDING_WEAPON.AXE:
                GetComponent<yPlayerAxe>().enabled = true;
                // UI 이미지 변경
                break;
            case HOLDING_WEAPON.GUN:
                GetComponent<yPlayerShooter>().enabled = true;
                // UI 이미지 변경    
                break;
            case HOLDING_WEAPON.GRENADE:
                GetComponent<yPlayerGrenade>().enabled = true;
                break;
        }
    }

    //void StateProcess()
    //{
    //    switch (myWeapon)
    //    {
    //        case HOLDING_WEAPON.FIST:
                
    //            break;
    //        case HOLDING_WEAPON.AXE:
                
    //            break;
    //        case HOLDING_WEAPON.GUN:
                
    //            break;
    //        case HOLDING_WEAPON.GRENADE:
                
    //            break;
    //    }
    //}
}
