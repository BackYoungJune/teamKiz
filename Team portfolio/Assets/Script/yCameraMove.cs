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
    Coroutine coroutine;

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
        if (playerGrenade.myState == yPlayerGrenade.STATE.SHOOT)
        {
            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 10.0f);

            foreach (RaycastHit hit in rayHits)
            {
                // 첫번째 Enemy와 충돌한 경우
                if (hit.transform.gameObject.tag == "Grenade")
                {
                    
                    // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
                    yGrenade target = hit.collider.GetComponent<yGrenade>();
                    if(target.LastPosition != Vector3.zero)
                    {
                        Debug.Log("Grenade");
                        StartCoroutine(Distance());
                    }

                }
            }
        }
    }

    IEnumerator Distance()
    {
        while (ShakeTime >= Mathf.Epsilon)
        {
            float dist = Vector3.Distance(Greande.LastPosition, transform.position);
            Debug.Log("Greande.LastPosition : " + Greande.LastPosition);
            Debug.Log("dist : " + dist);
            if (dist < 20.0f && ShakeTime > Mathf.Epsilon)
            {
                transform.position = (Vector3)Random.insideUnitSphere * ShakeAmount + InitPoint;
                ShakeTime -= Time.deltaTime;
            }
            Debug.Log("ShakeTime : " + ShakeTime);
            yield return null;
        }

        

        //transform.position = InitPoint;
        //ShakeTime = 1.0f;
        
    }
}
