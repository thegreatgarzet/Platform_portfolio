using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    public float delay, delayB;
    public GameObject ghost;
    public bool createGhost;
   
    private void Start()
    {
        delay = delayB;

    }
    private void Update()
    {
       
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                GameObject ghostGenerated = Instantiate(ghost, transform.position, transform.rotation);
                delay = delayB;
            }
        

    }
}
