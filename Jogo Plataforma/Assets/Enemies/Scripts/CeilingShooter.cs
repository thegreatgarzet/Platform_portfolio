using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CeilingShooter : MonoBehaviour
{
    DetectPlayerOnRange playerOnRange;
    Animator anim;
    public GameObject bullet;
    public float timer, timerB;
    private void Awake()
    {
        playerOnRange = GetComponent<DetectPlayerOnRange>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerOnRange._PlayerOnRange)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                anim.SetTrigger("shot");
                timer = timerB;
            }
        }
        else
        {
            timer = 0;
        }
    }
    public void Shot()
    {
        for (int i =-1; i < 2; i++)
        {
            GameObject projectile = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(i * 2.5f, -5);
        }
    }
}
