using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossStage_UI_Manager : MonoBehaviour
{
    MouseEvent mouseEvent;

    GameObject GameOverUI;
    GameObject GameWin;

    public AudioSource audiosource;
    public AudioClip AUDIOGameOver;
    public AudioClip AUDIOHit;
    

    
    
    public enum STATE
    {
        GAMESTART,PLAY,PLAYER_DEAD,BOSS_DEAD
    }
    public STATE myState = STATE.GAMESTART;

    private void Awake()
    {
        GameOverUI = GameObject.Find("GameOver_UI");
        GameWin = GameObject.Find("GameWin");
        GameWin.SetActive(false);
        GameOverUI.SetActive(false);
        GameObject playerMiniMap = GameObject.Find("Player");
       

        //StateChange(STATE.PLAYER_DEAD);
        
    }

    // Update is called once per frame
    void Update()
    {
  
        if (MN_UIManager.Instance.CurrentHealth < Mathf.Epsilon)
        {
            StateChange(STATE.PLAYER_DEAD);

        }

        if (MN_UIManager.Instance.WinGame)
        {

            StateChange(STATE.BOSS_DEAD);

        }

    }

    void StateChange(STATE s)
    {
        if (s == myState) return;
        myState = s;

        switch(myState)
        {
            case STATE.GAMESTART:
                break;
            case STATE.PLAY:
                break;
            case STATE.PLAYER_DEAD:
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    audiosource.PlayOneShot(AUDIOGameOver);

                    GameObject allclose1 = GameObject.Find("AllClose");
                    GameObject ItemCanvas = GameObject.Find("Item_Canvas");
                    ItemCanvas.SetActive(false);

                    allclose1.SetActive(false);

                    GameOverUI.SetActive(true);
                    //GameObject Item_Canvas = GameObject.Find("Item_Canvas");

                }
                break;
            case STATE.BOSS_DEAD:
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    audiosource.PlayOneShot(AUDIOGameOver);

                    GameObject allclose2 = GameObject.Find("AllClose");
                    GameObject ItemCanvas = GameObject.Find("Item_Canvas");
                    ItemCanvas.SetActive(false);

                    //ItemCanvas.SetActive(false);
                    allclose2.SetActive(false);

                    GameWin.SetActive(true);
                

                }
                break;
        }
    }
    //void StateProcess()
    //{
    //    switch (myState)
    //    {
    //        case STATE.GAMESTART:
    //            break;
    //        case STATE.PLAY:
    //            break;
    //        case STATE.PLAYER_DEAD:
    //            break;
    //        case STATE.BOSS_DEAD:
    //            break;
    //    }
    //}


    public void OnClick_RestartButton()
    {
        SceneManager.LoadScene("BossRoom");
    }
    public void OnClick_HomeButton()
    {
        SceneManager.LoadScene("MN_StartMenu");
    }
    
}
