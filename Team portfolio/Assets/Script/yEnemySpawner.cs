using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yEnemySpawner : MonoBehaviour
{
    public yEnemy[] enemyPrefab; // 생성할 적 AI들

    public Transform[] Wave1spawnPoints; // 적 AI를 소환할 위치들
    public Transform[] Wave2spawnPoints; // 적 AI를 소환할 위치들
    public Transform[] Wave3spawnPoints; // 적 AI를 소환할 위치들
    public Transform[] Wave4spawnPoints; // 적 AI를 소환할 위치들

    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float health1 = 100f; // 최소 체력

    public float speedMax = 10f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public float scoreMax = 100f; // 최대 점수
    public float scoreMin = 80f; // 최소 점수

    public List<yEnemy> enemies = new List<yEnemy>(); // 생성된 적들을 담는 리스트
    public int wave; // 현재 웨이브

    Transform spawnPoint;   // 스폰 포인트 결정
    public bool ChangedWave = true;    // 웨이브 생성 조건

    // Update is called once per frame
    void Update()
    {
        // 게임 오버 상태일때는 생성하지 않음
        if (yGameManager.instance != null && yGameManager.instance.isGameover)
        {
            return;
        }
    }
    

    public void SpawnWave()
    {
        wave++;

        // 현재 웨이브 * 1.5를 반올림한 수만큼 적 생성
        // RoundToInt는 float 값을 입력받고 입력값을 반올림한 정수를 반환한다.
        int spawnCount = Mathf.RoundToInt(wave * 4.5f + 15);

        // spawnCount만큼 적 생성
        for (int i = 0; i < spawnCount; i++)
        {
            // 적의 세기를 0%에서 100% 사이에서 랜덤 결정
            float enemyIntensity = Random.Range(0f, 1f);
            // 적 생성 처리 실행
            CreateEnemy(enemyIntensity);
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity)
    {
        // intensity를 기반으로 적의 능력치 결정
        float health = health1;
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed = Mathf.Lerp(speedMin, speedMax, intensity);
        float score = Mathf.Lerp(scoreMin, scoreMax, intensity);

        // 각 웨이브에 생성할 위치를 랜덤으로 결정
        WaveSpawn(wave);

        // 적 프리팹으로부터 적 생성
        yEnemy enemy = Instantiate(Random.Range(0, 2) == 0 ? enemyPrefab[0] : enemyPrefab[1], spawnPoint.position, spawnPoint.rotation);

        // 생성한 적의 능력치와 추적 대상 설정
        enemy.Setup(health, damage, speed);

        enemy.ChangeState(yEnemy.STATE.SEARCHING);

        // 생성한 적을 리스트에 추가
        enemies.Add(enemy);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        enemy.onDeath += () => enemies.Remove(enemy);
        // 적 사망 시 점수 상승
        enemy.onDeath += () => yGameManager.instance.AddScore((int)score);
        /* 좀비 죽었을 경우 유석 UI 추가하기*/
        //enemy.onDeath += () => 
    }

    Transform WaveSpawn(int wave)
    {
        switch(wave)
        {
            case 1:
                spawnPoint = Wave1spawnPoints[Random.Range(0, Wave1spawnPoints.Length)];
                break;
            case 2:
                spawnPoint = Wave2spawnPoints[Random.Range(0, Wave2spawnPoints.Length)];
                break;
            case 3:
                spawnPoint = Wave3spawnPoints[Random.Range(0, Wave3spawnPoints.Length)];
                break;
            case 4:
                spawnPoint = Wave4spawnPoints[Random.Range(0, Wave4spawnPoints.Length)];
                break;
        }
        return spawnPoint;
    }
}
