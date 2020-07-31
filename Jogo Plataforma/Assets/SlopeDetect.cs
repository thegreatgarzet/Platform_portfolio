using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetect : MonoBehaviour
{
    MovementController player;
    private void Start()
    {
        player = GetComponentInParent<MovementController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "slopes")
        {
            player.onSlope = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "slopes")
        {
            player.onSlope = false;
        }
    }
}
