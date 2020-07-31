using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvencibleBlink : MonoBehaviour
{
    public bool canblink;
    public float blinkTimer, blinkTimerB, timer;
    public SpriteRenderer spriteMain;
    private void Awake()
    {
        spriteMain = GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {
        if (canblink)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                
                if (blinkTimer > 0)
                {
                    blinkTimer -= Time.deltaTime;
                }
                else
                {
                    if (spriteMain.enabled)
                    {
                        spriteMain.enabled = false;
                    }
                    else
                    {
                        spriteMain.enabled = true;
                    }
                    blinkTimer = blinkTimerB;
                }
            }
            else
            {
                spriteMain.enabled = true;
                canblink = false;
            }
        }
    }
}
