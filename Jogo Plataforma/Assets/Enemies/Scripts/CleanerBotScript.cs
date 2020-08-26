using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerBotScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject player;
    public LayerMask playerLayer;
    public float radius;

    //Atack control

    public bool canCharge = true, grounded, candetectGround=false;
    public bool movingChar,checkSide=true;
    public bool onRange, canAddTimer, alreadyMoved = false;
    public float timer, timerCharge, timerJump, speed, jumpforce, movedir, gravity, gCheckDis;
    public int state;
    public Transform checkPlayerH, g1, g2;
    public LayerMask groundLayer;
    public BoxCollider2D boxSpear;
    //
    //Smoke Spawn
    bool canSpawnSmoke;
    public GameObject smokeFx;
    public float smokeTimer, smokeTimerB;
    //

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("MainChar");
        gravity = rb.gravityScale;
        smokeTimer = smokeTimerB;
        //timer = timerb;
    }
    private void FixedUpdate()
    {
        if (movingChar)
        {
            switch (state)
            {
                case 0:
                    rb.velocity = new Vector2(0.0f, 0.0f);
                    break;
                case 1:
                    rb.velocity = new Vector2(movedir * speed, 0.0f);
                    break;
                case 2:
                    rb.velocity = new Vector2(movedir * 3, 1 * jumpforce);
                    break;
            }
        }
        
    }
    private void Update()
    {
        onRange = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        bool groundcheck1 = Physics2D.Raycast(g1.position, Vector2.down, gCheckDis, groundLayer);
        bool groundcheck2 = Physics2D.Raycast(g2.position, Vector2.down, gCheckDis, groundLayer);
        if (groundcheck1 || groundcheck2)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (onRange)
        {
            if (checkSide)
            {
                anim.SetInteger("state", 1);
                checkSide = false;
            }
            if (timer > 0)
            {
                switch (state)
                {
                    case 1:
                        CheckPlayerHeight();
                        if (state == 2)
                        {
                            timer = timerJump;
                            anim.SetInteger("state", 2);
                        }
                        else
                        {
                            anim.SetInteger("state", 1);
                        }
                        break;
                    case 2:
                        anim.SetInteger("state", 2);
                        break;
                }
                timer -= Time.deltaTime;
                if (timer <=0)
                {
                    alreadyMoved = true;
                }
            }
            else 
            {
                if (alreadyMoved)
                {
                    switch (state)
                    {
                        case 1:
                            anim.SetInteger("state", 0);
                            state = 0;

                            break;
                        case 2:
                            rb.gravityScale = gravity * 3;
                            if (grounded)
                            {
                                state = 0;

                               anim.SetInteger("state", 0);
                            }
                            break;
                    }
                    checkSide = true;
                    movingChar = false;
                    canAddTimer = true;
                    alreadyMoved = false;
                }
                
            }
        }
        else
        {
            timer = 0;
            state = 0;
            checkSide = true;
            movingChar = false;
            canCharge = true;
            anim.SetInteger("state", 0);
        }
        if (grounded && state == 2 && candetectGround)
        {
            state = 0;
            anim.SetInteger("state", 0);
            candetectGround = false;
            checkSide = true;
        }
        if (canSpawnSmoke)
        {
            if(smokeTimer > 0)
            {
                smokeTimer -= Time.deltaTime;
            }
            else
            {
                Instantiate(smokeFx, transform.position, Quaternion.identity);
                smokeTimer = smokeTimerB;
            }
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (state == 1)
            {
                timer = 0.05f;
            }
        }
    }
    public void FlipChar()
    {
        if (player.transform.position.x < transform.position.x)
        {

            transform.localScale = new Vector2(1, transform.localScale.y);
            movedir = -1;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            movedir = 1;
        }
        
    }
    public void CheckPlayerHeight()
    {
        if (player.transform.position.y > checkPlayerH.position.y)
        {
            rb.gravityScale = gravity;
            state = 2;
        }
        else
        {
            state = 1;
        }
    }
    public void StartMovingChar()
    {
        movingChar = true;
    }
    public void StopMoving()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
    }
    public void AddtimerDash()
    {
        timer = timerCharge;
    }
    public void AddtimerJump()
    {
        timer = timerJump;
    }
    public void CanDetectGround()
    {
        candetectGround = true;
    }
    public void StartSpawnSmoke()
    {
        canSpawnSmoke = true;
    }
    public void EndSpawnSmoke()
    {
        canSpawnSmoke = false;
    }
    public void ActivateSpear()
    {
        boxSpear.enabled = true;
    }
    public void DeactivateSpear()
    {
        boxSpear.enabled = false;
    }
}
