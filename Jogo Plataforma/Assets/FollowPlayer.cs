using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player, newpos;
    public bool followplayer, hasNewPos;
    private void Awake()
    {
        player = GameObject.Find("MainChar").GetComponent<Transform>();
    }
    private void Update()
    {
        if (followplayer)
        {
            transform.position = player.position;
        }else if(!followplayer && hasNewPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, newpos.position, 7.5f * Time.deltaTime);
        }
    }
}
