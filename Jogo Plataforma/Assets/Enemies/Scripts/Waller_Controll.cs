using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Waller_Controll : MonoBehaviour
{
    public List<Animator> anims;
    public GameObject[] eyes;
    Animator anim;
    public int counter;
    public float timer, timerB;
    public bool start;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = timerB;
        foreach (GameObject eye in eyes)
        {
            anims.Add(eye.GetComponent<Animator>());
        }
    }
    private void Update()
    {
        if (start)
        {
            if (eyes[0] == null && eyes[1] == null && eyes[2] == null)
            {
                anim.SetTrigger("dead");
                //Destroy(gameObject, 0.1f);
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (counter >= 0)
                    {
                        if (anims[counter] == null)
                        {
                            counter--;
                            timer = 0.0f;
                        }
                        else
                        {
                            anims[counter].SetTrigger("shot");
                            counter--;
                            timer = timerB;
                        }

                    }
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            MultiShot(0, 2);
                        }
                        else
                        {
                            MultiShot(2, 1);
                        }
                        counter = 2;

                    }

                }
            }
            
        }
        
    }
    public void StartBoss()
    {
        start = true;
    }
    public void MultiShot(int eye1, int eye2)
    {
        if (anims[eye1] != null)
        {
            anims[eye1].SetTrigger("shot");
            timer = timerB;
            counter = 2;
        }
        if (anims[eye2] != null)
        {
            anims[eye2].SetTrigger("shot");
            timer = timerB;
            counter = 2;
        }
        if (anims[eye2] == null && anims[eye1] == null)
        {
            counter = 2;
            timer = 0.0f;
        }
    }
  
}
