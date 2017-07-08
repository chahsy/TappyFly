using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public GameObject panel;
    public MoveStartField startField;
    public Text text;
    public Vector2 upForce = new Vector2(0, 210);

    private Rigidbody2D rig;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioSource aircraftAudio;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        aircraftAudio = GetComponent<AudioSource>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
    }
    
    //Стартовая позиция и свойства в начале игре и при старте
    public void StartGame()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rig.angularVelocity = 0.0f;
        StartCoroutine(LateStart());
    }

    //Прыжок персонажа
    public void JumpPlayer()
    {
        rig.velocity = Vector2.zero;
        rig.AddForce(upForce);
    }

    // Запускаем движение после стартого отчета 
    // TODO: сделать запуск более надежным
    private IEnumerator LateStart()
    {
        if (!panel.activeSelf)
            panel.SetActive(true);
        yield return new WaitForSeconds(3.1f);
        aircraftAudio.Play();
        if(!rig.simulated)
            rig.simulated = true;
        if(panel.activeSelf)
            panel.SetActive(false);
        startField.Move();
    }

    // Удар о скалу -> проигрыш
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Rock")
        {
            if(rig.simulated)
                rig.simulated = false;
            GameController.Instance.GameOver();    
        }
        
    }

    // Лут звезд для очков
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Star")
        {
            GameController.TakeStar();           
            other.gameObject.SetActive(false);

        }
    }

    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param = null)
    {
        switch (Event_type)
        {
            case EVENT_TYPE.STARTING_READY:
                StartGame();
                break;
        }
    }

}
