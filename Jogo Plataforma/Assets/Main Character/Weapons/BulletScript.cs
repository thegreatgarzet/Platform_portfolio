using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed, destroyTime;
    public int damage, dir, bulletHP = 3;
    public bool isBall, isHyper, isSound;
    public Rigidbody2D rb;
    public GameObject hit_fx, damageHit_fx;
    private void Start()
    {
        if (dir < 0)
        {

            transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
            
        }
        else if (dir== 1)
        {
            transform.localScale = new Vector2(transform.localScale.x * 1, transform.localScale.y);
            
        }
    }
    private void Update()
    {
        if (dir == -1 || dir == 1)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
            Destroy(gameObject, destroyTime);
        }else if (dir ==-2 || dir ==2)
        {
            rb.velocity = new Vector2(rb.velocity.x, dir * speed);
            Destroy(gameObject, destroyTime);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSound)
        {
            if (collision.CompareTag("inimigo"))
            {
                collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(damage);
                if (isHyper)
                {
                    Instantiate(damageHit_fx, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    bulletHP--;
                    if (bulletHP <= 0)
                    {

                        Instantiate(hit_fx, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
                else
                {

                    Instantiate(hit_fx, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    Destroy(gameObject);
                }


            }
            else
            {
                Instantiate(hit_fx, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                Destroy(gameObject);
            }
            if (!isBall)
            {
                //Instantiate(hit_fx, transform.position, Quaternion.identity);
                //Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("inimigo"))
            {
                collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(damage);
            }
            else if (collision.CompareTag("wall"))
            {
                dir *= -1;
                damage *= 2;
            }
        }

        //Instantiate(hit_fx, transform.position, Quaternion.identity);
    }

}
