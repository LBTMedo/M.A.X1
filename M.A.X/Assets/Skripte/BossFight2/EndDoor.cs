using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour {

    public int sceneIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(vrata());
        }
    }

    public IEnumerator vrata()
    {
        yield return new WaitForSeconds(2);
        GameControl.control.Save();
        GameControl.control.SaveDefault();
        GameControl.control.currentLevel = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
