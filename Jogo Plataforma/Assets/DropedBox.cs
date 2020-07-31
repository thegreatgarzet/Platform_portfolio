using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedBox : MonoBehaviour
{
    public BoxCollider2D box;
    public bool onground;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !onground)
        {
            Destroy(gameObject);
        }
        if (collision.transform.CompareTag("wall"))
        {
            onground = true;
            
            transform.tag = "breakObject";
            gameObject.layer = 8;
        }
            
    }
}
