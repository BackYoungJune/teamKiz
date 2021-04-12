using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Handle_Image : MonoBehaviour
{
    public List<Image> ZombieImages;
    public List<Sprite> ZombieSprites;
    public float AnimationSpeed = 0.5f;

    GameObject handle;
    Image myHandle_Image;
    Slider mySlider;

    bool StopPlus = false;
    private void Awake()
    {
        mySlider = GetComponent<Slider>();

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
        while(mySlider.value < 1f)
        {
            mySlider.value += Time.deltaTime * 0.1f;
            yield return null;
        }
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
