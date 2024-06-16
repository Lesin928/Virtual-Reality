using UnityEngine; 
// 아드레날린 아이템의 기능을 구현한 스크립트
public class AdPack : MonoBehaviour, IItem
{
    private float adSpeed = 2f; // 아이템으로 인한 이동속도 상승 수치

    public void Use(GameObject target) {
        // 전달받은 게임 오브젝트로부터 LivingEntity 컴포넌트 가져오기 시도
        PlayerMovement life = target.GetComponent<PlayerMovement>();

        if (life != null) { // 살아있으면
            life.RestoreSpeed(adSpeed);
        }
        Debug.Log("Ad 획득");
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}
