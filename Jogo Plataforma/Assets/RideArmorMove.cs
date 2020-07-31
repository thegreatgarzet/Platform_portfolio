using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideArmorMove : MonoBehaviour
{
    public bool onRide, jumping, onGround, canJump, jumpPressed;
    public Rigidbody2D rb;
    public Transform[] groundCheck;
    public GameObject player;
    public BoxCollider2D box;
    public float groundRange, timer, timerB;
    public LayerMask groundLayer;
    public float horizontalInput, verticalInput, speed, jumpforce, jumptimer, jumptimerB;
    private void Start()
    {
        jumptimer = jumptimerB;
    }
    private void Update()
    {
        bool g1 = Physics2D.Raycast(groundCheck[0].position, Vector2.down, groundRange, groundLayer);
        bool g2 = Physics2D.Raycast(groundCheck[1].position, Vector2.down, groundRange, groundLayer);
        if (g1 || g2)
        {
            onGround = true;
            if (!Input.GetButton("Jump"))
            {
                jumptimer = jumptimerB;
                canJump = true;
            }
            
        }
        else
        {
            onGround = false;
            //canJump = false;
        }
        if (onRide)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if(verticalInput > 0)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    EjectPlayer();
                }
            }
            if (Input.GetButton("Jump") && jumptimer >0)
            {
                if (canJump)
                {
                    jumping = true;
                    rb.velocity = Vector2.up * jumpforce;
                    jumptimer -= Time.deltaTime;
                    if (jumptimer <= 0)
                    {
                        rb.velocity = Vector2.up * jumpforce;
                        jumping = false;
                        canJump = false;
                    }
                }
                
            }
            if (Input.GetButtonUp("Jump"))
            {
                if (jumping)
                {
                    //rb.velocity = Vector2.up * jumpforce/2;
                    canJump = false;
                    jumptimer = 0;
                }
            }
        }
        if (box.enabled == false)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                box.enabled = true;
                timer = timerB;
            }
        }
    }
    
    
    public void Movement()
    {
        rb.velocity = new Vector2(horizontalInput + 1 * speed, rb.velocity.y);
    }
    public void EjectPlayer()
    {
        box.enabled = false;
        player.transform.SetParent(null);
        player.GetComponent<MovementController>().rideArmor = false;
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * player.GetComponent<MovementController>().jumpforce * 2, ForceMode2D.Impulse);
        onRide = false;
    }
}
