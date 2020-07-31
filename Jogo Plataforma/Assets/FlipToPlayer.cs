using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipToPlayer : MonoBehaviour
{
    public Transform player;
    public bool isRight;
    private void Awake()
    {
        player = GameObject.Find("MainChar").GetComponent<Transform>();
    }
    private void Update()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            isRight = false;
        }
        else
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isRight = true;
        }
    }
}
