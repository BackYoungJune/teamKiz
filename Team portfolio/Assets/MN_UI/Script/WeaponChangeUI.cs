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

    
    // Start is called before the first frame update
    bool IsClick;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        Vector3 startScale = this.transform.localScale;
        Vector3 scale = new Vector3(4.5f, 9f, 1f);

        mouseEvent = GetComponent<MouseEvent>();        
        mouseEvent.MouseEnter += (PointerEventData data) => { this.transform.localScale = scale;  };
        mouseEvent.MouseExitExit += (PointerEventData data) => { this.transform.localScale = startScale; };
    }

    void Start()
    {
    
        //int a = 0;
        //mouseEvent.MouseDown += (PointerEventData data) => { IsClick = true; };
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerEnter");
    //}

    // Update is called once per frame
    void Update()
    {

        ////이미지에 맞게 버튼의 클릭가능 크기 변경
        ////this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;

        ////라이플을 들고 도끼를 선택할 시
        //if (myState == STATE.RIFLE && IsClick && buttonName == BUTTON_NAME.AXE_BUTTON)
        //{
        //    Debug.Log("RIFLE -> AEX");
        //    ChangeState(STATE.AXE);
        //    IsClick = false;
        //}
        ////라이플을 들고 수류탄을 선택할 시
        //else if (myState == STATE.RIFLE && IsClick && buttonName == BUTTON_NAME.GRNADE_BUTTON)
        //{
        //    Debug.Log("RIFLE -> GRANADE");
        //    ChangeState(STATE.GRENADE);

        //    IsClick = false;

        //}
        ////도끼를 들고 라이플을 선택할 시
        //else if(myState == STATE.AXE && IsClick && buttonName == BUTTON_NAME.RIFLE_BUTTON)
        //{
        //    Debug.Log("AXE -> RIFLE");
        //    ChangeState(STATE.RIFLE);

        //    IsClick = false;

        //}
        ////도끼를 들고 수류탄을 선택할 시
        //else if (myState == STATE.AXE && IsClick && buttonName == BUTTON_NAME.GRNADE_BUTTON)
        //{
        //    Debug.Log("AXE -> GRANADE");
        //    ChangeState(STATE.GRENADE);

        //    IsClick = false;

        //}
        ////수류탄을 들고 라이플을 선택할 시
        //else if (myState == STATE.GRENADE && IsClick && buttonName == BUTTON_NAME.RIFLE_BUTTON)
        //{
        //    Debug.Log("GRANADE -> RIFLE");
        //    ChangeState(STATE.RIFLE);

        //    IsClick = false;
        //}
        ////수류탄을 들고 도끼를 선택할 시
        //else if (myState == STATE.GRENADE && IsClick && buttonName == BUTTON_NAME.AXE_BUTTON)
        //{
        //    Debug.Log("GRANADE -> AXE");
        //    ChangeState(STATE.AXE);

        //    IsClick = false;
        //}
        //StateProcess();
    }
    //void ChangeState(STATE s)
    //{
    //    if (s == myState) return;
    //    myState = s;

    //    switch(myState)
    //    {
    //        case STATE.RIFLE:
    //            break;
    //        case STATE.AXE:
    //            break;
    //        case STATE.GRENADE:
    //            break;
    //        case STATE.POTION:
    //            break;
    //    }
    //}
    //void StateProcess()
    //{
    //    switch (myState)
    //    {
    //        case STATE.RIFLE:
    //            Debug.Log("RIFLE");
    //            break;
    //        case STATE.AXE:
    //            Debug.Log("AXE");

    //            break;
    //        case STATE.GRENADE:
    //            Debug.Log("GRENADE");

    //            break;
    //        case STATE.POTION:
    //            Debug.Log("RIFLE");

    //            break;
    //    }
    //}
}
