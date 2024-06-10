using System.Collections.Generic;
using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : LivingEntity {

    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    public Camera characterCmera; // 레이를 위한 카메라   
    public LayerMask LayerMask; // 레이가 오브젝트에 부딪히지 않도록 마스크

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();        
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 회전 실행
        //Rotate(); 마우스 방향으로 회전되어 이 코드는 사용되지 않음
        LookMouseCursor();
        // 움직임 실행
        Move();
        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        playerAnimator.SetFloat("Move", playerInput.move);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        // 상대적으로 이동할 거리 계산
        Vector3 moveDistance = playerInput.move * transform.forward * Time.deltaTime * speed;
        // 리지드바디를 통해 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    // 마우스의 위치에 따라 캐릭터가 바라보는 방향을 번경하는 코드
    public void LookMouseCursor()
    {
        Ray ray = characterCmera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitResult;
        if (Physics.Raycast(ray, out hitResult, Mathf.Infinity, LayerMask))
        {
            Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
            playerAnimator.transform.forward = mouseDir;
        }
    }

    // 이동속도 증가
    public override void RestoreSpeed(float newSpeed)
    {
        // LivingEntity의 RestoreSpeed() 실행 (이동속도 증가)
        base.RestoreSpeed(newSpeed);
        Debug.Log("속도 증가 :" + speed);
    }
    public override void ResetSpeed()
    {
        // LivingEntity의 RestoreSpeed() 실행 (이동속도 증가)
        base.ResetSpeed();
        Debug.Log("속도 리셋 :" + speed);
    }
}