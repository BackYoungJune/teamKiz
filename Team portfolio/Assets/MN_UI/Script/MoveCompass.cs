using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCompass : MonoBehaviour
{
    public float turnSpeed = 5f;
    public float offset = 0;
    public CanvasRenderer canvasrenerder;
    Image myImage;
    GameObject Player;
    //public Texture albedotexture;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player") as GameObject;
       // Player = GameObject.Find("Player_1") as GameObject;

        canvasrenerder = GetComponent<CanvasRenderer>();
        //canvasrenerder.GetMaterial(0).SetTextureOffset("_MainTex", new Vector2(0f, 0f));

    }

    // Update is called once per frame
    void Update()
    {
        if(MN_UIManager.Instance.CurrentHealth < Mathf.Epsilon)
        {
            Destroy(GameObject.Find("Compass_Canvas"));
        }
        if(canvasrenerder != null && canvasrenerder.GetMaterial(0) != null)
        {
            offset = Player.transform.rotation.eulerAngles.y * 0.001388f;

            canvasrenerder.GetMaterial(0).SetTextureOffset("_MainTex", new Vector2(offset, 0f));

        }

    }
}
