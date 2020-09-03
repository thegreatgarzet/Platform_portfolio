using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTransparency : MonoBehaviour
{
    public SpriteRenderer[] clouds;
    public bool enable;
    public Color basecolor, colorAlpha;
    private void Update()
    {
        if (!enable)
        {
            foreach (SpriteRenderer render in clouds)
            {
                render.color = Color.Lerp(basecolor, colorAlpha, 0.2f * Time.deltaTime);
            }
        }
        else
        {
            foreach (SpriteRenderer render in clouds)
            {
                render.color = Color.Lerp(colorAlpha, basecolor, 0.2f * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enable = false;
        }
    }
}
