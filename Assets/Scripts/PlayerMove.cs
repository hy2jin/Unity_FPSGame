using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public float moveSpeed = 7f;    // 이동 속도
    CharacterController cc;         // 캐릭터 컨트롤러 컴포넌트 변수
    float gravity = -20f;           // 중력 변수
    float yVelocity = 0;            // 수직 속력 변수
    public float jumpPower = 7f;    // 점프력 변수
    public bool isJumping = false;  // 점프 상태 변수

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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
    }
}
