using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCenter : MonoBehaviour
{
    Vector3 ScreenCenter = Vector3.zero;
    // Start is called before the first frame update

    void Update()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }
}
