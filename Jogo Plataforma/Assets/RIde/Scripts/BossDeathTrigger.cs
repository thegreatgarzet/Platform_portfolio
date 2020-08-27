﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathTrigger : MonoBehaviour
{
    GameManager gameManager;
    AudioControl audioman;
    public float timer, timerB;
    public SpriteRenderer sprite;
    public GameObject explosion;
    public int count;
    public Vector2[] points;
    public bool stopsounds;
    public string[] sounds;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioman = FindObjectOfType<AudioControl>();
    }
    private void Start()
    {
        if (stopsounds)
        {
            foreach (string soundname in sounds)
            {
                audioman.SoundStop(soundname);
            }
        }
    }
    public void Update()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(count>=0)
        {
            float randX = Random.Range(-0.5f, 0.51f);
            float randY = Random.Range(-0.5f, 0.51f);
            timer = timerB;
            Vector2 position;
            position.x = transform.position.x + randX;
            position.y = transform.position.y + randY;
            Instantiate(explosion,position, Quaternion.identity);
            audioman.PlaySound("explosion");
            count--;
            if(count < 0)
            {
                audioman.SoundStop("explosion");
                Destroy(gameObject, 0.15f);
                gameManager.ExitBossRoom();
            }
        }
        
    }
}
