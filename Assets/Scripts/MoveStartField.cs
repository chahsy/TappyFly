using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Управление старовой платформой
public class MoveStartField : MonoBehaviour {

    [SerializeField]
    private int stopTime = 8;

    private Rigidbody2D rigid;
    private Vector3 startPosition;
    

    void Start()    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
    }

    public void Move()
    {
        rigid.velocity = new Vector2(-4, 0);
        StartCoroutine(AutoStop());
    }

    private IEnumerator AutoStop()
    {
        yield return new WaitForSeconds(stopTime);
        rigid.velocity = Vector2.zero;
    }
    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param = null)
    {
        switch (Event_type)
        {
            case EVENT_TYPE.STARTING_READY:
                transform.position = startPosition;
                break;
        }
    }





}
