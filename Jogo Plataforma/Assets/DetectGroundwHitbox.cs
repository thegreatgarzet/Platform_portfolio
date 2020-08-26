using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGroundwHitbox : MonoBehaviour
{
    public bool onGround, isTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger && collision.CompareTag("wall"))
        {
            onGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTrigger && collision.CompareTag("wall"))
        {
            onGround = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTrigger && collision.transform.CompareTag("wall"))
        {
            onGround = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isTrigger && collision.transform.CompareTag("wall"))
        {
            onGround = false;
        }
    }
}
