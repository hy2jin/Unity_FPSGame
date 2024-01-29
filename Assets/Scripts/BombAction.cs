using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;       // ���� ����Ʈ ����

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
        Collider[] cols = Physics.OverlapSphere(transform.position, 5, 1 << 8);
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<EnemyFSM>().HitEnemy(30);
        }


        // ȿ�� ������ ����
        GameObject eff = Instantiate(bombEffect);
        // ȿ�� ��ġ�� ��ź ��ġ�� �̵�
        eff.transform.position = transform.position;

        // ��ź(�ڱ� �ڽ�) ����
        Destroy(gameObject);
    }
}
