using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject chain;
    public float gravity;
    public bool onground;
    DetectPlayerOnRange playerOnRange;
    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        playerOnRange = GetComponent<DetectPlayerOnRange>();
    }
    private void Update()
    {
        //rb.velocity = new Vector2(0.0f, rb.velocity.y);
        if (chain == null && !onground)
        {
            Destroy(playerOnRange);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y -10), gravity*Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("wall"))
        {
            onground = true;
        }
    }
}

