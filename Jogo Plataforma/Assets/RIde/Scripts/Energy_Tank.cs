using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Tank : MonoBehaviour
{
    public int value, maxvalue;
    bool usingTank = false;
    public Slider slider;
    ControleVida controleVida;
    private void Start()
    {
        controleVida = GameObject.Find("MainChar").GetComponent<ControleVida>();
        slider.maxValue = maxvalue;
        slider.value = maxvalue;
        value = maxvalue;
    }
    private void Update()
    {
        if (usingTank)
        {
            if (controleVida.hp < controleVida.maxHp && value > 0)
            {
                controleVida.hp++;
                value--;
                slider.value-=1;
            }
            else
            {
                usingTank = false;
            }
        }
    }
    public void UseTank()
    {
        usingTank = true;
    }
}
