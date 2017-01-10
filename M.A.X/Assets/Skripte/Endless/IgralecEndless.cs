﻿using UnityEngine;
using System.Collections;

public class IgralecEndless : MonoBehaviour {

    public AudioClip zvokUmri;
    public AudioClip zvok;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    int smrti = 0;
    public float trenutnaZivljenja;
    protected bool mrtev = false;

    public float movementSpeed;
    public float jumpHeight;
    public float sprintSpeed;

    public float zacetnaZivljenja;
    public event System.Action ObSmrti;

    private Vector3 originalenScale;

    [SerializeField]
    private IgralecStat health;

    HitIndicator indikator;

    private IgralecKontroler kontroler;

    public bool disabled = false;

    GenerateLevel generator;

    float orgTimeScale;

    public bool invincible = false;

    bool stop;
    public GameObject endPanel;

    void Start()
    {
        source = GetComponent<AudioSource>();
        indikator = GetComponent<HitIndicator>();
        trenutnaZivljenja = zacetnaZivljenja;
        Debug.Log(trenutnaZivljenja);
        originalenScale = transform.localScale;
        kontroler = GetComponent<IgralecKontroler>();

        orgTimeScale = Time.timeScale;

        generator = FindObjectOfType<GenerateLevel>();
        stop = false;
    }

    void Update()
    {
        if (!disabled)
        {
            if (Input.GetKeyDown(KeyCode.Numlock))
            {
                trenutnaZivljenja -= 20;
                health.CurrentVal -= 20;
                Debug.Log(trenutnaZivljenja);
                transform.localScale = new Vector3(4, 4, 0);
            }

            if (trenutnaZivljenja <= 0 && !mrtev)
            {
                Smrt();
                smrti++;
                GameControl.control.smrti += smrti;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                health.CurrentVal -= 10;
                indikator.damaged = true;
                if (indikator.sceneLoad == true)
                {
                    indikator.sceneLoad = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                health.CurrentVal += 100;
            }
        }

        if (invincible)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

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

    private void Awake()
    {
        health.Initialize();
    }

    public void PrejmiSkodo(float skoda)
    {
        if (!invincible)
        {
            trenutnaZivljenja -= skoda;
            health.CurrentVal -= skoda;
        }
        //source.PlayOneShot(zvok, GameControl.control.MASTER * GameControl.control.SFX);
    }

    public void Smrt()
    {

        //source.PlayOneShot(zvokUmri, GameControl.control.MASTER * GameControl.control.SFX);
        mrtev = true;
        //Debug.Log("Smrt");
        /*if (ObSmrti != null)
        {
            ObSmrti();
        }*/

        generator.Konec();

        Time.timeScale = 0;
       
    }

    public void EndGame()
    {
        Time.timeScale = orgTimeScale;
        GameManager.RestartGame();
        Destroy(gameObject);
    }

    public void Restart()
    {
        Time.timeScale = orgTimeScale;
        GameManager.OdZacetka();
        Destroy(gameObject);
    }

    public void DodajZivlenja(float ammount)
    {
        trenutnaZivljenja += ammount;
    }

    void Povecaj()
    {
        kontroler.currentScale = new Vector3(originalenScale.x * 2, originalenScale.y * 2);
    }

    void Pohitri()
    {
        kontroler.hitrostPremikanje = sprintSpeed;
    }

    void Ponastavi()
    {
        kontroler.currentScale = originalenScale;
        kontroler.hitrostPremikanje = movementSpeed;
    }

    void Enlarge()
    {
        Povecaj();
        Pohitri();
        Invoke("Ponastavi", 3f);
    }

    /* void OnColliderEnter2D(Collider2D other)
     {
         if(other.tag == "Sovraznik")
         {
             Sovraznik enemy = other.gameObject.GetComponent<Sovraznik>();
             if(enemy.vrsta == Sovraznik.Vrsta.leteci)
             {
                 PrejmiSkodo(100f);
                 Destroy(other.gameObject);
             }
         }
     }*/
}