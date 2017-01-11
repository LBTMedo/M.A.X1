using UnityEngine;
using System.Collections;

public class Sovraznik_Borba : MonoBehaviour {

    public float hitrostMetka;
    public Transform tockaZaStreljanje;
    public Rigidbody2D metek;

    private Sovraznik sovraznik;
    private GameObject igralec;

    private Vector3 target;

    [SerializeField]
    private bool desno;

    public bool umrl = false;

    public void Start()
    {
        sovraznik = GetComponent<Sovraznik>();
        desno = !sovraznik.left;
        igralec = FindObjectOfType<Igralec>().gameObject;
    }

    /*public IEnumerator Streljanje()
    {
        target = (igralec.transform.position - tockaZaStreljanje.position).normalized;

        for (;;)
        {
            Rigidbody2D instancaMetka = Instantiate(metek, tockaZaStreljanje.position, tockaZaStreljanje.rotation) as Rigidbody2D;
            if (!sovraznik.left)
            {
                instancaMetka.velocity = hitrostMetka * instancaMetka.transform.right;
            }
            else if(sovraznik.left)
            {
                instancaMetka.velocity = hitrostMetka * (-instancaMetka.transform.right);
            }

            yield return new WaitForSeconds(0.5f);

        }
    }*/

    void Update()
    {
        desno = !sovraznik.left;
    }

    public void Streljanje()
    {
        if (!umrl)
        {
            Rigidbody2D instancaMetka = Instantiate(metek, tockaZaStreljanje.position, tockaZaStreljanje.rotation) as Rigidbody2D;
            instancaMetka.velocity = hitrostMetka * transform.right;
        }
    }
}
