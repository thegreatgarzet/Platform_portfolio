using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public float speed, destroyTime, rand, angle, angleUp, angleDown;
    public int damage, dir;

    public Rigidbody2D rb;
    private void Start()
    {
        
        switch (rand)
        {
            case 1:
                angle = angleUp;
                break;
            case 2:
                angle = 0;
                break;
            case 3:
                angle = angleDown;
                break;
        }
        if (dir < 0)
        {

            transform.localScale = new Vector2(-2, transform.localScale.y);

        }
        else if (dir > 0)
        {
            transform.localScale = new Vector2(2, transform.localScale.y);

        }
    }
    private void Update()
    {
        
        rb.velocity = new Vector2(dir * speed, angle);
        
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(damage);
        }
        Destroy(gameObject);
        //Destroy(gameObject);
    }
}
