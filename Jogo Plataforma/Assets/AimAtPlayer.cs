using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    public float speed;
    public Vector2 dir;
    public FlipToPlayer flip;
    bool isright;
    private void Awake()
    {
        dir = GameObject.Find("MainChar").GetComponent<Transform>().position;
        Flip();
        
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, dir, speed * Time.deltaTime);
        
    }
    public void Flip()
    {
        if (transform.position.x > dir.x)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            isright = false;
            dir.x += -5;
        }
        else
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isright = false;
            dir.x += 5;
        }
        
        //dir.y -= 2;
    }
}

