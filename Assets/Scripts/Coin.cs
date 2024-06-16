using UnityEngine;

// 동전 아이템의 기능을 구현한 스크립트
public class Coin : MonoBehaviour, IItem {
    public int score = 200; // 아이템으로 인해 증가할 점수
    public void Use(GameObject target) {
        // 게임 매니저로 접근해 점수 추가
        GameManager.instance.AddScore(score);
        Debug.Log("Coin 획득");
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}