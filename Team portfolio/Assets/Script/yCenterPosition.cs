using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yCenterPosition : MonoBehaviour
{
    Vector3 Center = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Center = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        transform.position = Center;
    }
}
