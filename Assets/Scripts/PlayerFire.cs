using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePositon;          // 발사 위치
    public GameObject bombFactory;          // 폭탄 오브젝트
    public float throwPower = 15f;          // 투척 파워
    public GameObject bulletEffect;         // 총알 이펙트
    ParticleSystem ps;                      // 파티클 시스템

    // Start is called before the first frame update
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 오른쪽 버튼 입력
        if (Input.GetMouseButtonDown(1))
        {
            // 폭탄 생성
            GameObject bomb = Instantiate(bombFactory);
            // 폭탄의 위치를 발사 위치로 이동
            bomb.transform.position = firePositon.transform.position;
            // 폭탄의 리지드바디 컴포넌트를 가져옴
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            // 카메라의 정면으로 폭탄에 힘을 가함
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        // 마우스 왼쪽 버튼 입력
        if (Input.GetMouseButtonDown(0))
        {
            // 레이 생성 후 위치와 방향 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 레이가 부딪힌 대상 저장
            RaycastHit hitInfo = new RaycastHit();
            // 레이를 발사한 후, 부딪힌 물체가 있으면
            if (Physics.Raycast(ray, out hitInfo))
            {
                bulletEffect.transform.position = hitInfo.point;
                ps.Play();
            }
        }
    }
}
