using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class MN_UIManager : MonoBehaviour
{
    // 싱글톤이 할당될 변수
    private static MN_UIManager m_instance; 

    // 싱글톤 접근용 프로퍼티
    public static MN_UIManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MN_UIManager>();
            }
            
            return m_instance;
        }
        
    }


    //public Text ammoText; // 탄약 표시용 텍스트
   // public Text scoreText; // 점수 표시용 텍스트
    //public Text waveText; // 적 웨이브 표시용 텍스트
    //public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    
    // 보스 관련
    public float Boss_CurrentHealth { get; set; }
    public float Boss_MaxHealth { get; set; }
    public bool IsHitBox { get; set; }
    public bool WinGame { get; set; }


    // 플레이어 관련
    public float CurrentHealth { get; set; }
    public bool OnDamage { get;  set; }
    public bool IsDead { get; set; }
    public bool IsHit { get; set; }
    public int ammo { get; set; }
    public int MaxAmmo { get; set;}
    public int Potions { get; set; }
    public int Granade { get; set; }

   
    //플레이어 체력 갱신


    // 좀비 킬
    public bool IsZombieKill { get; set; }

    public bool Inventory { get; set; }

    public void UpdatePlayerHealth(float Health)
    {
        if (Health < 0)
            IsHit = true;
        Debug.Log(IsHit);
        CurrentHealth += Health;
        Debug.Log("Player Health = " + CurrentHealth);
        if(CurrentHealth < Mathf.Epsilon)
        {
            IsDead = true;
        }
    }
    public void UsePotion(float heal)
    {
        Debug.Log("IsDead" + IsDead);
        if (IsDead) return;
        Debug.Log("포션 마심1");

        if (CurrentHealth >= 180)
        {
            Debug.Log("체력오버");

            CurrentHealth = 200;
        }
        else
            CurrentHealth += heal;
        Debug.Log("Player Health = " + CurrentHealth);

    }

    // 한번에 맥스와 현재 창탄 수를 가져온다
    public void UpdateAmmos(int MaxAmmo,int ammo)
    {
        this.ammo = ammo;
        this.MaxAmmo = MaxAmmo;
    }

    public void UpdateBossHealth(float damage)
    {
        if (damage > 5000f)
            IsHitBox = true;

        Debug.Log("IsHitBox : " + IsHitBox);
        this.Boss_CurrentHealth -= damage;
    }


    //public void UpdatePlayerRestoreHealth(float Health)
    //{
    //    Debug.Log("ONRestoreHealth");
    //    yPlayerHealth player = FindObjectOfType<yPlayerHealth>();

    //    player.RestoreHealth(Health);
    //}


    // 탄약 텍스트 갱신
    //public void UpdateAmmoText(int magAmmo, int remainAmmo)
    //{
    //    ammoText.text = magAmmo + "/" + remainAmmo;
    //}

    //// 점수 텍스트 갱신
    //public void UpdateScoreText(int newScore)
    //{
    //    scoreText.text = "Score : " + newScore;
    //}

    //// 적 웨이브 텍스트 갱신
    //public void UpdateWaveText(int waves, int count)
    //{
    //    waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    //}

    //// 게임 오버 UI 활성화
    //public void SetActiveGameoverUI(bool active)
    //{
    //    gameoverUI.SetActive(active);
    //}

    // 게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 숨긴 커서 다시 보이기
    public void MouseCursorActive()
    {
        
        // Mouse Lock
        Cursor.lockState = CursorLockMode.None;

        // Cursor visible
        Cursor.visible = true;
    }

    // 전체화면 혹은 게임 실행시 마우스 커서 숨기기 코드
    public void MouseCursorDeactivate()
    {
        
        // Mouse Lock
        Cursor.lockState = CursorLockMode.Locked;

        // Cursor visible
        Cursor.visible = false;
    }
}