using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState                      // �� ���� ���
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState m_State;                 // �� ���� ����
    public float findDistance = 8f;     // �÷��̾� �߰� ����
    Transform player;                   // �÷��̾� Ʈ������ ������Ʈ
    public float attackDistance = 2f;   // ���� ����
    public float moveSpeed = 3;         // �� �̵� �ӵ�
    CharacterController cc;             // ĳ���� ��Ʈ�ѷ� ������Ʈ
    float currentTime = 0;              // ���� �ð�
    float attackDelay = 2f;             // ���� ������ �ð�
    public int attackPower = 15;        // �� ���ݷ�
    Vector3 originPos;                  // �ʱ� ��ġ ����
    public float moveDistance = 20f;    // �̵� ���� ����
    public int hp = 30;                 // �� ü��
    int maxHp;                          // �ִ� ü��
    public Slider hpSlider;             // ü�� �����̴� ����


    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� Ʈ������ ������Ʈ �Ҵ�
        player = GameObject.Find("Player").transform;
        // ������ �� ���¸� ���� ����
        m_State = EnemyState.Idle;
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �Ҵ�
        cc = GetComponent<CharacterController>();
        // �� �ʱ� ��ġ ����
        originPos = transform.position;
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
        }

        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        // ���� �÷��̾�� ���� �Ÿ��� �߰� ���� �̳���� Move ���·� ��ȯ
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("���� ��ȯ: Idle -> Move");
        }
    }

    void Move()
    {
        // ���� ���� ��ġ�� �ʱ� ��ġ �Ÿ��� �̵� ���� �������� ũ�ٸ� ����
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("���� ��ȯ: Move -> Return");
        }
        // ���� �÷��̾�� ���� �Ÿ��� ���� �������� ũ�ٸ� �÷��̾ ���� �̵�
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // �̵� ���� = ������ - �����
            Vector3 dir = (player.position - transform.position).normalized;
            // �̵�
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
       else
       {
            m_State = EnemyState.Attack;
            Debug.Log("���� ��ȯ: Move -> Attack");
            // ���� �ð��� ���� ������ �ð���ŭ �̸� ����(���ڸ��� �����ض�)
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // ���� �÷��̾�� ���� �Ÿ��� ���� ���� �̳���� ����
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ���� �ð����� �÷��̾� ����
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // �÷��̾��� PlayerMove ��ũ��Ʈ�� DamageAction�� ȣ��
                player.GetComponent<PlayerMove>().DamagedAction(attackPower);

                Debug.Log("����");
                currentTime = 0;
            }
        }
        // �׷��� �ʴٸ�, �̵�(Move)
        else
        {
            m_State = EnemyState.Move;
            Debug.Log("���� ��ȯ: Attack -> Move");
            currentTime = 0;
        }
    }

    void Return()
    {
        // ���� �ʱ� ��ġ������ �Ÿ��� 0.1���� ũ�ٸ� �ʱ� ��ġ ������ �̵�
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            // �̵� ���� = ������ - �����
            Vector3 dir = (originPos - transform.position).normalized;
            // �̵�
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            m_State = EnemyState.Idle;
            Debug.Log("���� ��ȯ: Return -> Idle");
        }
    }

    // ������ ó�� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // �ǰ� ��Ǹ�ŭ ��ٸ�
        yield return new WaitForSeconds(0.5f);
        // �̵� ���� ��ȯ
        m_State = EnemyState.Move;
    }

    void Damaged()
    {
        // �ǰ� �ڷ�ƾ ����
        StartCoroutine(DamageProcess());
    }

    // ������ ���� �Լ�
    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die)
        {
            return;
        }

        hp -= hitPower;
        // �� ü���� 0���� ũ�� �ǰ�
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            Damaged();
            Debug.Log("�� ü��: " + hp);
        }
        // 0���� ������ ����
        else
        {
            m_State = EnemyState.Die;
            Die();
            Debug.Log("����..");
        }
    }

    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ ��Ȱ��ȭ
        cc.enabled = false;
        // 2�� ���
        yield return new WaitForSeconds(2f);
        // �� ����
        Destroy(gameObject);
    }

    void Die()
    {
        // ���� ���� �ڷ�ƾ ����
        StopAllCoroutines();
        // ���� �ڷ�ƾ ����
        StartCoroutine(DieProcess());
    }
}
