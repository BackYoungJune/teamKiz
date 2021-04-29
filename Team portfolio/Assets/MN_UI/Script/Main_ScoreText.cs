using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_ScoreText : MonoBehaviour
{
    GameObject ScoreObj;
    GameObject HeadShotObj;
    Text Score_Text;
    Image HeadShot_Image;

    enum STATE
    {
        NORMAL, UP, STOP
    }
    STATE myState = STATE.NORMAL;

    private void Awake()
    {
        //setactive를 활용하기 위해서 gameobject로 불러온다.
        ScoreObj = GameObject.Find("ScoreText");
        HeadShotObj = GameObject.Find("HeadShot_Image");

        Score_Text = ScoreObj.GetComponent<Text>();
        HeadShot_Image = HeadShotObj.GetComponent<Image>();
        Score_Text.text = "0";
        ScoreObj.SetActive(false);
        HeadShotObj.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(J_ItemManager.instance.IsHeadShotKill)
        {
            ChangeState(STATE.UP);
        }
    }
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.UP:
                ScoreObj.SetActive(true);
                HeadShotObj.SetActive(true);
                //J_ItemManager.instance.remainScore += 100;
                StartCoroutine(UPScore());
                break;
            case STATE.STOP:
                ScoreObj.SetActive(false);
                HeadShotObj.SetActive(false);
                // J_ItemManager.instance.IsHeadShotKill = false;
                break;

        }
    }    
    IEnumerator UPScore()
    {
        //점수가 올라 갈 때 헤드샷을 맞으면 한번 더 올라가야 하니까 
        J_ItemManager.instance.IsHeadShotKill = false;

        int score = J_ItemManager.instance.remainScore;
        int NextScore = score + 100;
        // float TimePassed = 0f;
        J_ItemManager.instance.remainScore = NextScore;
        Debug.Log(J_ItemManager.instance.remainScore);

        while (score < NextScore)
        {
            // 스코어가 올라가는 중에 헤드샷을 한번 더 하면 다시 코루틴 반복



            score++;
            Score_Text.text = "+ "+score.ToString();

            yield return new WaitForSeconds(0.01f);
            if (J_ItemManager.instance.IsHeadShotKill)
            {
                StopAllCoroutines();
                StartCoroutine(UPScore());
            }
        }
        if (J_ItemManager.instance.IsHeadShotKill)
            StartCoroutine(UPScore());

        yield return new WaitForSeconds(1f);

        
        ChangeState(STATE.STOP);

    }
}
