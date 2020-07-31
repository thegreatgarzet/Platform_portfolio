using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDash : MonoBehaviour
{
    public int damage;
    public GameObject fireTrail;
    MovementController movement;
    public float timer, timerB;
    private void Start()
    {
        timer = 0.0f;
        movement = GetComponentInParent<MovementController>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            switch (movement.isRight)
            {
                case true:
                    Instantiate(fireTrail, new Vector2(transform.position.x - 1.5f, transform.position.y - 0.4f), Quaternion.identity);
                    break;
                case false:
                    Instantiate(fireTrail, new Vector2(transform.position.x + 1.5f, transform.position.y - 0.4f), Quaternion.identity);
                    break;
            }
            timer = timerB;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            collision.GetComponent<Inimigo_Basico_Hp_Control>().vida -= damage;
        }
    }
}
