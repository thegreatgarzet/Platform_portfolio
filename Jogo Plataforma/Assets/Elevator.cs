using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Vector2[] stops;
    public Vector2 box;
    public int actualStop, nextStop;
    float timer = 0.02f;
    public float speed;
    public bool playerOnTop, move, canDetectInput;
    public LayerMask layer;
    public MovementController movement;
    private void Start()
    {
        movement = FindObjectOfType<MovementController>();
        transform.position = stops[actualStop];
    }
    private void Update()
    {
        playerOnTop = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y + 1), box, 0, layer);
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y + 1), box, 0, layer)){
            

        }
        if (playerOnTop)
        {
            if (canDetectInput)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    
                    if (actualStop < stops.Count() - 1)
                    {
                        FreezePlayer(); 
                        nextStop = actualStop + 1;
                        canDetectInput = false;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    
                    if (actualStop >=0)
                    {
                        FreezePlayer();
                        nextStop = actualStop - 1;
                        canDetectInput = false;
                    }
                    
                }

            }
            else
            {
                if (move)
                {
                    MoveTo(nextStop);
                }
                else
                {
                    timer -= Time.deltaTime;
                    if(timer <= 0)
                    {
                        canDetectInput = true;
                        move = true;
                        timer = 0.02f;
                    }
                }
                
            }
                
            
            
        }
    }
    public void FreezePlayer()
    {
        movement.ispaused = true;
        movement.transform.SetParent(gameObject.transform);
        movement.rb.velocity = Vector2.right * 0;
        movement.rb.isKinematic = true;

    }
    public void MoveTo(int moveto)
    {
        transform.position = Vector2.MoveTowards(transform.position, stops[moveto], speed * Time.deltaTime);
        if(transform.position.y == stops[moveto].y)
        {
            if(stops[moveto] == stops[0])
            {
                actualStop = 0;
            }
            else
            {
                actualStop++;
            }
            move = false;
            movement.ispaused = false;
            movement.transform.SetParent(null);
            movement.rb.isKinematic = false;
        }
    }
}
