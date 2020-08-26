using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    public float timerResetCanMove, timerResetCanMoveB, timerInvencible, timerInvencibleB, recoilForce;
    public float timerBlink, timerBlinkB;
    public MovementController movement;
    public bool gotHit=false, canRecoilMove=true;
    public SpriteRenderer spriteMain;
    public GameObject spriteDamage;
    public Rigidbody2D rb;
    public bool oncePerHit=true;
    private void Start()
    {
        movement = GetComponent<MovementController>();
        spriteMain = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (gotHit)
        {
            if (movement.canReceiveKnockback)
            {
                TomandoDano();
                movement.canmove = false;
                //movement.wallJumping = false;
                //movement.timerWall = 0;
                if (canRecoilMove)
                {
                    
                    if (movement.isRight)
                    {
                        rb.velocity = Vector2.right * -recoilForce;
                    }
                    else
                    {
                        rb.velocity = Vector2.right * recoilForce;
                    }

                }
            }
            
            
            timerResetCanMove -= Time.deltaTime;
            if (timerResetCanMove<=0)
            {
                movement.canmove = true;
                canRecoilMove = false;   
                ReturnSprite();
                if (oncePerHit)
                {
                    movement.isjumping = false;
                    movement.jumptimer = movement.jumptimerB;
                    
                    oncePerHit = false;
                }
                timerInvencible -= Time.deltaTime;
                if (timerInvencible<=0)
                {
                    timerInvencible = timerInvencibleB;
                    timerResetCanMove = timerResetCanMoveB;
                    canRecoilMove = true;
                    oncePerHit = true;
                    movement.canReceiveDamage = true;
                    gotHit = false;
                }
            }
        }
    }
    public void TomandoDano()
    {
        spriteMain.enabled = false;
        spriteDamage.SetActive(true);
        if (timerBlink > 0)
        {
            timerBlink -= Time.deltaTime;
        }
        else
        {
            if (spriteDamage.GetComponent<SpriteRenderer>().enabled)
            {
                spriteDamage.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                spriteDamage.GetComponent<SpriteRenderer>().enabled = true;
            }
            timerBlink = timerBlinkB;
        }

    }
    public void ReturnSprite()
    {
        spriteDamage.SetActive(false);
        spriteMain.enabled = true; 
    }
}
