using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideArmorHPControl : MonoBehaviour
{
    public int hp;
    public InvencibleBlink invencibleBlink;
    public RideArmorMove ridearmor;
    public Slider hpSlider;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        ridearmor = GetComponent<RideArmorMove>();
        invencibleBlink = GetComponent<InvencibleBlink>();
        //hpSlider = GameObject.Find("HP_Ride_Slider").GetComponent<Slider>();
    }
    private void Update()
    {
        if (ridearmor.onRide)
        {
            hpSlider.value = hp;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("inimigo"))
        {
            if (!invencibleBlink.canblink)
            {
                hp -= ApplyDifficultyDamage(collision.gameObject.GetComponent<Inimigo_Colider>().dano);
                invencibleBlink.timer = 2f;
                invencibleBlink.canblink = true;
                CheckHp();
            }
        } 
        if(collision.CompareTag("inimigoBullet"))
        {
            if (!invencibleBlink.canblink)
            {
                hp -= ApplyDifficultyDamage(collision.gameObject.GetComponent<EnemyBullet>().damage);
                invencibleBlink.timer = 2f;
                invencibleBlink.canblink = true;
                CheckHp();
            }
        }

    }
    public int ApplyDifficultyDamage(int dano)
    {
        if (gameManager.dificulty == 0)
        {
            dano += 0;
        }
        else if (gameManager.dificulty == 1)
        {
            dano += 1;
        }
        else if (gameManager.dificulty == 2)
        {
            dano += 2;
        }

        return dano;
    }
    public void CheckHp()
    {
        if (hp <= 0)
        {
            ridearmor.EjectPlayer();
            Destroy(gameObject);
        }
    }
}
