using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyControl : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    FlipToPlayer flip;
    public LayerMask playerLayer;
    public int state;
    int canjump;
    public bool playerOnSight;
    public Vector2 boxSize;
    public float timer, timerB;
    public GameObject bullet1, bullet2;
    public Transform shotSpot;
    public bool canImpulse = true, isjumping;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flip = GetComponent<FlipToPlayer>();
        canjump = Random.Range(1,3);
    }
    private void Update()
    {
        playerOnSight = Physics2D.OverlapBox(transform.position, boxSize, 0, playerLayer);
        if (playerOnSight)
        {
            if (!isjumping)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (canjump == 0)
                    {
                        Jump();
                        state = 4;
                        anim.SetInteger("state", state);
                        timer = timerB;
                        canjump = 2;
                    }
                    else
                    {
                        state = Random.Range(1, 4);
                        anim.SetInteger("state", state);
                        timer = timerB;
                        canjump--;
                    }

                }
            }
            
        }
        else
        {
            timer = timerB/2;
        }
    }
    public void ResetState()
    {
        state = 0;
        anim.SetInteger("state", 0);
        
    }
    public void Shot(int whichShot)
    {
        if(whichShot == 0)
        {
            GameObject bullet = Instantiate(bullet1, shotSpot.position, Quaternion.identity);
            if (flip.isRight)
            {
                bullet.GetComponent<EnemyBullet>().dir = 1;
            }
            else
            {
                bullet.GetComponent<EnemyBullet>().dir = -1;
            }
        }
        else
        {
            GameObject bullet = Instantiate(bullet2, shotSpot.position, Quaternion.identity);
            if (flip.isRight)
            {
                bullet.GetComponent<EnemyBullet>().dir = 1;
            }
            else
            {
                bullet.GetComponent<EnemyBullet>().dir = -1;
            }
        }
    }
    public void Jump()
    {
        if(canImpulse)
        {
            Debug.Log("impulso");
            isjumping = true;
            if (flip.isRight)
            {
                rb.AddForce(new Vector2(3, 10), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-3, 10), ForceMode2D.Impulse);
            }
            
            canImpulse = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall") && isjumping)
        {
            state = 0;
            anim.SetInteger("state", state);
            timer = timerB/3;
            canjump = Random.Range(1, 3); ;
            isjumping = false;
            canImpulse = true;
        }
    }
}
