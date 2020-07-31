using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRefillControl : MonoBehaviour
{
    public Slider sliderValue;
    public int  cost;
    public float timer, timerB, slidermax;
    public bool canShot = true, refilled, canReduce;
    ArmorControl armorControl;
    private void Start()
    {
        timer = timerB;
        slidermax = sliderValue.maxValue;
        armorControl = FindObjectOfType<ArmorControl>();
    }
    public void Update()
    {
        if (armorControl._ReducedCost && sliderValue.value >= cost-1)
        {
            canReduce = true;
        }
        else if(!armorControl._ReducedCost && sliderValue.value >= cost)
        {
            canReduce = true;
        }
        else
        {
            canReduce = false;
        }
        if (sliderValue.value <=0)
        {
            refilled = false;
        }
            if (!refilled)
            {
                canShot = false;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    sliderValue.value += cost;
                    //sliderValue.value = slidermax;
                    if (sliderValue.value >= sliderValue.maxValue)
                    {
                        canShot = true;
                        refilled = true;
                    }
                    timer = timerB;
                }
            }
        
    }
    public void ReduceValue()
    {
        if (armorControl._ReducedCost)
        {
            sliderValue.value -= (cost-1);
        }
        else
        {
            sliderValue.value -= cost;
        }
        
    }
}
