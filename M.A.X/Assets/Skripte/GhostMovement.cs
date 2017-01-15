using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour {

    public Transform end;
    private Vector3 endpos;
    private Vector3 start;
    public float speed = 5f;
    Rigidbody2D rbd;

    bool moveRight = true;

    public bool desno = true;
    // Use this for initialization
    void Start()
    {
        start = transform.position;
        endpos = end.position;
        Debug.Log(endpos.x);
        rbd = GetComponent<Rigidbody2D>();
        rbd.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x >= endpos.x && moveRight)
        {
            moveRight = false;
        }
        else if (transform.position.x <= start.x && !moveRight)
        {
            moveRight = true;
        }
    }

    void FixedUpdate()
    {
        if (moveRight)
        {
            rbd.velocity = new Vector2(speed, 0);
            transform.rotation = (new Quaternion(0, 0, 0, 0));
        }
        else
        {
            rbd.velocity = new Vector2(-speed, 0);
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.SendMessage("PrejmiSkodo", 20f);
            //Destroy(gameObject);
        }
    }
}
