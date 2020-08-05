using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RideArmorMove : MonoBehaviour
{
    Animator anim;
    public bool onRide, jumping, onGround, canJump, jumpPressed, isRight = true;
    public Rigidbody2D rb;
    public Transform[] groundCheck;
    public GameObject player;
    public BoxCollider2D box;
    public float groundRange, timer, timerB;
    public LayerMask groundLayer;
    public float horizontalInput, verticalInput, speed, jumpforce, jumptimer, jumptimerB;
    public GameObject projectile;
    public Transform shotspot;
    GameManager gameManager;
    AudioControl audioman;
    public bool enableMovement;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioman = FindObjectOfType<AudioControl>();
    }
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
            anim.SetTrigger("playerEnter");
            if (enableMovement)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                verticalInput = Input.GetAxisRaw("Vertical");
                Movement();
                Flip();
                if (verticalInput > 0)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        EjectPlayer();
                    }
                }

                if (Input.GetButton("Jump") && jumptimer > 0)
                {
                    if (canJump)
                    {
                        jumping = true;
                        rb.velocity = new Vector2(rb.velocity.x, 1 * jumpforce);
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
                if (Input.GetButtonDown("Atirar"))
                {
                    anim.SetInteger("state", 2);
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
            
            
    }
    public void PlayWalkSound()
    {
        audioman.PlaySound("ridewalk");
    }
    public void EnableMovement()
    {
        enableMovement = true;
    }
    public void DisableMovement()
    {
        enableMovement = false;
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
    }
    public void Movement()
    {
        if (enableMovement)
        {
            if (horizontalInput != 0)
            {
                anim.SetInteger("state", 1);
                if (horizontalInput > 0)
                {
                    rb.velocity = new Vector2(horizontalInput + 1 * speed, rb.velocity.y);
                }
                else if (horizontalInput < 0)
                {
                    rb.velocity = new Vector2(horizontalInput + -1 * speed, rb.velocity.y);
                }

            }
            else
            {
                anim.SetInteger("state", 0);
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        

        
    }
    public void Stop()
    {
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
        
    }
    public void EjectPlayer()
    {
        anim.SetTrigger("playerExit");
        box.enabled = false;
        player.transform.SetParent(null);
        player.GetComponent<MovementController>().rideArmor = false;
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * player.GetComponent<MovementController>().jumpforce * 2, ForceMode2D.Impulse);
        gameManager.PlayerVisible();
        enableMovement = false;
        onRide = false;
    }
    private void Flip()
    {
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isRight = false;
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            isRight = true;
        }
    }
    public void Shot()
    {
        GameObject bullet = Instantiate(projectile, shotspot.position, Quaternion.identity);
        if (isRight) { bullet.GetComponent<BulletScript>().dir = 1; } else { bullet.GetComponent<BulletScript>().dir = -1; }
        audioman.PlaySound("charge3");
    }
}
