using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFX : MonoBehaviour
{
    public float delay, delayB;
    public GameObject ghost;
    public bool createGhost;
    MovementController movement;
    private void Start()
    {
        delay = delayB;
        movement = GetComponent<MovementController>();
    }
    private void Update()
    {
        if (movement.dashing)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                GameObject ghostGenerated = Instantiate(ghost, transform.position, transform.rotation);
                ghostGenerated.transform.localScale = gameObject.transform.localScale;
                Sprite sprite = GetComponent<SpriteRenderer>().sprite;
                ghostGenerated.GetComponent<SpriteRenderer>().sprite = sprite;
                delay = delayB;
            }
        }
        else
        {
            delay = delayB;
        }
        
    }
}
