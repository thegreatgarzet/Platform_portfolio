using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserSentinel : MonoBehaviour
{
    public MoveTowardsPlayer movetoplayer;
    Animator anim;
    Rigidbody2D rb;
    public bool spawned;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void StartMoving()
    {
        if (spawned)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
            rb.isKinematic = true;
            movetoplayer.enabled = true;
            spawned = false;
        }
        
    }
}
