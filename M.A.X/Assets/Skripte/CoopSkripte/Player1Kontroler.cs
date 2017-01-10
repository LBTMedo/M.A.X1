﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Player1Kontroler : MonoBehaviour {

    Igralec1 player;

    [SerializeField]
    private float moveSpeed = 20f;
    private float jumpHeight = 15f;
    private float sprintSpeed = 30f;
    private float originalMoveSpeed;
    public float airControll = 1; //air velocity multiplier

    public AudioClip zvok;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    public LayerMask Ground;
    public Transform groundCheck;

    public bool grounded;
    public bool disabled = false;
    public bool powerup = false;

    private bool facingRight;
    private bool jump;

    public Vector3 currentScale;


    public bool desno
    {
        get
        {
            return facingRight;
        }
        private set
        {
            facingRight = value;
        }
    }

    public float hitrostPremikanje
    {
        set { moveSpeed = value; }
    }

    private int Jumps; //num of current jumps
    public int originalJumps = 2; //the original num of jumps

    private Rigidbody2D rbd;

    Animator anim;

    // Use this for initialization
    void Start()
    {

        source = GetComponent<AudioSource>();
        player = GetComponent<Igralec1>();
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        originalMoveSpeed = player.movementSpeed;
        jumpHeight = player.jumpHeight;
        sprintSpeed = player.sprintSpeed;

        moveSpeed = originalMoveSpeed;

        facingRight = true;
        jump = false;
        Jumps = originalJumps;

        currentScale = transform.localScale;
    }

    void Update()
    {
        if (!disabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Jumps != 0)
            {
                if(Jumps == 1)
                {
                    anim.SetBool("frontflip", true);
                }
                else
                {
                    anim.SetBool("jump", true);
                }
                Debug.Log("Space");
                jump = true;
                transform.parent = null;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDir;

        if (!disabled)
        {
            if (powerup)
            {
                StartCoroutine(PowerUpPlayer());
            }
            if (powerup && Grounded())
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = originalMoveSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("A");
                moveDir = new Vector2(-moveSpeed, rbd.velocity.y);
                facingRight = false;
                anim.SetFloat("speed", 1f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("D");
                moveDir = new Vector2(moveSpeed, rbd.velocity.y);
                facingRight = true;
                anim.SetFloat("speed", 1f);
            }
            else
            {
                moveDir = new Vector2(0, rbd.velocity.y);
                
                anim.SetFloat("speed", 0f);
            }



            if (!Grounded())
            {
                anim.SetBool("jump", true);
                anim.SetFloat("speed", 0f);
                Debug.Log(airControll);
                moveDir.x = airControll * moveDir.x;
            }

            flip();

            rbd.velocity = moveDir; //set player velocity (x axis)        

            if (Grounded())
            {
                anim.SetBool("jump", false);
                anim.SetBool("frontflip", false);
                Jumps = originalJumps;
            }

            if (jump)
            {
                source.PlayOneShot(zvok, 1F);
                rbd.velocity = new Vector2(rbd.velocity.x, jumpHeight);
                jump = false;
                Jumps--;
            }
        }
    }

    bool Grounded() //returns true if player is on the "Ground" layer
    {
        grounded = Physics2D.Raycast(groundCheck.position, -Vector2.up, 0.3f, Ground);
        return grounded;
        //return Physics2D.OverlapCircle(groundCheck.position, 0.1f, Ground); 
    }

    void flip() //function to flip player left or right
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1); //turn right ----> to spremni pol v 1.5, 1.5, 1
        }
        else if (!facingRight)
        {
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1); //turn left -------> to spremni pol v obratno
        }
    }

    IEnumerator PowerUpPlayer()
    {
        powerup = true;
        yield return new WaitForSeconds(3);
        powerup = false;
    }
}
