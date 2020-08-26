using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollisionAnimTrigger : MonoBehaviour
{
    public Animator anim;
    public float timetrigger;
    public string _TriggerName;
    public bool trigger = false, cancolide;

    private void Update()
    {
        if (trigger)
        {
            timetrigger -= Time.deltaTime;
            if(timetrigger <= 0)
            {
                anim.SetTrigger(_TriggerName);
                trigger = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("RideArmor"))
        {
            if (cancolide)
            {
                trigger = true;
                cancolide = false;
            }
            
        }
        
    }
}
