using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChasingStar : MonoBehaviour
{
    public float speed;
    public Vector2 target;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
