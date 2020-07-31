using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetectWall : MonoBehaviour
{
    public bool detect=true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall") && detect)
        {
            Debug.Log("toqcou");
            gameObject.GetComponentInParent<BulletScript>().dir *= -1;
            gameObject.GetComponentInParent<Transform>().localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
            detect = false;
        }
    }
}
