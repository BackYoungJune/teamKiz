using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour
{
    Animator CameraAnim;
    yPlayerInput Input;

    J_Barrel Barrel;

    public enum STATE
    {
        NORMAL, AIM, Shake
    }
    public STATE myState = STATE.NORMAL;

    

    // Start is called before the first frame update
    void Start()
    {
        // 사용할 컴포넌트들을 가져오기
        CameraAnim = GetComponent<Animator>();
        Input = FindObjectOfType<yPlayerInput>();
    }

    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch(myState)
        {
            case STATE.NORMAL:
                CameraAnim.SetBool("aim", false);
                break;
            case STATE.AIM:
                CameraAnim.SetBool("aim", true);
                break;
            case STATE.Shake:
                CameraAnim.SetTrigger("Shake");
                break;
        }
    }
}
