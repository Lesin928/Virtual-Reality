using System.Collections;
using UnityEngine;

//플레이어가 불타는 기능을 구현한 스크립트
public class Fire : MonoBehaviour
{
    public GameObject objectToActivate; // 활성화할 게임 오브젝트
    public string triggeringTag = "Player"; // 트리거를 활성화하는 오브젝트의 태그
    public float deactivateDelay = 5f; // 비활성화 지연 시간 (초)
    public float damageInterval = 1f; // 데미지 간격 (초)
    public float burnDamage = 5f; // 화염의 데미지

    private Coroutine damageCoroutine;
    private Coroutine deactivateCoroutine;
    private bool isColliding = false;
    private LivingEntity currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 triggeringTag와 일치하면 objectToActivate 활성화
        if (other.CompareTag(triggeringTag))
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            if (attackTarget != null)
            {
                objectToActivate.SetActive(true);
                isColliding = true;
                currentTarget = attackTarget;

                // 데미지를 입히는 코루틴 시작
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }
                damageCoroutine = StartCoroutine(DamageOverTime(attackTarget, burnDamage));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌이 끝난 후 5초 동안 데미지를 입히는 코루틴 시작
        if (other.CompareTag(triggeringTag))
        {
            isColliding = false;

            if (deactivateCoroutine != null)
            {
                StopCoroutine(deactivateCoroutine);
            }
            deactivateCoroutine = StartCoroutine(DeactivateAfterDelay(deactivateDelay, other.GetComponent<LivingEntity>()));
        }
    }

    private IEnumerator DamageOverTime(LivingEntity target, float damageAmount)
    {
        while (isColliding)
        {
            target.OnDamage(damageAmount, transform.position, transform.forward);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay, LivingEntity target)
    {
        float remainingTime = delay;

        while (remainingTime > 0)
        {
            target.OnDamage(burnDamage, transform.position, transform.forward);
            yield return new WaitForSeconds(damageInterval);
            remainingTime -= damageInterval;
        }

        // 오브젝트 비활성화
        objectToActivate.SetActive(false);

        // 데미지를 입히는 코루틴 중지
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }
}
