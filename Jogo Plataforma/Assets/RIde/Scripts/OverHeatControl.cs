using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeatControl : MonoBehaviour
{
    MovementController movement;
    public SpriteRenderer[] sprites;
    public bool overHeat;
    public Color colorBase, colorOverHeat;
    public float timer, timerB;
    private void Awake()
    {
        movement = GetComponent<MovementController>();
        timer = timerB;
    }
    private void Update()
    {
        if (overHeat)
        {
            if (timer > 0)
            {
                movement._OverHeated = true;
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.color = Color.Lerp(colorBase, colorOverHeat, Mathf.PingPong(Time.time, 0.5f));
                }
                timer -= Time.deltaTime;
            }
            else
            {
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.color = colorBase;
                }
                timer = timerB;
                overHeat = false;
                movement._OverHeated = false;
            }
            
        }
    }
}
