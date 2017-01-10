﻿using UnityEngine;
using System.Collections;

public class RocnoOrozje : MonoBehaviour {

    public float damage = 20f;
    public float cena;
    public string ime;
    public Transform handPoint;

    /*[SerializeField]
    private Sprite slikaOrozja;*/

    public Sprite VrniSliko()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Sovraznik")
        {
            coll.gameObject.SendMessage("PrejmiSkodo", damage);
        }
    }
}
