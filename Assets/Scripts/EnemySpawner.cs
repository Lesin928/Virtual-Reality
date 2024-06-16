using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적 게임 오브젝트를 주기적으로 생성하는 스크립트
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI

    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    public float damageMax = 30f; // 최대 공격력
    public float damageMin = 10f; // 최소 공격력

    public float healthMax = 100f; // 최대 체력
    public float healthMin = 25f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public Color strongEnemyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    private int wave; // 현재 웨이브
    private int level; // 현재 난이도

    private void Start() {

        level = GameManager.instance.gameLevel;
        //난이도에 맞게 변수 값 조절
        damageMin += 5.0f * level;
        damageMax += 5.0f * level;
        healthMax += 20.0f * level;
        healthMin += 5.0f * level;
        speedMax += 1.0f * level;
        speedMin += 0.3f * level;
}

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.IsGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 스폰 실행
        if (enemies.Count <= 0)
        {
            SpawnWave();
        }

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() {
        // 현재 웨이브와 남은 적의 수 표시
        UIManager.instance.UpdateWaveText(wave, enemies.Count, level);
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave() {
        // 웨이브 1 증가
        wave++;

        // 현재 웨이브 * 1.2에 반올림 한 개수 만큼 적을 생성
        int spawnCount = Mathf.RoundToInt(wave * 1.2f + 3);
        
        // spawnCount 만큼 적을 생성
        for (int i = 0; i < spawnCount; i++)
        {
            // 적의 세기를 0%에서 100% 사이에서 랜덤 결정
            float enemyIntensity = Random.Range(0f, 1f);
            // 적 생성 처리 실행
            CreateEnemy(enemyIntensity);
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity) {
        // intensity를 기반으로 적의 능력치 결정
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed = Mathf.Lerp(speedMin, speedMax, intensity);

        // intensity를 기반으로 하얀색과 enemyStrength 사이에서 적의 피부색 결정
        Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);

        // 생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 적 프리팹으로부터 적 생성
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // 생성한 적의 능력치와 추적 대상 설정
        enemy.Setup(health, damage, speed, skinColor);

        // 생성된 적을 리스트에 추가
        enemies.Add(enemy);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        enemy.onDeath += () => enemies.Remove(enemy);
        // 사망한 적을 10 초 뒤에 파괴
        enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        // 적 사망시 점수 상승
        enemy.onDeath += () => GameManager.instance.AddScore(100);
    }
}