using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SwtichWeapon : MonoBehaviour
{
    enum HOLDING_WEAPON
    {
        AXE,
        GUN,
        GRENADE
    }

    [SerializeField]
    HOLDING_WEAPON myWeapon = HOLDING_WEAPON.AXE;

    [SerializeField]
    GameObject weaponIcon;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    void ChangeState(HOLDING_WEAPON s)
    {
        if (myWeapon == s) return;
        myWeapon = s;
        switch(myWeapon)
        {
            case HOLDING_WEAPON.AXE:
                GetComponent<yPlayerAxe>().enabled = true;
                // UI 이미지 변경
                break;
            case HOLDING_WEAPON.GUN:
                GetComponent<yPlayerShooter>().enabled = true;
                // UI 이미지 변경    
                break;
            case HOLDING_WEAPON.GRENADE:
                break;
        }
    }

    void StateProcess()
    {
        switch (myWeapon)
        {
            case HOLDING_WEAPON.AXE:
                if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    GetComponent<yPlayerAxe>().enabled = false;
                    ChangeState(HOLDING_WEAPON.GUN);
                }
                break;
            case HOLDING_WEAPON.GUN:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    GetComponent<yPlayerShooter>().enabled = false;
                    ChangeState(HOLDING_WEAPON.AXE);
                }
                break;
            case HOLDING_WEAPON.GRENADE:
                break;
        }
    }
}
