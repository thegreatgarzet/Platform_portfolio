using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.WSA;

public class MovingPlatform1 : MonoBehaviour
{
    public Vector2[] positions;
    public int startpoint, maxPoints, actualpoint, moveto;
    public bool cancheck;
    public float timer = 0.01f, speed;
    private void Start()
    {
        transform.position = positions[startpoint];
        maxPoints = positions.Count() - 1;
        actualpoint = startpoint;
        moveto = startpoint + 1;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[moveto], speed * Time.deltaTime);
        if (transform.position.x == positions[moveto].x && transform.position.y == positions[moveto].y)
        {
            if (cancheck)
            {
                UpdatePos();
                cancheck = false;
            }
            else
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    cancheck = true;
                    timer = 0.01f;
                }
            }
        }
    }
    public void UpdatePos()
    {
        if(moveto < maxPoints)
        {
            moveto++;
        }
        else
        {
            moveto = 0;
        }
    }
}
