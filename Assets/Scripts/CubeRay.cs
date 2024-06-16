using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRay : MonoBehaviour
{
    // 마우스가 바라보는 방향을 체크하기 위한 테스트 전용 스크립트
    // 본 프로그램에서는 사용되지 않음
    // 사용법 : 유니티 프로젝트 prefabs에 있는  rayChackCube를 Main씬에 넣고 실행

    private Transform cubeTransform; // 큐브 위치
    public Camera characterCmera; // 레이르 위한 카메라 

    private void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        cubeTransform = GetComponent<Transform>(); 
    }

    private void FixedUpdate()
    {        
        LookMouseCursor();
    }

    public void LookMouseCursor() //마우스 위치를 향해 큐브를 실시간으로 이동
    {
        Ray ray = characterCmera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;

        if (Physics.Raycast(ray, out hitResult))
        {
            Vector3 mouseDir = new Vector3(hitResult.point.x, hitResult.point.y, hitResult.point.z);
            cubeTransform.position = mouseDir;
        }

    }
}
