using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleControl : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCol;
    public List<GameObject> enemiesList;
    public float speed, timer, timerB, gravityPull;
    public bool move=true;
    public int dir, pulls;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        if (move)
        {
            rb.velocity = new Vector2(speed * dir, 2);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void Update()
    {
        if (move)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                move = false;
            }
        }
        else
        {
            boxCol.enabled = true;
            if(enemiesList.Count > 0 && pulls>0)
            {
                GravityPulse();       
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo")|| collision.CompareTag("breakObject")) 
        {
            enemiesList.Add(collision.gameObject);
        }
    }
    public void GravityPulse()
    {
        foreach (GameObject enemie in enemiesList)
        {
            enemie.GetComponent<Rigidbody2D>().gravityScale = 0;
            enemie.GetComponent<Rigidbody2D>().velocity = Vector2.up * gravityPull;
        }
    }
    public void ReducePulls()
    {
        pulls--;
        if (pulls <= 0)
        {
            foreach (GameObject enemie in enemiesList)
            {
                enemie.GetComponent<Rigidbody2D>().gravityScale = 4;
                
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
