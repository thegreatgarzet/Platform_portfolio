using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float timer;
    public int hp;
    private float timerB;
    private int hpB;
    public ControleArmas controlearmas;
    private void Start()
    {
        hpB = hp;
        timerB = timer;
        
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer<=0 || hp<=0)
        {
            timer = timerB;
            hp = hpB;
            controlearmas.shieldUp = false;
            gameObject.SetActive(false);
        }
    }
}
