using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector2 newpos;
    public bool followplayer, hasNewPos, atach;
    private void Awake()
    {
        player = GameObject.Find("MainChar").GetComponent<Transform>();
    }
    private void Update()
    {
        if (followplayer)
        {
            if (!atach)
            {
                if (transform.position != player.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, 9f * Time.deltaTime);
                }
                else if (transform.position == player.position)
                {
                    atach = true;
                }
            }
            else
            {
                transform.position = player.position;
            }
        }else if(!followplayer && hasNewPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, newpos, 7.5f * Time.deltaTime);
        }
    }
    public void ReturnCameraToPlayer()
    {
        hasNewPos = false;
        followplayer = true;
    }
}
