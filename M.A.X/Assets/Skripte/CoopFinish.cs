﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoopFinish : MonoBehaviour {

    bool prvi = false;
    bool drugi = false;

    public void SetPrvi(bool value)
    {
        prvi = value;
    }

    public void SetDrugi(bool value)
    {
        drugi = value;
    }

    public bool GetPrvi()
    {
        return prvi;
    }

    public bool GetDrugi()
    {
        return drugi;
    }
    
    void Update()
    {
        if (prvi && drugi)
        {
            Debug.Log("Konec.");
            GameControl.control.Save();
            GameControl.control.SaveDefault();
            SceneManager.LoadScene(0);
        }
    }

}
