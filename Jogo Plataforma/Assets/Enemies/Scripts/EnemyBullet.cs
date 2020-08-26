using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int dir, damage, hp;
    public float speed;
    public Rigidbody2D rb;
    public bool isBounce, trepass;
    public bool destroyOverTime, spawnFX;
    public GameObject hitFX;
    private void FixedUpdate()
    {
        if (!isBounce)
        {
            rb.velocity = (Vector2.right * dir) * speed;
        }
        else
        {
            //rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
        
    }
    private void Update()
    {
        if (destroyOverTime) { Destroy(gameObject, 1.5f); }
        
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall") && !trepass && collision.GetComponent<Inimigo_Basico_Hp_Control>() == null)
        {
            if (spawnFX)
            {
                Instantiate(hitFX, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            if (spawnFX)
            {
                Instantiate(hitFX, transform.position, Quaternion.identity);
            }
        }
    }
}
