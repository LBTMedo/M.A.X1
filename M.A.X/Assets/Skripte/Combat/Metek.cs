﻿using UnityEngine;
using System.Collections;

public class Metek : MonoBehaviour {

    public float damage = 20;
    public float lifeTime = 4f;
    public string tag1;

    void Start()
    {
        Invoke("DestroyGO", lifeTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == tag1)
        {
            Debug.Log("Streljanje");
            coll.gameObject.SendMessage("PrejmiSkodo", damage);
            Destroy(gameObject);
        }
        else if(coll.gameObject.tag == "Melee" || coll.gameObject.tag == "Tla")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void DestroyGO()
    {
        Destroy(gameObject);
    }
}
