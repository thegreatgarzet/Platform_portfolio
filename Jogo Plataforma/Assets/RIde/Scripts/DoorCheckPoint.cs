using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheckPoint : MonoBehaviour
{
    bossDoor boss_door;
    public bool right, text_played=false;
    GameManager gameManager;
    private void Start()
    {
        boss_door = GetComponentInParent<bossDoor>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss_door.box.enabled = true;
            boss_door.movePlayer = false;
            boss_door.anim.SetInteger("state", 0);
            boss_door.getPos = true;
            /*if (boss_door.leadsToBoss && boss_door.bossRight == right && !text_played && !boss_door.bossBeated)
            {
                boss_door.trigger.Invoke("TriggerDialogue", 1);
                text_played = true;
                boss_door.text_played = true;
            }*/
        }
    }
}
