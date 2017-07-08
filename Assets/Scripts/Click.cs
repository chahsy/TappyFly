using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Взаимодействие с пользователем
public class Click : MonoBehaviour {

    [SerializeField]
    private Player player;
    private static Collider2D col;
    private AudioSource clickAudio;    
    

    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.GAMEOVER, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.PAUSED, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.READY, OnEvent);
        clickAudio = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    void OnMouseDown()
    {
        clickAudio.Play();
        player.JumpPlayer();
    }

    public static void ActiveClickCollider(bool active)
    {
        Click.col.enabled = active;
    }

    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param = null)
    {
        if (Event_type == EVENT_TYPE.GAME)
        {
            ActiveClickCollider(true);
        }
        else
        {
            ActiveClickCollider(false);
        }
    }
}
