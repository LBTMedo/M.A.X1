using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KonecIgre : MonoBehaviour {
    public GameObject endPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            endPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
