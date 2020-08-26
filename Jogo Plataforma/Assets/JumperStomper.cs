using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperStomper : MonoBehaviour
{
    public Vector2 jumpdir;
    DetectPlayerOnRange playerOnRange;
    FlipToPlayer flip;
    Rigidbody2D rb;
    Animator anim;
    public DetectGroundwHitbox detectGround;
    public int dir;
    public bool onground, jumping=false;
    private void Awake()
    {
        flip = GetComponent<FlipToPlayer>();
        rb = GetComponent<Rigidbody2D>();
        playerOnRange = GetComponent<DetectPlayerOnRange>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!playerOnRange)
        {
            anim.SetTrigger("playeroutrange");
        }
        else
        {
            anim.SetTrigger("playeronrange");
        }
        if (detectGround.onGround)
        {
            if (jumping)
            {
                anim.SetTrigger("landing");
                jumping = false; 
            }

            rb.velocity = new Vector2(0.0f, 0.0f);
            if (playerOnRange._PlayerOnRange)
            {
                TriggerJump();
            }
        }
        else
        {
            EnableAll();
        }
        
    }
    public void TriggerJump()
    {
        anim.SetTrigger("jump");
    }
    public void Jump()
    {
        if (flip.isRight)
        {
            rb.AddForce(jumpdir, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(jumpdir.x * -1, jumpdir.y), ForceMode2D.Impulse);
        }
        jumping = true;
        flip.enabled = false;
        
    }
    public void EnableAll()
    {
        flip.enabled = true;
        
    }
}
