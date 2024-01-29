using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState                      // 적 상태 상수
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState m_State;                 // 적 상태 변수
    public float findDistance = 8f;     // 플레이어 발견 범위
    Transform player;                   // 플레이어 트랜스폼 컴포넌트
    public float attackDistance = 2f;   // 공격 범위
    public float moveSpeed = 3;         // 적 이동 속도
    CharacterController cc;             // 캐릭터 컨트롤러 컴포넌트
    float currentTime = 0;              // 누적 시간
    float attackDelay = 2f;             // 공격 딜레이 시간
    public int attackPower = 15;        // 적 공격력
    Vector3 originPos;                  // 초기 위치 저장
    public float moveDistance = 20f;    // 이동 가능 범위

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 트랜스폼 컴포넌트 할당
        player = GameObject.Find("Player").transform;
        // 최초의 적 상태를 대기로 설정
        m_State = EnemyState.Idle;
        // 캐릭터 컨트롤러 컴포넌트 할당
        cc = GetComponent<CharacterController>();
        // 적 초기 위치 저장
        originPos = transform.position;
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
            case EnemyState.Damaged:
                // Damaged();
                break;
            case EnemyState.Die:
                // Die();
                break;
        }
    }

    void Idle()
    {
        // 만약 플레이어와 적의 거리가 발견 범위 이내라면 Move 상태로 전환
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("상태 전환: Idle -> Move");
        }
    }

    void Move()
    {
        // 만약 현재 위치와 초기 위치 거리가 이동 가능 범위보다 크다면 복귀
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("상태 전환: Move -> Return");
        }
        // 만약 플레이어와 적의 거리가 공격 범위보다 크다면 플레이어를 향해 이동
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // 이동 방향 = 목적지 - 출발지
            Vector3 dir = (player.position - transform.position).normalized;
            // 이동
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
       else
       {
            m_State = EnemyState.Attack;
            Debug.Log("상태 전환: Move -> Attack");
            // 누적 시간을 공격 딜레이 시간만큼 미리 진행(닿자마자 공격해라)
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // 만약 플레이어와 적의 거리가 공격 범위 이내라면 공격
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // 일정 시간마다 플레이어 공격
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // 플레이어의 PlayerMove 스크립트의 DamageAction을 호출
                player.GetComponent<PlayerMove>().DamagedAction(attackPower);

                Debug.Log("공격");
                currentTime = 0;
            }
        }
        // 그렇지 않다면, 이동(Move)
        else
        {
            m_State = EnemyState.Move;
            Debug.Log("상태 전환: Attack -> Move");
            currentTime = 0;
        }
    }

    void Return()
    {
        // 만약 초기 위치에서의 거리가 0.1보다 크다면 초기 위치 쪽으로 이동
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            // 이동 방향 = 목적지 - 출발지
            Vector3 dir = (originPos - transform.position).normalized;
            // 이동
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            m_State = EnemyState.Idle;
            Debug.Log("상태 전환: Return -> Idle");
        }
    }
}
