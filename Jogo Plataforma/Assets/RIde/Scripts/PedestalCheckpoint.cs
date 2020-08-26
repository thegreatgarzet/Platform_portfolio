using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalCheckpoint : MonoBehaviour
{
    GameManager gameManager;
    public Transform rayPos;
    public float raydist;
    public LayerMask playerLayer;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        bool playerOnTop = Physics2D.Raycast(rayPos.position, Vector2.up, raydist, playerLayer);
        /*if (playerOnTop)
        {
            gameManager.checkPoint = rayPos;
            gameManager.canCallSaveMenu = true;
        }
        else
        {
            gameManager.canCallSaveMenu = false;
        }*/
    }
}
