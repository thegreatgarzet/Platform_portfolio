using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesPlayerOnRange : MonoBehaviour
{
    public GameObject inimigo;
    public bool playerOnRange;
    public float timer, timerB;
    private void Update()
    {
        if (playerOnRange)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SpawnPlayer();
                timer = timerB;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("RideArmor"))
        {
            playerOnRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("RideArmor"))
        {
            playerOnRange = false;
            
        }
    }
    public void SpawnPlayer()
    {
        Instantiate(inimigo, transform.position, Quaternion.identity);
    }
}
