using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBomberControl : MonoBehaviour
{
    Animator anim;

    public Transform midofroom;
    public Vector2[] positions, shotspots;
    public float speed, timer;
    float timerb;
    public int state, actualpos;
    public bool bomb, next, finishedbombing, start;
    public GameObject egg, bombobj;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        timerb = timer;
    }

    private void Update()
    {
        if (start)
        {
            switch (state)
            {
                case 0:
                    if (bomb)
                    {
                        anim.SetInteger("whichbomb", Random.Range(1, 4));
                        anim.SetTrigger("bomb");
                        bomb = false;
                    }
                    else
                    {
                        if (finishedbombing)
                        {
                            timer -= Time.deltaTime;
                            if (timer <= 0)
                            {
                                print("teste green");
                                if (actualpos == 0)
                                {
                                    actualpos++;
                                    state = 2;
                                }
                                else if (actualpos == 2)
                                {
                                    actualpos--;
                                    state = 1;
                                }
                                else if (actualpos == 1)
                                {
                                    print("debug");
                                    int rand = Random.Range(0, 2);
                                    if (rand == 1)
                                    {
                                        actualpos = 2;
                                        state = 2;
                                    }
                                    else
                                    {
                                        actualpos = 0;
                                        state = 1;
                                    }

                                }
                                timer = timerb;
                                finishedbombing = false;
                            }
                        }

                    }
                    break;
                case 1:

                    if (next)
                    {
                        anim.SetTrigger("turnleft");
                        next = false;
                    }
                    if (transform.position.x == midofroom.transform.position.x + positions[actualpos].x)
                    {
                        anim.SetTrigger("returnleft");
                        state = 0;
                    }
                    else
                    {
                        print("move");
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(midofroom.transform.position.x + positions[actualpos].x, transform.position.y), speed * Time.deltaTime);
                    }
                    break;
                case 2:
                    if (next)
                    {
                        anim.SetTrigger("turnright");
                        next = false;
                    }
                    if (transform.position.x == midofroom.transform.position.x + positions[actualpos].x)
                    {
                        anim.SetTrigger("returnright");
                        state = 0;
                    }
                    else
                    {
                        print("move");
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(midofroom.transform.position.x + positions[actualpos].x, transform.position.y), speed * Time.deltaTime);

                    }
                    break;
                case 3:
                    GetComponent<SpriteRenderer>().enabled = true;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, midofroom.transform.position.y + positions[3].y), (speed * 2.2f)  * Time.deltaTime);
                    if(transform.position.y == midofroom.transform.position.y + positions[3].y)
                    {
                        GetComponent<BossBasics>().ReleasePlayerControls();
                        state = 0;
                    }
                    break;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
    public void StartBoss()
    {
        start = true;
    }
    public void FinishBomb()
    {
        finishedbombing = true;
        next = true;
    }
    public void FinishTurn()
    {
        bomb = true;
    }
    public void Shot2()
    {
        Instantiate(egg, new Vector2(transform.position.x + shotspots[0].x, transform.position.y + shotspots[0].y), Quaternion.identity);
        Instantiate(bombobj, new Vector2(transform.position.x + shotspots[2].x, transform.position.y + shotspots[2].y), Quaternion.identity);
    }
    public void Shot1()
    {
        Instantiate(bombobj, new Vector2(transform.position.x + shotspots[1].x, transform.position.y + shotspots[1].y), Quaternion.identity);
    }
    public void Shot3()
    {
        Instantiate(bombobj, new Vector2(transform.position.x + shotspots[0].x, transform.position.y + shotspots[0].y), Quaternion.identity);
        Instantiate(egg, new Vector2(transform.position.x + shotspots[1].x, transform.position.y + shotspots[1].y), Quaternion.identity);
        Instantiate(bombobj, new Vector2(transform.position.x + shotspots[2].x, transform.position.y + shotspots[2].y), Quaternion.identity);
    }
}

