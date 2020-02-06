﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }

            return _instance;

        }
    }

    public Text gameOverText;

    public GameObject startUI;

    public GameObject readyUI;
    public Text getReadyText;
    public Text countDownText;

    public Text scoreText;
    public GameObject replayUI;

    public Animator animator;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        //startButton.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //getReadyText.gameObject.SetActive(false);
        //replayUI.SetActive(false);
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void SetActiveScoreText(bool active)
    {
        scoreText.gameObject.SetActive(active);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetActiveStartUI(bool active)
    {
        startUI.SetActive(active);
    }

    public void SetActiveReplayUI(bool active)
    {
        replayUI.SetActive(active);
    }

    public void ShowGetReadyText()
    {
        readyUI.SetActive(true);
        StartCoroutine(Countdown());    // Get Ready 재생 후 3, 2, 1 카운트 다운

        //getReadyText.gameObject.SetActive(true);
        //OnShowGetReadyText();
    }

    public void SetActiveGameOverText(bool active)
    {
        gameOverText.gameObject.SetActive(active);
    }

    IEnumerator Countdown()
    {
        getReadyText.gameObject.SetActive(true);

        // Get Ready 텍스트 애니메이션 재생
        animator.SetBool("GetReady", true);
        yield return new WaitForSeconds(2.1f);

        animator.SetBool("GetReady", false);
        getReadyText.gameObject.SetActive(false);


        // 카운트 다운 텍스트
        countDownText.gameObject.SetActive(true);
        for (int count = 3; count > 0; count--)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(0.7f);
        }

        countDownText.gameObject.SetActive(false);
        readyUI.SetActive(false);

        // 게임 시e
        GameManager.instance.StartGame();

        yield break;
    }
}
