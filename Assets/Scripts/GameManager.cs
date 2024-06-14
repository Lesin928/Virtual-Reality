using UnityEngine;

// 점수와 게임 오버 여부, 게임 UI를 관리하는 게임 매니저
public class GameManager : MonoBehaviour {
    // 외부에서 싱글톤 오브젝트를 가져올때 사용할 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태

    public int gameLevel = 0; //현재 게임 난이도

    private void Awake() {

        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }

        //게임 시작
        if (UIManager.instance.GetActiveGameStartUI())
        {
            isGameover = true;
            Time.timeScale = 0;
        }

    }

    private void Start() {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;

    }


    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore) {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score, gameLevel);
        }
    }

    // 게임 오버 처리
    public void EndGame() {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);
        Time.timeScale = 0;
    }

    // 게임 클리어 처리
    public void ClearGame()
    {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 클리어 UI를 활성화
        UIManager.instance.SetActiveGameClearUI(true);
        Time.timeScale = 0;
    }

    //게임 시작
    public void NewGame()
    {
        // 게임 오버 상태를 거짓으로 변경
        isGameover = false;
        // 게임 오버 UI를 비활성화
        UIManager.instance.SetActiveGameoverUI(false);
        UIManager.instance.SetActiveGameClearUI(false);
    }


}