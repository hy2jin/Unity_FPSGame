using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePositon;          // �߻� ��ġ
    public GameObject bombFactory;          // ��ź ������Ʈ
    public float throwPower = 15f;          // ��ô �Ŀ�
    public GameObject bulletEffect;         // �Ѿ� ����Ʈ
    ParticleSystem ps;                      // ��ƼŬ �ý���
    public int weaponPower = 10;          // �Ѿ� ���ݷ�

    // Start is called before the first frame update
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ ��ư �Է�
        if (Input.GetMouseButtonDown(1))
        {
            // ��ź ����
            GameObject bomb = Instantiate(bombFactory);
            // ��ź�� ��ġ�� �߻� ��ġ�� �̵�
            bomb.transform.position = firePositon.transform.position;
            // ��ź�� ������ٵ� ������Ʈ�� ������
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            // ī�޶��� �������� ��ź�� ���� ����
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        // ���콺 ���� ��ư �Է�
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ���� �� ��ġ�� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // ���̰� �ε��� ��� ����
            RaycastHit hitInfo = new RaycastHit();
            // ���̸� �߻��� ��, �ε��� ��ü�� ������
            if (Physics.Raycast(ray, out hitInfo))
            {
                // ���� �ε��� ����� ���̾ Enemy���
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // �ε��� ���(=Enemy)�� EnemyFSM�� HitEnemy�Լ� ����
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }

                bulletEffect.transform.position = hitInfo.point;
                // �Ѿ� ȿ���� ���̰� �΋J�� ������ ���� ���Ϳ� ��ġ
                bulletEffect.transform.forward = hitInfo.normal;
                ps.Play();
            }
        }
    }
}
