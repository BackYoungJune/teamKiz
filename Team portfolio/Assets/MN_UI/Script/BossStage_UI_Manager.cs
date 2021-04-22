using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossStage_UI_Manager : MonoBehaviour
{

    GameObject GameOverUI;
    GameObject GameWin;

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

        
    }

    // Update is called once per frame
    void Update()
    {

        if (MN_UIManager.Instance.CurrentHealth < Mathf.Epsilon)
        {
            StateChange(STATE.PLAYER_DEAD);

        }
        if (MN_UIManager.Instance.Boss_CurrentHealth < Mathf.Epsilon)
        {
            StateChange(STATE.PLAYER_DEAD);

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
                    GameObject allclose = GameObject.Find("AllCLose");
                    allclose.SetActive(false);

                    GameOverUI.SetActive(true);

                }
                break;
            case STATE.BOSS_DEAD:
                {
                    GameObject allclose = GameObject.Find("AllCLose");
                    allclose.SetActive(false);

                    GameWin.SetActive(true);
                

                }
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.GAMESTART:
                break;
            case STATE.PLAY:
                break;
            case STATE.PLAYER_DEAD:
                break;
            case STATE.BOSS_DEAD:
                break;
        }
    }


    public void OnClick_RestartButton()
    {
        SceneManager.LoadScene("BossRoom");
    }
    public void OnClick_HomeButton()
    {
        SceneManager.LoadScene("MN_StartMenu");
    }
    
}
