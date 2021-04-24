using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCameraMove : MonoBehaviour
{
    Animator CameraAnim;
    yPlayerInput Input;

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

    
}
