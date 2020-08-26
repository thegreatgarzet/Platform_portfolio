using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationPlayerClose : MonoBehaviour
{
    public DetectPlayerOnRange playerOnRange;
    public string animationToTrigger;
    Animator anim;
    public float timer, timerB;
    private void Awake()
    {
        playerOnRange = GetComponent<DetectPlayerOnRange>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerOnRange!=null)
        {
            if (playerOnRange._PlayerOnRange)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    anim.SetTrigger(animationToTrigger);
                    timer = timerB;
                }
            }
            else
            {
                timer = 1;
            }
        }
    }
}

