using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePositon;          // �߻� ��ġ
    public GameObject bombFactory;          // ��ź ������Ʈ
    public float throwPower = 15f;          // ��ô �Ŀ�

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
