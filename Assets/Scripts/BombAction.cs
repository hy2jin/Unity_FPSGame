using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �浹���� ��
    private void OnCollisionEnter(Collision collision)
    {
        // ��ź(�ڱ� �ڽ�) ����
        Destroy(gameObject);
    }
}
