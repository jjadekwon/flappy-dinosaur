using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Intro : 첫 시작 화면
 * Ready : 플레이 버튼을 눌렀을 때 준비 상태
 * Start : 게임 시작
 * End   : 게임 종료
 */
public enum GameState { Intro, Ready, Start, End }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public GameState gameState { get; private set; }

    private int score = 0;
    private float scoreUpdateTime = 1.5f;
    private float lastScoreUpdateTime;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        //var objs = FindObjectsOfType<GameManager>();

        //if (objs.Length > 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        gameState = GameState.Intro;

        UIManager.instance.SetActiveStartUI(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.End) return;

        // update score
        if (gameState == GameState.Start)
        {
            if (Time.time >= lastScoreUpdateTime + scoreUpdateTime)
            {
                UIManager.instance.UpdateScore(score++);
                lastScoreUpdateTime = Time.time;
            }
        }
    }

    public void EndGame()
    {
        gameState = GameState.End;

        UIManager.instance.SetActiveGameOverText(true);
        UIManager.instance.SetActiveReplayUI(true);
    }

    public void OnPlayButtonClick()
    {
        gameState = GameState.Ready;

        UIManager.instance.SetActiveScoreText(true);
        UIManager.instance.SetActiveStartUI(false);
        UIManager.instance.ShowGetReadyText();
    }

    public void OnReplayButtonClick()
    {
        //gameState = GameState.Ready;

        //UIManager.instance.SetActiveGameOverText(false);
        //UIManager.instance.SetActiveReplayUI(false);
        //UIManager.instance.ShowGetReadyText();
    }

    public void StartGame()
    {
        gameState = GameState.Start;
    }
}
