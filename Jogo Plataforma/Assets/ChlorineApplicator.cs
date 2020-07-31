using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChlorineApplicator : MonoBehaviour
{
    public float yVelocityPush, timer, timerB, timerMomentum, timerMomentumB;
    public bool onwater;
    public Rigidbody2D playerRB;
    private void Start()
    {
        timer = timerB;
        timerMomentum = timerMomentumB;
    }
    private void Update()
    {
        if (onwater)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * yVelocityPush;
               /* if (timerMomentum>0)
                {
                    timerMomentum -= Time.deltaTime;
                    GetComponent<Rigidbody2D>().velocity -= Vector2.up * (playerRB.velocity.y-1);
                }
                else
                {
                    
                    //timerMomentum = timerMomentumB;
                }*/
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            onwater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            onwater = false;
            if (playerRB != null)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, yVelocityPush - 2);
            }
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 0;
            timer = timerB;
            timerMomentum = timerMomentumB;
        }
    }
}
