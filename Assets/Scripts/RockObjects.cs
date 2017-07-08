using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObjects : MonoBehaviour {

    public bool inPull = true;
    [SerializeField]
    private Vector2 velocityDirection = new Vector2(-4, 0);

    private Vector3 pullPosition;
    private Transform childStar;
    private Rigidbody2D rockRigidbody2d;

    
    
    
	// Use this for initialization
	void Start () {

        EventManager.Instance.AddListener(EVENT_TYPE.STARTING_READY, OnEvent);
        rockRigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        childStar = gameObject.transform.FindChild("Star");
        
    }

    // Возвращение в пул после выхода за экран
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "End")
        {
            ReturnToPull();
        }
    }

   
    public void SetStartPosition(Vector3 сoordinates)
    {
        pullPosition = сoordinates;
        transform.position = pullPosition;
    }

    // Перемещение объекта от старовой точки 
    public void Move(Vector3 startMovePosition)
    {
        if (inPull)
        {
            inPull = false;
            transform.position = startMovePosition;
            rockRigidbody2d.velocity = velocityDirection;
        }
    }

    // Делаем объект неподвижным и возвращаем в позицию пула
    public void Stop()
    {
        rockRigidbody2d.velocity = Vector2.zero;
        transform.position = pullPosition;
    }

    // возвращаем объект в пул 
    public void ReturnToPull()
    {

        if (!inPull)
        {
            inPull = true;
            Stop();                      

            if(!childStar.gameObject.activeSelf)
                childStar.gameObject.SetActive(true);

            SpawnRocks.ChangeChildPosition(gameObject);
            SpawnRocks.pullObjects.Enqueue(gameObject);
        }        

    }

    public void OnEvent(EVENT_TYPE Event_type, Component Sender, object Param)
    {
        switch (Event_type)
        {
            case EVENT_TYPE.STARTING_READY:
                ReturnToPull();
                break;
        }
    }


}
