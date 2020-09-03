using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirds : MonoBehaviour
{
    public GameObject explosion, bird;
    public float playerX, speed;
    public int dir;
    private void Awake()
    {
        playerX = GameObject.Find("MainChar").GetComponent<Transform>().position.x;
        if (playerX < transform.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
    }
    private void Start()
    {
        Destroy(gameObject, 0.5f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Rigidbody2D bird1 = Instantiate(bird, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        Rigidbody2D bird2 = Instantiate(bird, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bird1.velocity = Vector2.right * speed * dir;
        bird1.gameObject.transform.localScale = new Vector2(-dir, transform.localScale.y);
        bird2.velocity = new Vector2(speed * dir,  3);
        bird2.gameObject.transform.localScale = new Vector2(-dir, transform.localScale.y);
    }
}
