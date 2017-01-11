using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IgralecKontroler))]
public class Igralec : MonoBehaviour {
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

    bool stop;

    public GameObject endPanel;

    void Start ()
    {
        source = GetComponent<AudioSource>();
        indikator = GetComponent<HitIndicator>();
        trenutnaZivljenja = zacetnaZivljenja;
        Debug.Log(trenutnaZivljenja);
        originalenScale = transform.localScale;
        kontroler = GetComponent<IgralecKontroler>();
        stop = false;
    }

    private void Update()
    {
        if (!disabled)
        {
            if (trenutnaZivljenja <= 0 && !mrtev)
            {
                Smrt();
                smrti++;
                GameControl.control.smrti += smrti;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.DodajDenar(10);
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
        trenutnaZivljenja -= skoda;
        health.CurrentVal -= skoda;
        indikator.damaged = true;
        source.PlayOneShot(zvok, GameControl.control.MASTER * GameControl.control.SFX);
    }

    public void Smrt()
    {
       
        source.PlayOneShot(zvokUmri, GameControl.control.MASTER * GameControl.control.SFX);
        mrtev = true;
        //Debug.Log("Smrt");
        /*if (ObSmrti != null)
        {
            ObSmrti();
        }*/
        endPanel.SetActive(true);
        Destroy(gameObject);
    }

    public void DodajZivlenja(float ammount)
    {
        trenutnaZivljenja += ammount;
        health.CurrentVal += ammount;
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
