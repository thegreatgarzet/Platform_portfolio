using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedTurtle : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 impulse;
    Vector2 actualimpulse;
    public Vector2 newpos;
    float originalgravity, timer, timertoidle;
    public FlipToPlayer flip;
    DetectPlayerOnRange playerOnRange;
    Animator anim;
    AudioControl audioControl;
    GameManager gameManager;
    public bool dashing, canchecknewpos;
    public string state;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalgravity = rb.gravityScale;
        flip = GetComponent<FlipToPlayer>();
        playerOnRange = GetComponent<DetectPlayerOnRange>();
        anim = GetComponent<Animator>();
        timertoidle = 2f;
        gameManager = FindObjectOfType<GameManager>();
        audioControl = FindObjectOfType<AudioControl>();
    }
    private void FixedUpdate()
    {
        if (dashing)
        {
            rb.gravityScale = 0.0f;
            if (canchecknewpos)
            {
                
                newpos = transform.position;
                newpos.y += 0.3f;
                canchecknewpos = false;
            }
            else if(transform.position.y != newpos.y)
            {
                anim.SetTrigger("moving");
                transform.position = Vector2.MoveTowards(transform.position, newpos, 1 * Time.deltaTime);
                print("moving towards");
            }else if (transform.position.y == newpos.y)
            {
                rb.AddForce(actualimpulse, ForceMode2D.Force);
                
            }
            
        }
    }
    private void Update()
    {
        
        switch (state)
        {
            case "idle":
                
                flip.enabled = true;
                if (flip.isRight)
                {
                    actualimpulse.x = impulse.x;
                }
                else
                {
                    actualimpulse.x = -impulse.x;
                }
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    if (playerOnRange._PlayerOnRange)
                    {
                        timer = 2f;
                        state = "dash";
                    }
                }
                
                break;
            case "dash":
                flip.enabled = false;
                dashing = true;
                break;
            case "hitwall":
                flip.enabled = false;
                rb.gravityScale = originalgravity;
                timertoidle -= Time.deltaTime;
                if(timertoidle <= 0)
                {
                    timertoidle = 2f;
                    state = "idle";
                }
                
                break;
        }
        if (!playerOnRange._PlayerOnRange)
        {
            state = "idle";
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        dashing = false;
        state = "hitwall";
        anim.SetTrigger("idle");
        canchecknewpos = true;
        if (playerOnRange._PlayerOnRange)
        {
            gameManager.CameraShake(0.5f, gameManager.basicEnemiesIntensity);
            audioControl.PlaySound("rumble");
        }
        if (flip.isRight)
        {
            rb.AddForce(new Vector2(-impulse.x * 3, impulse.y), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(impulse.x * 3, impulse.y), ForceMode2D.Force);
        }
            
        
    }
}
