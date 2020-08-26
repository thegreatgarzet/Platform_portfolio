using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public int state;
    public LayerMask groundLayer;
    public float raycast, jump, push;
    public float timer, timerB;
    public bool onGround, jumped, checkGround;
    FlipToPlayer flip;
    DetectPlayerOnRange playeronrange;
    private void Start()
    {
        timer = timerB;
        flip = GetComponent<FlipToPlayer>();
        playeronrange = GetComponent<DetectPlayerOnRange>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        bool isGrounded1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.1f, transform.position.y - 0.05f), transform.position, raycast, groundLayer);
        bool isGrounded2 = Physics2D.Raycast(new Vector2(transform.position.x + -0.1f, transform.position.y - 0.05f), transform.position, raycast, groundLayer); ;
        if(isGrounded1 || isGrounded2)
        {
            onGround = true;
            if (checkGround)
            {
                if (jumped)
                {
                    anim.SetInteger("state", 2);
                    jumped = false;
                }
                else
                {
                    anim.SetInteger("state", 0);
                    checkGround = false;
                }
            }
            
        }else if (!isGrounded1 || !isGrounded2)
        {
            onGround = false;
        }
        if (playeronrange._PlayerOnRange)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Jump();
                timer = timerB;
            }
        }
        
        
    }
    public void EnableCheckGround()
    {
        checkGround = true;
    }
    public void Jump()
    {
        jumped = true;
        anim.SetInteger("state", 1);
        Vector2 impulseVector;
        if (flip.isRight)
        {
            impulseVector = new Vector2(push, jump);
        }
        else
        {
            impulseVector = new Vector2(push * -1, jump);
        }
        rb.AddForce(impulseVector, ForceMode2D.Impulse);
    }
    
}
