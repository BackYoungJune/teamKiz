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
    public HOLDING_WEAPON myWeapon = HOLDING_WEAPON.FIST;

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
        StateProcess();
        Debug.Log(myWeapon);
        //if (playerInput.swap0)
        //{
        //    ChangeState(HOLDING_WEAPON.FIST);
        //}
        //if (playerInput.swap1)
        //{
        //    ChangeState(HOLDING_WEAPON.AXE);
        //}
        //if (playerInput.swap2)
        //{
        //    ChangeState(HOLDING_WEAPON.GUN);
        //}
        //if (playerInput.swap3)
        //{
        //    ChangeState(HOLDING_WEAPON.GRENADE);
        //}

    }

    public void ChangeState(HOLDING_WEAPON s)
    {
        if (myWeapon == s) return;
        myWeapon = s;
        switch(myWeapon)
        {
            case HOLDING_WEAPON.FIST:
                GetComponent<yPlayerAxe>().enabled = false;
                GetComponent<yPlayerShooter>().enabled = false;
                GetComponent<yPlayerGrenade>().enabled = false;
                break;
            case HOLDING_WEAPON.AXE:
                GetComponent<yPlayerShooter>().enabled = false;
                GetComponent<yPlayerGrenade>().enabled = false;
                GetComponent<yPlayerAxe>().enabled = true;
                // UI 이미지 변경
                break;
            case HOLDING_WEAPON.GUN:
                GetComponent<yPlayerAxe>().enabled = false;
                GetComponent<yPlayerGrenade>().enabled = false;
                GetComponent<yPlayerShooter>().enabled = true;
                // UI 이미지 변경    
                break;
            case HOLDING_WEAPON.GRENADE:
                GetComponent<yPlayerAxe>().enabled = false;
                GetComponent<yPlayerShooter>().enabled = false;
                GetComponent<yPlayerGrenade>().enabled = true;
                break;
        }
    }

    void StateProcess()
    {
        switch (myWeapon)
        {
            case HOLDING_WEAPON.FIST:
                if (playerInput.swap1)  { ChangeState(HOLDING_WEAPON.AXE); }
                if (playerInput.swap2)  { ChangeState(HOLDING_WEAPON.GUN); }
                if (playerInput.swap3)  { ChangeState(HOLDING_WEAPON.GRENADE); }
                break;
            case HOLDING_WEAPON.AXE:
                if (playerInput.swap0)  { ChangeState(HOLDING_WEAPON.FIST); }
                if (playerInput.swap2) { ChangeState(HOLDING_WEAPON.GUN); }
                if (playerInput.swap3) { ChangeState(HOLDING_WEAPON.GRENADE); }
                break;
            case HOLDING_WEAPON.GUN:
                if (playerInput.swap0) { ChangeState(HOLDING_WEAPON.FIST); }
                if (playerInput.swap1) { ChangeState(HOLDING_WEAPON.AXE); }
                if (playerInput.swap3) { ChangeState(HOLDING_WEAPON.GRENADE); }
                break;
            case HOLDING_WEAPON.GRENADE:
                if (playerInput.swap0) { ChangeState(HOLDING_WEAPON.FIST); }
                if (playerInput.swap1) { ChangeState(HOLDING_WEAPON.AXE); }
                if (playerInput.swap2) { ChangeState(HOLDING_WEAPON.GUN); }
                break;
        }
    }
}
