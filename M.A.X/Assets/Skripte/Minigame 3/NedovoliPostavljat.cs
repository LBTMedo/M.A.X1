﻿using UnityEngine;
using System.Collections;

public class NedovoliPostavljat : MonoBehaviour {

    public static bool IsValid = true;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseOver()
    {
        IsValid = false;
    }
    void OnMouseExit()
    {
        IsValid = true;
    }
}
