using UnityEngine;
using System.Collections;

public class CoinPickup1 : MonoBehaviour {

    public int value = 1;
    public AudioClip zvokCoin;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            source.PlayOneShot(zvokCoin, GameControl.control.MUSIC * GameControl.control.SFX);
            GameManager.DodajDenar(value);
            Destroy(transform.parent.gameObject);
        }
    }
}
