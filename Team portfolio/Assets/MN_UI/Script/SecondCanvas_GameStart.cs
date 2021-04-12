using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondCanvas_GameStart : MonoBehaviour
{
    bool IsOnGameStart = false;
    GameObject secondCanvas;
    Image secondImage;


    
    private void Awake()
    {
        
        secondCanvas = GameObject.Find("Second");
        secondCanvas.SetActive(false);
        secondImage = secondCanvas.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(IsOnGameStart)
        {
           // if (secondCanvas) return;
            

            secondCanvas.SetActive(true);
            //StopAllCoroutines();
            StartCoroutine(BackgroundAlphaPlus());
        }
    }
    public void OnStartGame()
    {
        IsOnGameStart = true;
    }

    IEnumerator BackgroundAlphaPlus()
    {
        Color col = secondImage.color;
        col = new Color(secondImage.color.r, secondImage.color.g, secondImage.color.b, 0f);


        while (secondImage.color.a < 1f)
        {
            col.a += Time.deltaTime * 0.8f;
            secondImage.color = col;

            yield return null;
        }
         
        

    }
}
