using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {



    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    public static GameStatus Status {
        get
        {
            return status;
        }
        set
        {
            status = value;
            Instance.InvokeGameEvent(status);           
        }
    }
   
    
    public Player player;
    public StartRocksMove rocks;
   

    private static GameController instance = null;
    private static AudioSource soundSource;
    private static GameStatus status;

      


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
	void Start () {
        soundSource = gameObject.GetComponent<AudioSource>();        
        Status = GameStatus.Ready;  
    }	

	public void StartGame()
    {       
        Status = GameStatus.Starting_Ready;
        Status = GameStatus.Game;
    }

    public void PauseGame()
    {
        Status = GameStatus.Paused;      
    }

    public void ResumeGame()
    {
        Status = GameStatus.Game;      
    }

    public void GameOver()
    {
        Status = GameStatus.GameOver;   
    }

    public void Retry()
    {
        Status = GameStatus.Starting_Ready;        
        Status = GameStatus.Game;
    }


    public void Exit()
    {
        Application.Quit();
    }

    public static void TakeStar()
    {
        soundSource.Play();
        ScoreController.SetScore();        
    }

    private void InvokeGameEvent(GameStatus status)
    {
        switch (status)
        {
            case GameStatus.Game:
                EventManager.Instance.PostNotification(EVENT_TYPE.GAME);
                break;
            case GameStatus.GameOver:
                EventManager.Instance.PostNotification(EVENT_TYPE.GAMEOVER);
                break;
            case GameStatus.Paused:
                EventManager.Instance.PostNotification(EVENT_TYPE.PAUSED);
                break;
            case GameStatus.Starting_Ready:
                EventManager.Instance.PostNotification(EVENT_TYPE.STARTING_READY);
                break;
            case GameStatus.Ready:
                EventManager.Instance.PostNotification(EVENT_TYPE.READY);
                break;

        }
    }
    
}

public enum GameStatus
{
    Starting_Ready,
    Game,
    Paused,
    GameOver,
    Ready
}



