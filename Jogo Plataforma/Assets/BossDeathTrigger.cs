using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathTrigger : MonoBehaviour
{
    GameManager gameManager;
    public float timer, timerB;
    public SpriteRenderer sprite;
    public GameObject explosion;
    public int count;
    public Vector2[] points;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            count--;
            if(count < 0)
            {
                Destroy(gameObject, 0.15f);
                gameManager.ExitBossRoom();
            }
        }
        
    }
}

