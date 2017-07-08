using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Класс отвечает за отображение очков на экране и контролю по их набору
public class ScoreController : MonoBehaviour {

    public static ScoreController Instance
    {
        get
        {
            return instance;
        }
    }
    private static ScoreController instance = null;

    public Text score;
    public Text highScore;
    public static int points = 0;

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

    void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
        ResetScore();
	}

    public static void SetScore()
    {
        points++;
        Instance.score.text = "Score: " + points.ToString();
    }

    public static void ResetScore()
    {
        points = 0;
        Instance.score.text = "Score: " + points.ToString();
        Instance.highScore.text = PlayerPrefs.GetInt("High").ToString() + " :High Score";
    }

    public static bool IsHighScore()
    {
        if (points > PlayerPrefs.GetInt("High"))
        {
            PlayerPrefs.SetInt("High", points);
            return true;
        }
        else
            return false;
    }

    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param)
    {
        switch (Event_type)
        {
            case EVENT_TYPE.STARTING_READY:
                ResetScore();
                break;
        }
    }
}
