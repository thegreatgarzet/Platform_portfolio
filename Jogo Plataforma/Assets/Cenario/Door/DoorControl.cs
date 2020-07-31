using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    Animator anim;
    public bool open=false;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!open)
            {
                anim.SetBool("open", true);
                open = true;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (open)
        {
            anim.SetBool("open", false);
            open = false;
        }
    }
}
