using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    
    public float moveSpeed = 7f;    // �̵� �ӵ�
    CharacterController cc;         // ĳ���� ��Ʈ�ѷ� ������Ʈ ����
    float gravity = -20f;           // �߷� ����
    float yVelocity = 0;            // ���� �ӷ� ����
    public float jumpPower = 7f;    // ������ ����
    public bool isJumping = false;  // ���� ���� ����
    public int hp = 100;            // �÷��̾� ü�� ����
    int maxHp;                      // �ִ� ü��
    public Slider hpSlider;         // ü�� �����̴� ����

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        // Ű���� �Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        // ī�޶� �������� ���� ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        // �̵�
        //transform.position += dir * moveSpeed * Time.deltaTime;

        // �ٴڿ� �����ߴٸ�
        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0;
        }

        // �����̽��� ��ư�� ������ ����(�����ӵ� �߷� ���� ���� ����� ��! �����ϰ� �߷� ���� �Ǿ�� �ϴϱ�!!)
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        // ���� �ӵ��� �߷� �� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // �̵�
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // ���� ü���� �����̴��� value�� �ݿ�
        hpSlider.value = (float)hp / (float)maxHp;
    }

    // �÷��̾� �ǰ� �Լ�
    public void DamagedAction(int damage)
    {
        // ���� ���ݷ¸�ŭ �÷��̾� ü���� ����
        hp -= damage;
        // ü���� ������ �� 0���� �ʱ�ȭ
        if (hp < 0)
        {
            hp = 0;
        }
        Debug.Log("hp: " + hp);
    }
}
