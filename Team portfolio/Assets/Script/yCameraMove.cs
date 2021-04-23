using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour
{
    Animator CameraAnim;
    yPlayerInput Input;
    yGrenade Greande;
    yPlayerGrenade playerGrenade;

    public float ShakeAmount = 0.3f;
    public float ShakeTime = 1.0f;
    Vector3 InitPoint;

    // Start is called before the first frame update
    void Start()
    {
        // 사용할 컴포넌트들을 가져오기
        CameraAnim = GetComponent<Animator>();
        Input = FindObjectOfType<yPlayerInput>();
        playerGrenade = FindObjectOfType<yPlayerGrenade>();

        // 초기 위치를 현재 위치로 초기화한다
        InitPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shake();
    }

    void Aim()
    {
        if (Input.aim)
        {
            CameraAnim.SetBool("aim", true);
        }
        else
        {
            CameraAnim.SetBool("aim", false);
        }
    }

    void Shake()
    {
        if(playerGrenade.myState == yPlayerGrenade.STATE.SHOOT)
        {
            Greande = FindObjectOfType<yGrenade>();
            Invoke("Distance", 2.0f);
        }

       
    }

    void Distance()
    {
        float dist = Vector3.Distance(Greande.LastPosition, transform.position);
        //Debug.Log(dist);
        Debug.Log(transform.position);
        if (dist < 20.0f && ShakeTime > Mathf.Epsilon)
        {
            transform.position = (Vector3)Random.insideUnitSphere * ShakeAmount + InitPoint;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0f;
            transform.position = InitPoint;
        }
    }
}
