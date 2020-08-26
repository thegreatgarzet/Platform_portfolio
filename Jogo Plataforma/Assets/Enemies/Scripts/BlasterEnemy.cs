using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterEnemy : MonoBehaviour
{
    DetectPlayerOnRange detectplayer;
    Animator anim;
    public float timer, timerB;
    public GameObject projectile;
    private void Awake()
    {
        detectplayer = GetComponent<DetectPlayerOnRange>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (detectplayer._PlayerOnRange)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                anim.SetTrigger("shoot");
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
        Rigidbody2D bullet1 = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity).GetComponent<Rigidbody2D>();
        bullet1.AddForce(new Vector2(1.5f, 6f), ForceMode2D.Impulse);
        Rigidbody2D bullet2 = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity).GetComponent<Rigidbody2D>();
        bullet2.AddForce(new Vector2(-1.5f, 6f), ForceMode2D.Impulse);
    }
}
