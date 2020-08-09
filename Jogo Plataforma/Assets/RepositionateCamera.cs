using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionateCamera : MonoBehaviour
{
    public FollowPlayer camerafollow;
    public Vector2 newpos;
    public GameManager gameManager;
    public bool cancolide;
    public PolygonCollider2D colliderobj;
    private void Awake()
    {
        camerafollow = GameObject.Find("CameraFollow").GetComponent<FollowPlayer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cancolide)
            {
                gameManager.PausePlayer();
                /*camerafollow.atach = false;
                camerafollow.newpos = newpos;
                camerafollow.followplayer = false;
                camerafollow.hasNewPos = true;
                */
                gameManager.ChangeCameraToBoss(colliderobj);
                cancolide = false;
            }
        }
    }
}
