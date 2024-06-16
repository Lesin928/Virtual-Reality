using UnityEngine;

// 총알 아이템의 기능을 구현한 스크립트
public class AmmoPack : MonoBehaviour, IItem {
    private int ammo = 60; // 아이템으로 충전할 총알 수

    public void Use(GameObject target) {
        // 전달 받은 게임 오브젝트로부터 PlayerShooter 컴포넌트를 가져오기 시도
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        // PlayerShooter 컴포넌트가 있으며, 총 오브젝트가 존재하면
        if (playerShooter != null && playerShooter.gun != null) {
            playerShooter.gun.ammoRemain += ammo; // 총의 남은 탄환 수를 ammo 만큼 추가
        }
        Debug.Log("Ammo 획득");
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}