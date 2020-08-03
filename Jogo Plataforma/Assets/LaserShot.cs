using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    public LayerMask groundLayer;

    Vector2 lastPoint;

    public float timer, timerB;

    public GameObject start, middle, end, groundFX;

    bool startLaser = false;

    public bool started = true;
    AudioControl audioman;
    private void Awake()
    {
        audioman = FindObjectOfType<AudioControl>();
    }

    private void Update()
    {
        if (startLaser)
        {
            if (start == null)
            {
                start = Instantiate(laserStart, transform.position, Quaternion.identity);
                start.transform.parent = this.transform;
            }
            if (middle == null)
            {
                middle = Instantiate(laserMiddle, transform.position, Quaternion.identity);
                middle.transform.parent = this.transform;
            }
            float maxDist = 20f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxDist, groundLayer);

            if (hit.collider != null)
            {
                

                if (end == null)
                {
                    end = Instantiate(laserEnd, hit.point, Quaternion.identity);
                    end.transform.parent = this.transform;
                    started = false;
                }
                else
                {
                    if (lastPoint != hit.point)
                    {
                        end.transform.position = hit.point;
                    }
                    Vector3 distReduce = new Vector2(0.0f, 0.5f);
                    float midDist = Vector2.Distance(start.transform.position, end.transform.position);
                    midDist -= 1f;
                    middle.transform.localScale = new Vector2(1, midDist);
                    //Debug.Log(midDist);
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Instantiate(groundFX, hit.point, Quaternion.identity);
                    timer = timerB;
                }
                lastPoint = hit.point;
            }
        }
        else
        {
            Destroy(start);
            Destroy(middle);
            Destroy(end);
        }
        
    }
    public void StartLaser()
    {
        startLaser = true;
    }
    public void EndLaser()
    {
        startLaser = false;
        Destroy(start);
        Destroy(middle);
        Destroy(end);
        Debug.Log("teste");
    }
    public void EnableLaserMove()
    {
        GetComponentInParent<LunarSateliteControl>().move = true;
        audioman.PlaySound("laserLunar");

    }
}
