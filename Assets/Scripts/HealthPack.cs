﻿using UnityEngine;

// 체력회복 아이템의 기능을 구현한 스크립트
public class HealthPack : MonoBehaviour, IItem {
    public float health = 50; // 체력을 회복할 수치

    public void Use(GameObject target) {
        // 전달받은 게임 오브젝트로부터 LivingEntity 컴포넌트 가져오기 시도
        LivingEntity life = target.GetComponent<LivingEntity>();

        // LivingEntity컴포넌트가 있다면
        if (life != null)
        {
            // 체력 회복 실행
            life.RestoreHealth(health);
        }
        Debug.Log("HP PACK 획득");
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}