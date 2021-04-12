using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerInput : MonoBehaviour
{
    public string xAxisName = "Horizontal";     // 좌우 움직임 입력 이름
    public string yAxisName = "Vertical";     // 앞뒤 움직임 입력 이름
    public string fireButtonName = "Fire1";         // 발사 입력버튼 이름
    public string reloadButtonName = "Reload";      // 재장전 입력버튼 이름
    //public string hitButtonName = "Hit";          // 타격시 입력버튼 이름
    public string aimButtonName = "Aim";            // 에임 조준시 입력버튼 이름
    
    // public string swap1ButtonName = "Swap1";
    // public string swap2ButtonName = "Swap2";
    // public string swap3ButtonName = "Swap3";

    public float xMove { get; private set; }    // 감지된 좌우 움직임 입력값
    public float yMove { get; private set; }    // 감지된 좌우 움직임 입력값
    public bool walk { get; private set; }      // 감지된 걷기 입력값
    public bool reload { get; private set; }    // 감지된 재장전 입력값
    public bool fire { get; private set; }     // 감지된 발사 입력값
    public bool jump { get; private set; }      // 감지된 점프 입력값
    public bool aim { get; private set; }       // 감지된 에임조준 입력값
    

    bool isDead = false;    // 나중에 게임매니저로 옮겨야댐

    void Update()
    {
        if(!isDead)
        {
            // 좌우에 관한 입력감지
            xMove = Input.GetAxis(xAxisName);
            // 상하에 관한 입력감지
            yMove = Input.GetAxis(yAxisName);
            // 걷기에 관한 입력감지
            walk = Input.GetKey(KeyCode.LeftShift);
            // 재장전 입력감지
            reload = Input.GetButtonDown(reloadButtonName);
            // 발사 입력감지
            fire = Input.GetButton(fireButtonName);
            // 점프 입력감지
            jump = Input.GetKeyDown(KeyCode.Space);
            // 에임조준 입력감지
            //aim = Input.GetButton(aimButtonName);
        }
    }
}
