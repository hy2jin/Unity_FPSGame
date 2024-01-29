using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    
    public float moveSpeed = 7f;    // 이동 속도
    CharacterController cc;         // 캐릭터 컨트롤러 컴포넌트 변수
    float gravity = -20f;           // 중력 변수
    float yVelocity = 0;            // 수직 속력 변수
    public float jumpPower = 7f;    // 점프력 변수
    public bool isJumping = false;  // 점프 상태 변수
    public int hp = 100;            // 플레이어 체력 변수
    int maxHp;                      // 최대 체력
    public Slider hpSlider;         // 체력 슬라이더 변수

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        // 카메라를 기준으로 방향 변환
        dir = Camera.main.transform.TransformDirection(dir);

        // 이동
        //transform.position += dir * moveSpeed * Time.deltaTime;

        // 바닥에 착지했다면
        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0;
        }

        // 스페이스바 버튼을 누르면 점프(수직속도 중력 적용 전에 해줘야 함! 점프하고 중력 적용 되어야 하니깐!!)
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 수직 속도에 중력 값 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 이동
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 현재 체력을 슬라이더의 value에 반영
        hpSlider.value = (float)hp / (float)maxHp;
    }

    // 플레이어 피격 함수
    public void DamagedAction(int damage)
    {
        // 적의 공격력만큼 플레이어 체력을 감소
        hp -= damage;
        // 체력이 음수일 때 0으로 초기화
        if (hp < 0)
        {
            hp = 0;
        }
        Debug.Log("hp: " + hp);
    }
}
