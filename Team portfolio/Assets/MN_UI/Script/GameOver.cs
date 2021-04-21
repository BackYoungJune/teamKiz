using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject myPanel;

    private void Awake()
    {
        myPanel = GameObject.Find("GameOver_Panel");
        myPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(MN_UIManager.Instance.CurrentHealth < Mathf.Epsilon)
        {
            myPanel.SetActive(true);
        }
    }
    public void RestartButton()
    {
        //SceneManager.LoadScene("MN_StartMenu");
        SceneManager.LoadScene("MN_StartMenu");
    }
    public void QuitButton()
    {
        Debug.Log("아직 기능 없음");
    }
}
