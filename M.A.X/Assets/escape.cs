using UnityEngine;
using System.Collections;

public class escape : MonoBehaviour {

    public GameObject endPanel;
    bool stop = false;

    private void Update()
    {
        if (!stop && Input.GetKeyDown(KeyCode.Escape))
        {
            endPanel.SetActive(true);
            Time.timeScale = 0f;
            stop = true;
            Debug.Log("Nek izpis");
        }
        else if (stop && Input.GetKeyDown(KeyCode.Escape))
        {
            endPanel.SetActive(false);
            Time.timeScale = 1f;
            stop = false;
            Debug.Log("Nek izpis");
        }
    }
}
