using UnityEngine; 
// 아드레날린
public class AdPack : MonoBehaviour, IItem
{
    public float adSpeed = 5f; // 이동속도 상승 

    public void Use(GameObject target)
    {
        // 전달받은 게임 오브젝트로부터 LivingEntity 컴포넌트 가져오기 시도
        LivingEntity life = target.GetComponent<LivingEntity>();

        // LivingEntity컴포넌트가 있다면
        if (life != null)
        {
            life.RestoreSpeed(adSpeed); 
        }

        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}
