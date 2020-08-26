using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiperAtack : MonoBehaviour
{
    public bool canHiperAtack=true, hiperAtacking, canMoveup=true;
    public float timer, timerB;
    public MovementController player;
    public GameObject animationHiper1;
    public SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponent<MovementController>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Especial") && canHiperAtack)
        {
            if (player.isgrounded==false)
            {
                sprite.enabled = false;
                
                animationHiper1.SetActive(true);
                hiperAtacking = true;
            }
            
        }
        if (hiperAtacking)
        {
            player.rb.gravityScale = 0;
            if (canMoveup) { player.rb.velocity = Vector2.up * 5; } else
            {
                player.rb.velocity = Vector2.up * 1;
            }
            player.ispaused = true;
        }
        else
        {
            sprite.enabled = true;

            animationHiper1.SetActive(false);
        }
    }
}
