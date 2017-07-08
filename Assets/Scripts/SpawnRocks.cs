using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocks : MonoBehaviour {

    public GameObject prefab;   
    public static Queue<GameObject> pullObjects;

    // Use this for initialization
    void Start () {  

        // Создаем пул объектов
        pullObjects = new Queue<GameObject>();        
        for (int i = 0; i < 5; i++)
        {
            GameObject rock = Instantiate(prefab);
            rock.GetComponent<RockObjects>().SetStartPosition(transform.position);
            ChangeChildPosition(rock);            
            pullObjects.Enqueue(rock);
        }
        
	}

    // Новое положение для дочерних объектов для разнообразия происходящего в игре
    public static void ChangeChildPosition(GameObject pref)
    {
        
        Transform rockDown = pref.transform.FindChild("RockDown");
        Transform rockUp = pref.transform.FindChild("RockUp");
        Transform star = pref.transform.FindChild("Star");
        
        rockDown.transform.localPosition = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(-5.5f, -3.5f), 0);
        rockUp.transform.localPosition = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(3.5f, 5.5f), 0);
        star.transform.localPosition = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
        
    }

    
    

    
}
