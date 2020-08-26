using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMovimentation : MonoBehaviour
{
    public float verticalSpeed, horizontalSpeed,timer, timerB, rayRange;
    public LayerMask groundLayer, playerLayer;
    public Rigidbody2D rb;
    public int movedir = -1;
    public bool up = true, isRight = false;
    public GameObject box;
    private void Start()
    {
        timer = timerB;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        MoveUpDown();
        MoveLeftRight();
    }
    private void Update()
    {
        bool wallInFront = Physics2D.Raycast(transform.position, Vector2.right * movedir, rayRange, groundLayer);
        bool dropOnPlayer = Physics2D.Raycast(transform.position, Vector2.down, rayRange * 2.5f, playerLayer);
        if (wallInFront)
        {
            Flip();
        }
        if (dropOnPlayer && box !=null)
        {

            box.transform.SetParent(null);
            box.GetComponent<Rigidbody2D>().simulated = true;
            box = null;
        }
    }
    public void Flip()
    {
        if (isRight)
        {
            movedir = -1;
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isRight = false;
        }
        else
        {
            movedir = 1;
            transform.localScale = new Vector2(1, transform.localScale.y);
            isRight = true;
        }
    
    }
    public void MoveUpDown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (up)
            {
                rb.velocity = Vector2.up * verticalSpeed;   
            }
            else
            {
                rb.velocity = Vector2.up * -verticalSpeed;
            }
        }
        else
        {
            if (up)
            {
                up = false;
            }
            else { up = true; }
            timer = timerB;
        }
    }
    public void MoveLeftRight()
    {
        rb.velocity = new Vector2((movedir) * horizontalSpeed, rb.velocity.y);
    }
}
