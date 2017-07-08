using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRocksMove : MonoBehaviour
{
        
    public float startTime = 2.0f;
    public float repeatTime = 1.0f;

    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.GAMEOVER, OnEvent);
    }
    
    // Запуск бесконечного движение игровых объектов пока игра находится в статусе Game 
    public void StartGame()
    {        
        InvokeRepeating("StartRock", startTime, repeatTime);

    }
	void StartRock () {
        if (SpawnRocks.pullObjects != null)
        {
            GameObject g = SpawnRocks.pullObjects.Dequeue();
            g.GetComponent<RockObjects>().Move(transform.position);
        }
	}

    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param = null)
    {
        switch (Event_type)
        {
            case EVENT_TYPE.GAME:
                StartGame();
                break;
            case EVENT_TYPE.GAMEOVER:
                CancelInvoke();
                break;
        }
    }
}
