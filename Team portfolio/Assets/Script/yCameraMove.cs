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
        CameraAnim = GetComponent<Animator>();
        Input = FindObjectOfType<yPlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.aim)
        {
            CameraAnim.SetBool("aim", true);
        }
        else
        {
            CameraAnim.SetBool("aim", false);
        }
    }
}
