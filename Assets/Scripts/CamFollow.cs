using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // CamPosition의 트랜스폼 컴포넌트
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // 카메라 위치를 타겟 위치에 일치
        transform.position = target.position;

    }
}
