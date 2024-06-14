using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerManager : MonoBehaviour
{
    public float totalTime = 11f; // 총 시간 (초 단위로 3분)
    private float currentTime; // 현재 남은 시간
    public Text GameTimeText;
    public GameObject TimeText;

    // 싱글톤 접근용 프로퍼티
    public static TimerManager instance
    {
        get
        {
            if (t_instance == null)
            {
                t_instance = FindObjectOfType<TimerManager>();
            }

            return t_instance;
        }
    }
    private static TimerManager t_instance; // 싱글톤이 할당될 변수

    private void Start()
    {
        currentTime = totalTime; // 시작할 때 총 시간을 현재 시간으로 설정
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // 경과 시간만큼 감소
            UpdateGameTimeText(); // 텍스트 업데이트

            if (currentTime <= 0)
            {
                currentTime = 0;
                GameTimeText.gameObject.SetActive(false); // 시간이 0이 되면 텍스트 비활성화
            }
        }
        else
        {
            GameManager.instance.ClearGame();
        }
    }

    public void SetActiveTimerUI(bool active)
    {
        TimeText.SetActive(active);
    }

    private void UpdateGameTimeText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60); // 분 계산
        int seconds = Mathf.FloorToInt(currentTime % 60); // 초 계산
        GameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 텍스트 업데이트
    }
}