using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubbles : MonoBehaviour
{
    public GameObject[] bubbles;
    public float timer, timerB;

    bool onWater;
    private void Start()
    {
      
        timer = timerB;
    }
    private void Update()
    {
        if (onWater)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Instantiate(bubbles[Random.Range(0, bubbles.Length)], transform.position, Quaternion.identity);
                timer = timerB;
            }
        }
        else
        {
            timer = timerB;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            onWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            onWater = false;
        }
    }
}
