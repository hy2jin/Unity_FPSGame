using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // CamPosition�� Ʈ������ ������Ʈ
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ��ġ�� Ÿ�� ��ġ�� ��ġ
        transform.position = target.position;

    }
}
