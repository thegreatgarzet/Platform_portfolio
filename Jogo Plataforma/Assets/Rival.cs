using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour
{
    
    Transform player;
    public Transform midroom;
    Rigidbody2D rb;
    Animator anim;
    public int state;
    public bool onground, doubleshot, reset;
    public float speed, timer, movedir;
    float timerB;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("MainChar").GetComponent<Transform>();
        timerB = timer;
    }
    /*
     * 0 - IDLE
     * 1 - DASH
     * 2 - JUMP
     * 3 - JUMPDOUBLESHOT
     * 4 - GROUNDDOUBLESHOT
     * 5 - DASHEND
     * 6 - TAKINGDAMAGE
     * 7 - RESET
     * 8 - SPECIAL
     * */
    private void Update()
    {
        switch (state)
        {
            case 0:
                if (player.position.x<=midroom.position.x -4 && transform.position.x <= midroom.position.x - 4)
                {
                    reset = true;
                }
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    if (reset)
                    {
                        SetState(7);//RESET
                    }else
                    if (doubleshot)
                    {
                        SetState(4);//GROUNDDOUBLESHOT
                    }
                    else
                    {
                        if (player.position.x > transform.position.x)
                        {
                            movedir = 1;//Go RIGHT
                        }
                        else
                        {
                            movedir = -1;//Go LEFT
                        }
                        SetState(1);//DASH
                    }
                }
                break;
            case 1:
                if (movedir == 1)
                {
                    FlipIgnorePlayer(true);//flip to the right
                }
                else
                {
                    FlipIgnorePlayer(false);//flip to the left
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(midroom.position.x + (movedir * 4f), transform.position.y), speed * Time.deltaTime);
                if (transform.position.x == midroom.position.x + (movedir * 4f))
                {
                    SetState(5);//DASHEND
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                if (movedir==1)
                {
                    FlipIgnorePlayer(false);//flip to the left
                }
                else
                {
                    FlipIgnorePlayer(true);//flip to the right
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(midroom.position.x + (movedir * 4.5f), transform.position.y), speed * Time.deltaTime);
                if (transform.position.x == midroom.position.x + (movedir * 4.5f))
                {
                    timer = timerB;
                    SetState(0);//IDLE GROUND
                }
                break;
            case 6:
                break;
            case 7:
                if (movedir == -1)
                {
                    FlipIgnorePlayer(false);//flip to the left
                }
                else
                {
                    FlipIgnorePlayer(true);//flip to the right
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(midroom.position.x + (-movedir * 2f), transform.position.y), speed * Time.deltaTime);
                if (transform.position.x == midroom.position.x + (-movedir * 2f))
                {
                    reset = false;
                    timer = timerB;
                    SetState(0);//IDLEGROUND
                }
                break;
            case 8:
                break;
        }
    }
    public void FlipIgnorePlayer(bool boolean)
    {
        if (boolean)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            
        }
        else
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }
    public void SetState(int callstate)
    {
        state = callstate;
        anim.SetInteger("state", callstate);
    }

}
