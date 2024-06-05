using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRay : MonoBehaviour
{
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

    public void LookMouseCursor()
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
