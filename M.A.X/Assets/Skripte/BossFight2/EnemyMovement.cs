using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float speed = 1f;
    [SerializeField]
    int index;

    Igralec igralec;

    public Vector2[] path;

    public bool dead;

    bool desno = true;

    private void Start()
    {
        index = 0;
        dead = false;
        igralec = FindObjectOfType<Igralec>();
    }

    public void UpdatePath(Vector2[] _path)
    {
        path = _path;
        index = 0;
    }

    private void Update()
    {
        desno = (transform.position.x < igralec.gameObject.transform.position.x) ? true : false;
    }

    private void FixedUpdate()
    {
        if (index < path.Length - 1 && !dead)
        {
            Vector2 dir = path[index + 1] - (Vector2)transform.position;
            transform.Translate(dir.normalized * speed * Time.fixedDeltaTime, Space.World);
            if (Vector2.Distance(transform.position, path[index + 1]) < 0.1f)
            {
                index++;
            }
        }

        if (desno)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
    }
}
