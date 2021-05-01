using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuButtons: MonoBehaviour
{
    Button myButton;

    Image StartImage;
    Image ChangeImage;
    Color StartColor;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        StartImage = GetComponent<Button>().image;
        ChangeImage = GetComponentInChildren<Button>().image;
        StartColor = StartImage.color;


    }
    public void ChangeButtonENTER()
    {
        myButton.image.sprite = ChangeImage.sprite;
        myButton.image.color = new Color(1f, 0f, 0f, 1f);
    }
    public void ChangeButtonEXIT()
    {
        myButton.image.sprite = StartImage.sprite;
        myButton.image.color = StartColor;
    }
    public void QuitButtonClick()
    {
        Application.Quit();
    }

}
