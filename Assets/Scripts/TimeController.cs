using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Time.Scale в зависимости от статуса игры
public class TimeController : MonoBehaviour {

    public static TimeController Instance
    {
        get
        {
            return instance;
        }
    }

    private static TimeController instance = null;

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

    // Use this for initialization
    void Start ()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.GAMEOVER, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.PAUSED, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.READY, OnEvent);
    }

    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param = null)
    {              
        switch (Event_type)
        {
            case EVENT_TYPE.GAMEOVER:
                Time.timeScale = 0.0f;
                break;
            case EVENT_TYPE.GAME:
                Time.timeScale = 1.0f;
                break;
            case EVENT_TYPE.PAUSED:
                Time.timeScale = 0.0f;
                break;
            case EVENT_TYPE.STARTING_READY:
                Time.timeScale = 1.0f;
                break;
            case EVENT_TYPE.READY:
                Time.timeScale = 0.0f;
                break;


        }
    }
}
