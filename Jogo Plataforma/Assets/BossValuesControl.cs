using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossValuesControl : MonoBehaviour
{
    public GameObject bossHPHolder;
    GameManager gameManager;
    public Slider sliderHP1, sliderHP2;
    public Inimigo_Basico_Hp_Control bossHPCheck;
    public int hpBoss, maxHp2Bar;
    public bool startBossFight=false;
    public float timer, timerB;
    private void Start()
    {
        timer = timerB;
        sliderHP1.maxValue = 30;
        sliderHP2.maxValue = 30;
        sliderHP1.value = 0;
        sliderHP2.value = 0;
        gameManager = FindObjectOfType<GameManager>();
        hpBoss = gameManager.bossList[gameManager.actualBoss].GetComponent<Inimigo_Basico_Hp_Control>().vida;
        bossHPCheck = gameManager.bossList[gameManager.actualBoss].GetComponent<Inimigo_Basico_Hp_Control>();
        if (hpBoss > 30)
        {
            maxHp2Bar = hpBoss - 30;
        }
    }
    private void Update()
    {
        if (startBossFight)
        {
            hpBoss = gameManager.bossList[gameManager.actualBoss].GetComponent<Inimigo_Basico_Hp_Control>().vida;
            bossHPCheck = gameManager.bossList[gameManager.actualBoss].GetComponent<Inimigo_Basico_Hp_Control>();
            bossHPHolder.SetActive(true);
            if (hpBoss <= 30)
            {
                sliderHP1.value += 1;
                if (sliderHP1.value == sliderHP1.maxValue)
                {
                    gameManager.ReturnPlayerControls();
                    startBossFight = false;
                }
            }
            else if (hpBoss > 30)
            {
                if(sliderHP1.value < sliderHP1.maxValue)
                {
                    sliderHP1.value += 1;
                }
                else
                {
                    if (maxHp2Bar > 0)
                    {
                        sliderHP2.value += 1;
                        maxHp2Bar--;
                    }
                    else
                    {
                        gameManager.ReturnPlayerControls();
                        startBossFight = false;
                    }
                    
                }
                
            }
        }
        else
        {
            if(bossHPCheck != null)
            {
                if (bossHPCheck.vida > 30)
                {
                    sliderHP2.value = bossHPCheck.vida - 30;
                }
                else
                {
                    sliderHP2.value = 0;
                    sliderHP1.value = bossHPCheck.vida;
                }
            }
            else
            {
                timer = timerB;
                sliderHP1.maxValue = 30;
                sliderHP2.maxValue = 30;
                sliderHP1.value = 0;
                sliderHP2.value = 0;
                bossHPHolder.SetActive(false);
            }
            
        }
    }
}
