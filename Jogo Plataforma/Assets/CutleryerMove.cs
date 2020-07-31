using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutleryerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed,timer, timerB;
    public float timerShot, timerShotB;
    public float groundCheckRange;
    public Vector2 range;
    public LayerMask playerLayer, groundLayer;
    public GameObject[] projectiles;
    public bool moveRight, onRange, flipCheck = true, firstProjectile = true, canmove;
    int movedir;
    Transform player;
    public Transform[] checkGround;
    public Vector2 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("MainChar").GetComponent<Transform>();
        timer = timerB;
        timerShot = timerShotB/2;
    }
    private void FixedUpdate()
    {
        if (onRange)
        {
            rb.velocity = Vector2.right * (movedir * speed);
        }
        else
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }
    private void Update()
    {
        bool ground1 = Physics2D.Raycast(checkGround[0].position, Vector2.down, groundCheckRange, groundLayer);
        bool ground2 = Physics2D.Raycast(checkGround[1].position, Vector2.down, groundCheckRange, groundLayer);
        if (ground1 && ground2)
        {
            canmove = true;
        }
        else
        {
            speed *= -1;
        }
        onRange = Physics2D.OverlapBox(transform.position, range, 0, playerLayer);

        if (onRange)
        {
            if (timerShot>0)
            {
                timerShot -= Time.deltaTime;
            }
            else
            {
                if (firstProjectile)
                {
                    GameObject projectile = Instantiate(projectiles[Random.Range(0, projectiles.Length)], transform.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
                    firstProjectile = false;
                    timerShot = 0.2f;
                }
                else
                {
                    GameObject projectile = Instantiate(projectiles[Random.Range(0, projectiles.Length)], transform.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * 1.5f, direction.y + 1), ForceMode2D.Impulse);
                    firstProjectile = true;
                    timerShot = timerShotB;
                }
                
            }
            if (flipCheck)
            {
                Flip();
                flipCheck = false;
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Flip();
                speed *= -1;
                timer = timerB;
            }
        }
        else
        {
            flipCheck = true;
            timer = timerB;
        }
    }
    public void Flip()
    {
       
            if (player.position.x < transform.position.x)
            {

                transform.localScale = new Vector2(1, transform.localScale.y);
                movedir = -1;
                direction.x = -2.5f;
            }
            else if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
                movedir = 1;
                direction.x = 2.5f;
        }

        
    }
}
