using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 1.5f;    // 지속 시간
    float currentTime = 0;              // 경과 시간

    // Update is called once per frame
    void Update()
    {
        // 경과 시간이 지속 시간을 초과하면
        if (currentTime > destroyTime)
        {
            // 이펙트 제거
            Destroy(gameObject);
        }
        // 경과 시간 누적
        currentTime += Time.deltaTime;
    }
}
