using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 1.5f;    // ���� �ð�
    float currentTime = 0;              // ��� �ð�

    // Update is called once per frame
    void Update()
    {
        // ��� �ð��� ���� �ð��� �ʰ��ϸ�
        if (currentTime > destroyTime)
        {
            // ����Ʈ ����
            Destroy(gameObject);
        }
        // ��� �ð� ����
        currentTime += Time.deltaTime;
    }
}
