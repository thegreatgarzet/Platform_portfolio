using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControleVida : MonoBehaviour
{
    public Slider HPFill;
    public float maxHp, hp;
    public Animator blackBackground, border, hpBarFillSize;
    public bool canAdd=true;
    public float timer, timerB;
    GameManager gameManager;
    public int _DamageReduction;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blackBackground = GameObject.Find("BlackBackground").GetComponent<Animator>();
        border = GameObject.Find("Border").GetComponent<Animator>();
        HPFill = GameObject.Find("HPBarravida").GetComponent<Slider>();
        hpBarFillSize = GameObject.Find("HpHolder").GetComponent<Animator>();
        hp = maxHp;
        HPFill.maxValue = maxHp;
        HPFill.value = hp;
        timer = timerB;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        UpdateHP();
        if (hp <= 0)
        {
            gameManager.playerDead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TookDamage(10);
        }
        if (!canAdd)
        {
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                timer = timerB;
                canAdd = true;
            }
        }
    }
    public void ReceiveDamage(int dano)
    {
        if (dano > _DamageReduction)
        {
            hp -= (dano - _DamageReduction);
        }
        else
        {
            hp -= 1;
        }
    }
    void TookDamage(int danoRecebido)
    {
        if (danoRecebido > _DamageReduction)
        {
            HPFill.value -= (danoRecebido -_DamageReduction);
        }
        else
        {
            HPFill.value -= 1;
        }
        
    }
    public void UpdateHP()
    {

        if (hp > HPFill.maxValue)
        {
            HPFill.value = HPFill.maxValue;
        }
        else
        {
            HPFill.value = hp;
        }
        
    }
    public void DamageFX()
    {
        blackBackground.SetTrigger("hit");
    }
    public void AddHP()
    {
        if (canAdd)
        {
            maxHp += 5;
            HPFill.maxValue = maxHp;
            hp = HPFill.maxValue;
            int actualhp = border.GetInteger("hpup");
            border.SetInteger("hpup", actualhp + 1);
            hpBarFillSize.SetInteger("hpsize", actualhp + 1);
            Debug.Log(actualhp);
            canAdd = false;
        }
     
    }
}
