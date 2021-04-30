using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyText_Anim : MonoBehaviour
{
    public void Awake()
    {
        GameObject TEXT = Resources.Load("MoenyMove_Text") as GameObject;
       // Instantiate(TEXT, this.transform.localScale, Quaternion.identity);

    }
    public void Update()
    {
       // GameObject TEXT = Resources.Load("MoenyMove_Text") as GameObject;
    }
    public void OnDestroyTEXT()
    {
        Destroy(this.gameObject);
    }
    
}
