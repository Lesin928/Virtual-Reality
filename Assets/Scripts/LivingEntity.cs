﻿using System;
using UnityEngine;

// 생명체로서 동작할 게임 오브젝트들을 위한 뼈대를 제공
// 체력, 데미지 받아들이기, 사망 기능, 사망 이벤트를 제공
public class LivingEntity : MonoBehaviour, IDamageable {
    public float startingHealth =  100f; // 시작 체력
    public float startingSpeed = 5f;//시작 속도
    public float health { get; protected set; } // 현재 체력
    public float speed { get; protected set; }// 현재 속도
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

    // 생명체가 활성화될때 상태를 리셋
    protected virtual void OnEnable() {
        // 사망하지 않은 상태로 시작
        dead = false;
        // 속도를 시작 속도로 초기화
        speed = startingSpeed;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
    }

    // 데미지를 입는 기능
    public virtual void OnDamage(float damage, Vector3 hitPoint,
        Vector3 hitNormal) {
        // 데미지만큼 체력 감소
        health -= damage;

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 체력을 회복하는 기능
    public virtual void RestoreHealth(float newHealth) { 
        if (dead)
        {
            // 이미 사망한 경우 체력을 회복할 수 없음
            return;
        }
        // 체력 추가
        if (health <= 50f)
        {
            health += newHealth;
        }
        else if (health > 50f)
        {
            health = 100f;
        }
    }

    // 이동속도가 증가하는 기능
    public virtual void RestoreSpeed(float newSpeed)
    {
        if (dead)
        {
            // 이미 사망한 경우 이동속도를 증가시킬 수 없음
            return;
        }
        // 이동속도 증가
        if (speed <= 8f)
        {
            speed += newSpeed;
        }       
        
    }
    // 이동속도가 감소하는 기능
    public virtual void ResetSpeed()
    {
        if (dead)
        {
            // 이미 사망한 경우 이동속도를 증가시킬 수 없음
            return;
        }

        if (speed != startingSpeed)
        { 
            //이동속도가 증가한 경우 속도 리셋
            speed = startingSpeed;
        }
    }


    // 사망 처리
    public virtual void Die() {
        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        if (onDeath != null)
        {
            onDeath();
        }
        // 사망 상태를 참으로 변경
        dead = true;
    }
}