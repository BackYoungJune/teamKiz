using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class WeaponChangeUI : MonoBehaviour
{
    public MouseEvent mouseEvent;
    //public float AlphaThreshold = 0.1f;

    public enum STATE
    {
       NORMAL, RIFLE,AXE, GRENADE, POTION
    }
    //처음에 캐릭터가 들고 있는 무기
    public STATE myState = STATE.RIFLE;

    //각 버튼을 파악하기 위한 state
    public enum BUTTON_NAME
    {
        RIFLE_BUTTON,AXE_BUTTON,GRNADE_BUTTON,POTION_BUTTON
    }
    public BUTTON_NAME buttonName;
    Image myImage;
    Vector3 startScale;
    Vector3 scale;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        startScale = this.transform.localScale;
        scale = new Vector3(4.5f, 9f, 1f);

        mouseEvent = GetComponent<MouseEvent>();        
        mouseEvent.MouseEnter += (PointerEventData data) => { this.transform.localScale = scale;  };
        mouseEvent.MouseExitExit += (PointerEventData data) => { this.transform.localScale = startScale; };
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.transform.localScale = startScale;
        }
      
    }
  
}
