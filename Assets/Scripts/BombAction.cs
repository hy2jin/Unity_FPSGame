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

    // 충돌했을 때
    private void OnCollisionEnter(Collision collision)
    {
        // 폭탄(자기 자신) 제거
        Destroy(gameObject);
    }
}
