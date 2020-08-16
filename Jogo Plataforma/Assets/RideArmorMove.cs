using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject projectile, dashobj;
    public Transform shotspot;
    GameManager gameManager;
    AudioControl audioman;
    public bool enableMovement, dash;

    //Dash control
    public float dashtimer, dashtimerB;
    public float refreshtimer, refreshtimerB;
    public int dashunit;
    public Slider dashslider;
    //
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
                if (Input.GetButtonDown("L1"))
                {
                    dash = true;
                }
                if (Input.GetButtonUp("L1"))
                {
                    dash = false;
                }
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
           
        }
        else
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
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
        if (onGround)
        {
            audioman.PlaySound("ridewalk");
        }
        
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
            dashslider.value = dashunit;
            if (!dash)
            {
                dashobj.SetActive(false);
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
                if (dashunit < 16)
                {
                    refreshtimer -= Time.deltaTime;
                    if (refreshtimer<=0)
                    {
                        dashunit++;
                        refreshtimer = refreshtimerB;
                    }
                }
                
            }
            else
            {
                if (dashunit >= 0)
                {
                    dashobj.SetActive(true);
                    if (isRight)
                    {
                        rb.velocity = new Vector2(1 * speed * 3, rb.velocity.y);
                    }
                    else if (!isRight)
                    {
                        rb.velocity = new Vector2(-1 * speed * 3, rb.velocity.y);
                    }
                    dashtimer -= Time.deltaTime;
                    if (dashtimer <= 0)
                    {
                        dashunit--;
                        dashtimer = dashtimerB;
                    }
                }
                else
                {
                    dashobj.SetActive(false);
                    dash = false;
                }
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
        gameManager.EnablePlayerHP();
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
