using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMIniGame3 : MonoBehaviour {


    Text tekst;
    bool Started = false;
    int repetition = 0;
    // Use this for initialization
    void Start () {
    	
        tekst = GameObject.Find("TextStart").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void StartGAme()
    {
        if (!Started)
        {
            Time.timeScale = 1f;
            repetition++;
            tekst.text = "END GAME";
            Started = true;
        }
        else if(Started)
        {
            SceneManager.LoadScene(9);
        }
    }
}
