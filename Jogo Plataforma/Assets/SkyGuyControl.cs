using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyGuyControl : MonoBehaviour
{
    DetectPlayerOnRange playerOnRange;
    public LayerMask playerLayer;
    Vector2 newpos;
    public Transform playerpos, shotspot;
    public GameObject bulletobj;
    Animator anim;
    public string state;
    public bool start, playerclose, checkupdown=true, changedir=true, canshot = true;
    public float speed, timer, range, distance, minYvalue;
    int playerdir, rand, aim;
    float timerb;
    
    
    private void Awake()
    {
        timerb = timer;
        playerpos = GameObject.Find("MainChar").GetComponent<Transform>();
        playerOnRange = GetComponent<DetectPlayerOnRange>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckPlayerPos();
        if (playerOnRange._PlayerOnRange)
        {
            start = true;
        }
        playerclose = Physics2D.OverlapBox(transform.position, new Vector2(range, range), 0.0f, playerLayer);
        if (start)
        {
            anim.SetInteger("aim", aim);
            switch (state)
            {
                case "followplayer":
                    changedir = true;
                    canshot = true;
                    aim = 3;
                    transform.position = Vector2.MoveTowards(transform.position, playerpos.position, speed * Time.deltaTime);
                    if (playerclose)
                    {
                        state = "goaway";
                    }
                    break;
                case "goaway":
                    Aim();
                    if (checkupdown)
                    {
                        rand = Random.Range(0, 2);
                        if (rand == 0) { rand = 1; } else { rand = -1; }
                        newpos = new Vector2(transform.position.x + (distance * playerdir), transform.position.y + (distance * rand));
                        if(newpos.y < minYvalue)
                        {
                            newpos.y = (minYvalue + 0.1f);
                        }
                        checkupdown = false;
                    }
                    
                    if (transform.position.y > minYvalue)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, newpos, speed * Time.deltaTime);
                    }
                    else
                    {
                        state = "stayaway";
                    }
                    
                    if(transform.position.x == newpos.x && transform.position.y == newpos.y)
                    {
                        checkupdown = true;
                        state = "stayaway";
                    }
                    break;
                case "stayaway":
                    if (checkupdown)
                    {
                        //rand = Random.Range(0, 2);
                        rand *= -1;
                        checkupdown = false;
                    }
                    if (rand == 0) { rand = 1; } else { rand = -1; }
                    if (playerclose)
                    {
                        if (changedir)
                        {
                            if (transform.position.y > minYvalue)
                            {
                                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + (range * playerdir), transform.position.y + (range * rand)), (speed * 2) * Time.deltaTime);
                            }
                            else
                            {
                                changedir = false;
                            }
                        }
                        else
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + (range * playerdir), transform.position.y + range), (speed * 2) * Time.deltaTime);
                        }
                        
                        
                    }
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        if (playerclose)
                        {
                            rand *= -1;
                            timer = timerb;
                        }
                        else
                        {
                            timer = timerb;
                            checkupdown = true;
                            state = "followplayer";   
                        }
                    }
                    break;
            }
        }
        
    }
    public void Aim()
    {
        if (transform.position.y > playerpos.position.y+2)
        {
            aim = 2;
        }else if (transform.position.y < playerpos.position.y - 2)
        {
            aim = 0;
        }else if (transform.position.y > playerpos.position.y - 2 && transform.position.y < playerpos.position.y + 2)
        {
            aim = 1;
        }
    }
    public void CheckPlayerPos()
    {
        if (playerpos.position.x < transform.position.x)
        {
            playerdir = 1;
        }
        else
        {
            playerdir = -1;
        }
    }
    public void Shot()
    {
        
        if (canshot)
        {
            Vector2 bulletdir;
            bulletdir.x = 4 * -playerdir;
            if (aim == 0)
            {
                bulletdir.y = 4;
                Rigidbody2D bullet = Instantiate(bulletobj, shotspot.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bullet.GetComponent<EnemyBullet>().trepass = true;
                bullet.velocity = bulletdir;
                canshot = false;
            }
            else if(aim ==1)
            {
                bulletdir.y = 0;
                Rigidbody2D bullet = Instantiate(bulletobj, shotspot.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bullet.GetComponent<EnemyBullet>().trepass = true;
                bullet.velocity = bulletdir;
                canshot = false;
            }
            else if(aim ==2)
            {
                bulletdir.y = -4;
                Rigidbody2D bullet = Instantiate(bulletobj, shotspot.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bullet.GetComponent<EnemyBullet>().trepass = true;
                bullet.velocity = bulletdir;
                canshot = false;
            }
            
        }
        
    }
}
