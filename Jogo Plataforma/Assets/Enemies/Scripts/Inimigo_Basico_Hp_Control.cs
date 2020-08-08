using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_Basico_Hp_Control : MonoBehaviour
{
    public int vida, damageLimit, limitIframe;
    public bool boss;
    public bool canReceiveDamage=true;
    public float timer, timerB;
    public GeneralFunctions basicFunctions;
    public GameObject explosion;
    GameManager gameManager;
    InvencibleBlink blink;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (boss)
        {
            blink = GetComponent<InvencibleBlink>();
        }
        timer = timerB;
    }
    private void Start()
    {
        if (gameManager != null)
        {
            switch (gameManager.dificulty)
            {
                case 0:
                    if (boss)
                    {
                        vida += 0;
                        damageLimit = 4;
                    }
                    else
                    {
                        vida += 0;
                    }

                    break;
                case 1:
                    if (boss)
                    {
                        vida += 15;
                        damageLimit = 3;
                    }
                    else
                    {
                        vida += 3;
                    }
                    break;
                case 2:
                    if (boss)
                    {
                        vida += 30;
                        damageLimit = 2;
                    }
                    else
                    {
                        vida += 5;
                    }
                    break;
            }
        }
    }
    private void Update()
    {
        if (vida<=0)
        {
            Instantiate(explosion, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            //basicFunctions.AutoDestruirObj(0);
            Destroy(gameObject);
        }
        if (boss)
        {
            if (limitIframe >= damageLimit)
            {
                canReceiveDamage = false;
                limitIframe = 0;
                blink.timer = 2f;
                blink.canblink = true;
            }
            if (!blink.canblink)
            {
                /*timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = timerB;
                    canReceiveDamage = true;
                }*/
                canReceiveDamage = true;
            }
        }
    }
    public void ReceiveDamage(int dano)
    {
        if (!boss)
        {
            vida -= dano;
        }
        else if(canReceiveDamage)
        {
            if (dano > damageLimit)
            {
                vida -= damageLimit;
                limitIframe += damageLimit;
            }
            else
            {
                vida -= dano;
                limitIframe += dano;
            }
        }
    }
}
