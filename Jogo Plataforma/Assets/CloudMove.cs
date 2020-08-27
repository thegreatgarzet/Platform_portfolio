using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public Vector2 startpoint, endpoint;
    public float speed;
    private void Awake()
    {
        transform.position = startpoint;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endpoint, speed * Time.deltaTime);
        if(transform.position.x == endpoint.x)
        {
            transform.position = startpoint;
        }
    }
}
