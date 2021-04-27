using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour
{
    Animator CameraAnim;
    yPlayerInput Input;

    yPlayerGrenade playerGrenade;
    yGrenade Grenade;

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float throwTime = 3.0f;    // 던지는 소요 시간
    float lastFireTime; // 수류탄을 마지막으로 발사한 시점

    public enum STATE
    {
        NORMAL, AIM, EXPLOSION
    }
    public STATE myState = STATE.NORMAL;

    

    // Start is called before the first frame update
    void Start()
    {
        // 사용할 컴포넌트들을 가져오기
        CameraAnim = GetComponent<Animator>();
        Input = FindObjectOfType<yPlayerInput>();
        playerGrenade = FindObjectOfType<yPlayerGrenade>();
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
            case STATE.EXPLOSION:
                StartCoroutine(GrenadeSearching());

                if (Grenade != null)
                {
                    Debug.Log("Time.time : " + Time.time);
                    Debug.Log("lastFireTime + throwTime : " + lastFireTime + throwTime);
                    if (Time.time >= lastFireTime + throwTime)
                    {
                        // 마지막 총 발사 시점 갱신
                        lastFireTime = Time.time;

                        float dist = Vector3.Distance(transform.position, Grenade.LastPosition);
                        if (dist < 5.0f)
                        {
                            CameraAnim.SetTrigger("Grenade");
                        }
                    }
                }
                ChangeState(STATE.NORMAL);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Explosion();
        
    }

    void Aim()
    {
        if (Input.aim)
        {
            ChangeState(STATE.AIM);
        }
        else
        {
            ChangeState(STATE.NORMAL);
        }
    }

    void Explosion()
    {
        if(playerGrenade.myState == yPlayerGrenade.STATE.SHOOT)
        {
            ChangeState(STATE.EXPLOSION);
        }
    }

    IEnumerator GrenadeSearching()
    {
        if (Grenade != null) yield break;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 0, LayerMask.GetMask("Grenade"));

        foreach (RaycastHit hit in rayHits)
        {
            if (hit.transform.gameObject.tag == "Grenade")
            {
                Grenade = hit.collider.GetComponent<yGrenade>();
                
            }
        }
        Debug.Log(Grenade);
        yield return new WaitForSeconds(0.5f);
    }

}
