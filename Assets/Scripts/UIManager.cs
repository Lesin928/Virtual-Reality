using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text resultText; // 결과 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameStartUI; // 게임 시작시 비활성화할 UI 
    public GameObject gameOverUI; // 게임 오버시 활성화할 UI 
    public GameObject gameClearUI; // 게임 클리어시 활성화할 UI 
    public GameObject EnemySpawner;      

    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo) {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore, int level) {
        scoreText.text = "Score : " + newScore;
        resultText.text = "Score : " + newScore + "\nlevel : " + LevelCheck(level);
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count, int level) {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count+ "\nlevel : " + LevelCheck(level);
    }

    // 게임 스타트 UI 비활성화
    public void SetActiveGameStartUI(bool active)
    {
        gameStartUI.SetActive(active);
    }
    public bool GetActiveGameStartUI()
    {
        return gameStartUI.activeSelf;
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {

        gameOverUI.SetActive(active);
    }

    // 게임 클리어 UI 활성화
    public void SetActiveGameClearUI(bool active)
    {

        gameClearUI.SetActive(active);
    }

    // 게임 시작
    public void EasyStart()
    {
        GameManager.instance.gameLevel = 0;
        GameStart();
    }
    public void NomalStart()
    {
        GameManager.instance.gameLevel = 1;
        GameStart();
    }
    public void HardStart()
    {
        GameManager.instance.gameLevel = 2;
        GameStart();
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        SetActiveGameStartUI(false);
        TimerManager.instance.SetActiveTimerUI(true);
        GameManager.instance.NewGame(); 
        GameManager.instance.AddScore(0);
        EnemySpawner.SetActive(true);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public string LevelCheck(int level)
    {
        switch (level)
        {
            case (0):
                return "easy";
            case (1):
                return "Nomal";
            case (2):
                return "Hard";
            default:
                return "easy";                
        }
    }
}