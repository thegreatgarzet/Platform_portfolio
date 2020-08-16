using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public bool needToDetect;
    DetectPlayerOnRange playerOnRange;
    public Transform player;
    public float speed;
    private void Awake()
    {
        if (needToDetect)
        {
            playerOnRange = GetComponent<DetectPlayerOnRange>();
        }
        player = GameObject.Find("MainChar").GetComponent<Transform>();
    }
    private void Update()
    {
        if (needToDetect)
        {
            if (playerOnRange._PlayerOnRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, player.position.y+0.5f), speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        
    }
}
