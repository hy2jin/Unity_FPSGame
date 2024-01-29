using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    // 게임 상태 상수
    public enum GameState
    {
        Ready,
        Go,
        GameOVer
    }
    public GameState gState;
    // UI 오브젝트 변수
    public GameObject gameLabel;
    // 게임 상태 텍스트
    Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 상태를 준비로 설정
        gState = GameState.Ready;
        gameText = gameLabel.GetComponent<Text>();
        gameText.text = "Ready";
        StartCoroutine(ReadyToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReadyToStart()
    {
        // 2초 대기
        yield return new WaitForSeconds(2f);
        // Go
        gameText.text = "Go!";
        // 대기
        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);
        // 상태 변경
        gState = GameState.Go;
    }
}
