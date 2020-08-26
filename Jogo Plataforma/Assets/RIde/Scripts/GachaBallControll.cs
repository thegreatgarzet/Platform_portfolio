using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaBallControll : MonoBehaviour
{
    public bool onRide;
    public Rigidbody2D rb;
    public GameObject player;
    InvencibleBlink blink;
    public float horizontalInput,speed;
    public int damage, duration;
    private void Awake()
    {
        blink = FindObjectOfType<InvencibleBlink>();
    }
    private void FixedUpdate()
    {
        if (onRide)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            //Movement();
        }
        else
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    private void Update()
    {
        if (onRide)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        if(duration <= 0)
        {
            Invoke("ReleasePlayer", 0.7f);
        }
    }
    public void ReleasePlayer()
    {
        player.GetComponent<MovementController>().rideArmor = false;
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.transform.SetParent(null);
        player.GetComponent<MovementController>().canGachaBall = false;
        blink.canblink = true;
        blink.timer = 1f;
        player.GetComponent<MovementController>().state = 7;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(damage);
            rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            duration--;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("wall"))
        {
            duration--;
        }
        //print(collision.contacts[0].point);

    }
    public void ImpulseOnCollision(float impulse)
    {
           
    }
}
