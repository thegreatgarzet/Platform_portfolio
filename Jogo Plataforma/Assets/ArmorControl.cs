using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorControl : MonoBehaviour
{
    public SpriteRenderer playerRender;
    public SpriteRenderer[] peçasWhiteArmor;
    MovementController movement;
    public ControleArmas armas;
    //HyperCharge
    public Material _Outline, _OutlineRef;
    public bool _HyperCharge, canAdd=true, _HyperDash;
    public float dashSpeedBase, hyperDashSpeed;
    public float hpTimer, hpTimerB, resetTimer;
    public List<SpriteRenderer> head, body, legs, arms;
    float thicc = 0.0f;
    //Reduced cost
    public bool _ReducedCost;
    //
    //HyperStrike
    GigaAtackControl gigaAtackControl;
    GameObject hyperStrikeObj;
    public Slider hyperStrikeSlider;
    public float hsTimer, hsTimerB, hsTimerCDR;
    public bool startRefillGiga=true;
    //
    //Vida
    ControleVida controleVida;
    //
    private void Start()
    {
        movement = GetComponentInParent<MovementController>();
        controleVida = GetComponent<ControleVida>();
        gigaAtackControl = FindObjectOfType<GigaAtackControl>();
        hyperStrikeObj = GameObject.Find("Special");
        hyperStrikeSlider = hyperStrikeObj.GetComponentInChildren<Slider>();
        _Outline = new Material(_OutlineRef);
        hpTimer = hpTimerB;
        playerRender.material = _Outline;
        dashSpeedBase = movement.dashSpeed;
    }
    
    private void Update()
    {
        CheckSetBuff();
        if (startRefillGiga && hyperStrikeSlider.value < hyperStrikeSlider.maxValue)
        {
            if (hsTimer > 0)
            {
                hsTimer -= Time.deltaTime;
            }
            else
            {
                hyperStrikeSlider.value++;
                hsTimer = hsTimerCDR;
            }
        }
        else if(!startRefillGiga)
        {
            if (hsTimer > 0)
            {
                hsTimer -= Time.deltaTime;
            }
            else
            {
                hyperStrikeSlider.value--;
                hsTimer = hsTimerB;
            }
            if (hyperStrikeSlider.value==0)
            {
                startRefillGiga = true;
            }
        }
        if (_HyperDash)
        {
            movement.dashSpeed = hyperDashSpeed;
        }
        else
        {
            movement.dashSpeed = dashSpeedBase;
            movement._HypDash = false;
        }
        //HEAD
        if (head[0].enabled)
        {
            _ReducedCost = true;//CUSTO REDUZIDO
        }
        else if (head[1].enabled)//HYPERCHARGE
        {
            _ReducedCost = false;
            _HyperCharge = true;
            if (controleVida.hp <= controleVida.maxHp/2)
            {
                if (_HyperCharge)
                {
                    HyperCharge();
                }
            }
            
        }else//PADRÃO
        {
            _ReducedCost = false;
            _HyperCharge = false;
        }
        //BODY
        if (body[0].enabled)//HYPERATACK
        {
            controleVida._DamageReduction = 1;
            movement._HiperAtackGet = true;
            hyperStrikeObj.SetActive(true);
            movement.canReceiveKnockback = true;
        }
        else if (body[1].enabled)//IGNORE KNOCKBACK
        {
            controleVida._DamageReduction = 2;
            movement.canReceiveKnockback = false;
            movement._HiperAtackGet = false;
            hyperStrikeObj.SetActive(false);
            //Ignora colisão
        }
        else//PADRÃO
        {
            controleVida._DamageReduction = 0;
            hyperStrikeObj.SetActive(false);
            movement._HiperAtackGet = false;
            movement.canReceiveKnockback = true;
        }
        //ARMS
        if (arms[0].enabled)
        {
            armas.canCharge = true;
            armas.plasmaBuster = true;
            armas._SwordSlash = false;
        }
        else if(arms[1].enabled)
        {
            armas.canCharge = true;
            armas._SwordSlash = true;
            armas.plasmaBuster = false;
        }
        else
        {
            armas.plasmaBuster = false;
            armas.canCharge = false;
            armas._SwordSlash = false;
        }
        //LEGS
        if (legs[0].enabled)//AIRDASH
        {
            movement._DashGet = true;
            movement._DoubleJump = false;
            _HyperDash = false;
        }
        else if (legs[1].enabled)//DOUBLEJUMP
        {
            movement._DashGet = false;
            movement._DoubleJump = true;
            _HyperDash = true;
            //double Jump
        }
        else//PADRÃO
        {
            movement._DashGet = false;
            movement._DoubleJump = false;
            _HyperDash = false;
        }
    }
    public void AtivaPeça(int armorpiece, int whicharmor)
    {
        if(whicharmor == 0)
        {
            DeactivateArmors(head);
            HeadActivate(armorpiece);
        }
        if (whicharmor == 1)
        {
            DeactivateArmors(body);
            BodyActivate(armorpiece);
        }
        if (whicharmor == 2)
        {
            DeactivateArmors(arms);
            ArmActivate(armorpiece);
        }
        if (whicharmor == 3)
        {
            DeactivateArmors(legs);
            LegsActivate(armorpiece);
        }

    }
    public void ArmActivate(int i)
    {
        DeactivateArmors(arms);
        if (i !=2)
        {
            arms[i].enabled = true;
        }
    }
    public void BodyActivate(int i)
    {
        DeactivateArmors(body);
        if (i != 2)
        {
            body[i].enabled = true;
        }
    }
    public void LegsActivate(int i)
    {
        DeactivateArmors(legs);
        if (i != 2)
        {
            legs[i].enabled = true;
        }
    }
    public void HeadActivate(int i)
    {
        DeactivateArmors(head);
        if (i != 2)
        {
            head[i].enabled = true;
        }
    }
    public void DeactivateArmors(List<SpriteRenderer> lista)
    {
        foreach (SpriteRenderer item in lista)
        {
            item.enabled = false;
        }
    }
    public void HyperCharge()
    {
        if (hpTimer>0)
        {
            hpTimer -= Time.deltaTime;
            armas._HypeCharge = true;
            if (canAdd)
            {
                thicc += 0.0095f * Time.deltaTime;
                if (thicc >= 0.01f)
                {
                    canAdd = false;
                }
            }
            else
            {
                thicc -= 0.0095f * Time.deltaTime;
                if (thicc <= 0.0f)
                {
                    canAdd = true;
                }
            }
            _Outline.SetFloat("_Thicc", thicc);
        }
        else
        {
            _Outline.SetFloat("_Thicc", 0.0f);
            armas._HypeCharge = false;
            if (resetTimer > 0)
            {
                resetTimer -= Time.deltaTime;
            }
            else
            {
                hpTimer = hpTimerB;
            }
        }
        
    }
    public void CheckSetBuff()
    {
        if (arms[0].enabled && legs[0].enabled && body[0].enabled && head[0].enabled)
        {
            //Ativa buff Force armor
            gigaAtackControl.dano = gigaAtackControl.danobase * 2;
            movement._SwordProjectile = false;
        }
        else if (arms[1].enabled && legs[1].enabled && body[1].enabled && head[1].enabled)
        {
            //Ativa buff Force armor
            gigaAtackControl.dano = gigaAtackControl.danobase;
            movement._SwordProjectile = true;
        }
        else
        {
            gigaAtackControl.dano = gigaAtackControl.danobase;
            movement._SwordProjectile = false;
        }
    }
}
