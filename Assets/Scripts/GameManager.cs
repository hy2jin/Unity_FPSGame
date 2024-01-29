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

    // ���� ���� ���
    public enum GameState
    {
        Ready,
        Go,
        GameOVer
    }
    public GameState gState;
    // UI ������Ʈ ����
    public GameObject gameLabel;
    // ���� ���� �ؽ�Ʈ
    Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ���¸� �غ�� ����
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
        // 2�� ���
        yield return new WaitForSeconds(2f);
        // Go
        gameText.text = "Go!";
        // ���
        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);
        // ���� ����
        gState = GameState.Go;
    }
}
