using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Icon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(GameObject.Find("MiniMap").transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FollowTransform(Transform target)
    {
        StartCoroutine(Following(target));

    }
    
    IEnumerator Following(Transform target)
    {
        Vector3 pos = Camera.allCameras[0].WorldToViewportPoint(target.position);

        pos.x = pos.x * 200.0f - 100f;
        pos.y = pos.y * 200.0f - 100f;

        this.transform.localPosition = pos;

        yield return null;

    }
}
