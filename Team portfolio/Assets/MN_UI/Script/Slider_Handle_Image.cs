using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slider_Handle_Image : MonoBehaviour
{
    public List<Image> ZombieImages;
    public List<Sprite> ZombieSprites;
    public float AnimationSpeed = 0.5f;
    MN_UIManager instance;

    GameObject handle;
    Image myHandle_Image;
    Slider mySlider;
    Text childText;

    bool StopPlus = false;
    private void Awake()
    {
        mySlider = GetComponent<Slider>();
        childText = GetComponentInChildren<Text>();
        GameObject obj = GameObject.Find("Handle");

        myHandle_Image = obj.GetComponent<Image>();
        StartCoroutine(SpriteZombie());
        StartCoroutine(SliderPlus());

    }

    // Update is called once per frame
    void Update()
    {
        
       // StartCoroutine(SpriteZombie());
       
    }
    IEnumerator SliderPlus()
    {
        while (mySlider.value < 1f)
        {
            mySlider.value += Time.deltaTime * 0.1f;
            float value = mySlider.value * 100f;
            childText.text = (int)value + "%";
            yield return null;
        }
        //yGameManager.instance.RestartGame();
        SecondCanvas_GameStart second = FindObjectOfType<SecondCanvas_GameStart>();

        if (second.IsOnGameStart)
        {
            J_DataManager j_data = FindObjectOfType<J_DataManager>();

            if (!J_DataManager.instance.IsNone)
            {
                Debug.Log("data Deleta !!");
                j_data.DeleteItemData();
                j_data.DeletePlayData();

            }

            SceneManager.LoadScene("PlayScene");
        }
        else if(second.IsOnLoadStart)
            SceneManager.LoadScene("PlayScene");

        StopPlus = true;
    }
    IEnumerator SpriteZombie()
    {
        if(!StopPlus)
        {
            for(int i = 0;i<ZombieImages.Count;i++)
            {
                myHandle_Image.sprite = ZombieSprites[i];//ZombieImages[i].sprite;
                yield return new WaitForSeconds(AnimationSpeed);
            }
            StartCoroutine(SpriteZombie());

        }

    }
}
