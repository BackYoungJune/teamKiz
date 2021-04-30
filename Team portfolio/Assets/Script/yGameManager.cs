using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yGameManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static yGameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<yGameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static yGameManager m_instance; // 싱글톤이 할당될 static 변수
    private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //// 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        //if (instance != this)
        //{
        //    // 자신을 파괴
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        Debug.Log("yGameManager.Start()");
        FindObjectOfType<yPlayerHealth>().onDeath += EndGame;
    }

    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore)
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover)
        {
            // 점수 추가
            score += newScore;
            /* 점수 UI 텍스트 갱신 */
        }
    }

    // 게임 오버 처리
    public void EndGame()
    {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        
        /* 게임오버 UI 활성화 */
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadBossScene()
    {
        SceneManager.LoadScene("BossRoom");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        //J_DataManager.instance.LoadItemDataFromJson();
        //J_DataManager.instance.LoadPlayDataFromJson();
        Debug.Log("SceneLoaded");
    }
}
