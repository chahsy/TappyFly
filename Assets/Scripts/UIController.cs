using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Отображение UI элементов в зависимости от статуса игры 

// TODO: Доработать, сделать более надежным
public class UIController : MonoBehaviour {

    public GameObject startPanel;
    public GameObject gameOverImage;
    public GameObject highScoreImage;
    public GameObject menuButton;
    public GameObject menuWindow;
    public GameObject gameButton;
    public GameObject retryButton;
    public GameObject resumeButton;

    public static UIController Instance
    {
        get
        {
            return instance;
        }
    }

    private static UIController instance = null;


    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.GAMEOVER, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.PAUSED, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.READY, OnEvent);
    }



    // Состояние меня в зависимости от текущего статуса игры
    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param = null)
    {
        switch (Event_type)
        {
            case EVENT_TYPE.GAME:
                menuButton.SetActive(true);
                menuWindow.SetActive(false);
                break;
            case EVENT_TYPE.STARTING_READY:
                menuWindow.SetActive(false);
                startPanel.SetActive(true);
                menuButton.SetActive(true);
                break;
            case EVENT_TYPE.PAUSED:
                menuWindow.SetActive(true);
                if (menuWindow.activeSelf)
                {
                    if (gameButton.activeSelf)
                        gameButton.SetActive(false);
                    if (retryButton.activeSelf)
                        retryButton.SetActive(false);
                    resumeButton.SetActive(true);
                }
                break;
            case EVENT_TYPE.GAMEOVER:
                StartCoroutine(ActiveUIGameOver());                
                break;

        }
    }

    private IEnumerator ActiveUIGameOver()
    {
        gameOverImage.SetActive(true);

        if(ScoreController.IsHighScore())
        {
            highScoreImage.SetActive(true);            
        }

        yield return new WaitForSecondsRealtime(1.5f);

        gameOverImage.SetActive(false);
        highScoreImage.SetActive(false);

        menuButton.SetActive(false);
        menuWindow.SetActive(true);
        if (menuWindow.activeSelf)
        {
            if (gameButton.activeSelf)
                gameButton.SetActive(false);
            if (resumeButton.activeSelf)
                resumeButton.SetActive(false);
            retryButton.SetActive(true);
        }
    }
}
