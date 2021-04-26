using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour
{
    Animator CameraAnim;
    yPlayerInput Input;
    yGrenade Grenade;
    // Start is called before the first frame update
    void Start()
    {
        // 사용할 컴포넌트들을 가져오기
        CameraAnim = GetComponent<Animator>();
        Input = FindObjectOfType<yPlayerInput>();
        
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
            CameraAnim.SetBool("aim", true);
        }
        else
        {
            CameraAnim.SetBool("aim", false);
        }
    }

    void Explosion()
    {

    }

    IEnumerator GrenadeSearching()
    {
        if (Grenade) yield break;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 0, LayerMask.GetMask("Grenade"));

        foreach (RaycastHit hit in rayHits)
        {
            if (hit.transform.gameObject.tag == "Grenade")
            {
                Grenade = hit.collider.GetComponent<yGrenade>();
            }
        }

       yield return new WaitForSeconds(1.0f);
    }

}
