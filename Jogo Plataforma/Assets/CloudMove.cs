using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public Vector2 startpoint, endpoint;
    public float speed;
    private void Awake()
    {
        //transform.position = startpoint;
        startpoint.y = transform.position.y;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(endpoint.x, transform.position.y), speed * Time.deltaTime);
        if(transform.position.x == endpoint.x)
        {
            transform.position = startpoint;
        }
    }
}
