using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ActionCode.ColorPalettes;

public class MovementController : MonoBehaviour
{
    Vector2 dir;
    public bool isRight, isgrounded, canjump, isjumping,isonwall, jumped=false, canDouble=false, canDetectGround=true, canReceiveDamage = true;
    public bool groundDetected;
    public bool onWater = false, isfalling;
    public int jumps;
    public float speed, jumpforce,gravity, originalgravity,waterGravity, baseSpeed, recoil;
    public float moveInput, rayCastDistance;
    public Transform groundcheck, groundcheck2;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    //WallJump
    public bool wallJumping;
    public float timerWall, timerWallB;
    //
    //Timers
    public float jumptimer, jumptimerB, jumpdelay;
    //
    //Controles Gerais
    public bool canmove, ispaused, pressedJump;
    //
    public BoxCollider2D box, box2;
    //Animações
    public ControleAnimaçao animationC;
    public int state;
    //
    public bool rideArmor=false;
    //VIDA
    public ControleVida vida;    
    public RecoilControl recoilControl;
    public bool canReceiveKnockback;
    InvencibleBlink blink;
    //
    //OBJETOS
    public GameObject shieldObj;
    //
    //GameManager
    public bool canCallSafeRoom = false;
    public Animator doorAnim;//Animator da porta para transição para safeRoom 
    GameManager gameManager;
    //Dash
    public bool dashing = false, canDash = true, airdash = true, _DashGet, _HypDash, _DoubleJump;
    public bool doubleJumped = true, _OverHeated = false, soundCrash = false, cansoundCrash;
    public bool gachaBall = false, canGachaBall;
    public bool fireUppercut = false, canFireUppercut;
    public float dashSpeed, dTimer, dTimerB;
    public GameObject hypDashObj, fireDashObj;
    
    //
    ArmorControl armorControl;
    //Controles 
    public bool fixedonground;
    public bool canMoveup=true, hiperAtacking, canHiperAtack=true, _HiperAtackGet=false, _SwordSlash, _SwordProjectile;
    public GameObject _SwordProjectileObj;
    public bool returnMovement;
    public float timerSlash, timerSlashB;
    Animator giga;
    //
    public Vector2 wallJumpDir;
    public float wallJumpForce, wallJumpTimer, wallJumpTimerB;

    public ControleArmas controleArmas;

    //SLOPES
    Vector2 colliderSize, slopeNormalPerp;
    public float slopeCheckDistance, downValue;
    public Transform slopeCheckPoint, slopeFrontCheck;
    private float slopeDownAngle, slopeDownAngleOld;
    public bool onSlope, slopeInFront;
    public LayerMask slopeLayer;
    //
    public bool isOnPlatform = false;
    float moveDir;
    InputControl control;

    AudioControl audioman;

    public int frames = 1;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioman = FindObjectOfType<AudioControl>();
        giga = GameObject.Find("GigaAtack").GetComponent<Animator>();
        recoilControl = GetComponent<RecoilControl>();
        vida = GetComponent<ControleVida>();
        armorControl = GetComponent<ArmorControl>();
        baseSpeed = speed;
        jumptimer = jumptimerB;
        gravity = rb.gravityScale;
        originalgravity = gravity;
        colliderSize = box.size;
        timerSlash = timerSlashB;
        fireDashObj.SetActive(false);
        blink = GetComponent<InvencibleBlink>();
    }
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        
        moveInput = Input.GetAxisRaw("Horizontal");
        //moveInput = moveDir;
        if (isRight)
        {
            slopeInFront = Physics2D.Raycast(slopeFrontCheck.position, Vector2.right, slopeCheckDistance, slopeLayer);
        }
        else
        {
            slopeInFront = Physics2D.Raycast(slopeFrontCheck.position, Vector2.left, slopeCheckDistance, slopeLayer);
        }
        bool grounded1 = Physics2D.Raycast(groundcheck.position, Vector2.down, rayCastDistance, groundLayer);
        bool grounded2 = Physics2D.Raycast(groundcheck2.position, Vector2.down, rayCastDistance, groundLayer);
        bool grounded3 = Physics2D.Raycast(new Vector2(transform.position.x, groundcheck2.position.y), Vector2.down, rayCastDistance, groundLayer);

        if (!grounded1 && !grounded2 && !grounded3)
            {
                isgrounded = false;
                groundDetected = false;
                rb.gravityScale = gravity;
                fixedonground = false;
            }
            else 
            {
            //isgrounded = true;
                groundDetected = true;
                doubleJumped = true;
                isfalling = false;
                rb.gravityScale = 0;

            }
       
        if (Input.GetAxisRaw("Vertical") > 0 && canCallSafeRoom && isgrounded && !gameManager.onTyping && !gameManager.onMenu)
        {
            doorAnim.SetBool("open", true);
            ispaused = true;
        }
        if (!ispaused)
        {
            if (!canReceiveDamage)
            {
                recoilControl.gotHit = true;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (canmove && !rideArmor)
            {
                Flip();
                if (Input.GetButtonDown("Jump") && jumps>0 && !jumped)
                {
                    if (_DoubleJump && !isonwall)
                    {
                       
                        isjumping = true;
                    }
                    else if(pressedJump /*&& !isonwall*/)
                    {
                        
                        isjumping = true;
                    }
                    
                }
                if (Input.GetButtonUp("Jump"))
                {
                    if (pressedJump && !isgrounded)
                    {
                        if (isjumping)
                        {
                            rb.velocity = Vector2.up * jumpforce / 2;
                        }
                        pressedJump = false;
                    }
                    isjumping = false;
                    if (!_DoubleJump)
                    {
                        jumps = 0;
                        jumped = false;
                        jumptimer = jumptimerB;
                    }
                    else
                    {
                        if (doubleJumped)
                        {
                            //jumped = false;
                            jumptimer = jumptimerB;
                            doubleJumped = false;
                        }
                        else
                        {
                            jumps = 0;
                            jumped = false;
                            jumptimer = jumptimerB;
                        }
                        jumptimer = jumptimerB;
                    }
                    
                }
                if (isjumping)
                {
                    jumptimer -= Time.deltaTime;
                    rb.velocity = Vector2.up * jumpforce;
                    isfalling = false;
                    if (jumptimer <= 0)
                    {
                        //rb.velocity = Vector2.up * 1f;
                        rb.velocity = Vector2.up * jumpforce/2;
                        if (!_DoubleJump)
                        {
                            isjumping = false;
                            jumps = 0;
                            jumped = true;
                        }
                        else
                        {
                            jumptimer = jumptimerB;
                            
                            jumps = 1;
                             jumped = false;
                            isjumping = false;
                        }
                       
                    }
                }
                else if (!isjumping && !isgrounded)
                {
                    isfalling = true;
                    if (!onWater)
                    {
                        rb.gravityScale = gravity * 2.3f;
                    }
                    else
                    {
                        rb.gravityScale = waterGravity/1.5f;
                    }
                }
            }
            animationC.TocaAnimaçao(state);
            if (Input.GetButtonUp("Jump") && isgrounded)
            {
                pressedJump = true;
            }
            if (state !=8)
            {
                hypDashObj.SetActive(false);
                fireDashObj.SetActive(false);
            }
            if(state != 15)
            {
                soundCrash = false;
            }
            if(state != 14)
            {
                fireUppercut = false;
            }
            switch (state)
            {
                case 0:
                    rb.velocity = Vector2.up * 0;
                    box.enabled = true;
                    box2.enabled = false;
                    canmove = true;
                    speed = baseSpeed;
                    jumps = 1;
                    jumptimer = jumptimerB;
                    if (!dashing)
                    {
                        canDash = true;
                        dTimer = dTimerB;
                    }
                    if (!isgrounded /* isjumping*/)
                    {
                        state = 3;
                        return;
                    }
                    if (Input.GetButtonDown("R1") && canHiperAtack && _HiperAtackGet && armorControl.hyperStrikeSlider.value == 16)
                    {
                        state = 9;
                        return;
                    }
                    if (Input.GetButtonDown("Jump"))
                    {
                        state = 3;
                        //return;
                    }
                    if (isgrounded && Input.GetAxisRaw("Vertical") < 0 && canReceiveDamage)
                    {
                        if (!Input.GetButtonDown("Jump"))
                        {
                            state = 4;
                            return;
                        }
                        //animação agachado;
                    }
                    if (Input.GetButtonDown("Atirar"))
                    {
                        state = 1;
                        return;
                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (!_SwordSlash)
                        {
                            if (canGachaBall)
                            {
                                gachaBall = true;
                                state = 16;
                                return;
                            }else
                            if (cansoundCrash)
                            {
                                soundCrash = true;
                                state = 15;
                                return;
                            }else if (canFireUppercut)
                            {
                                fireUppercut = true;
                                state = 14;
                                return;
                            }
                            else if (/*controleArmas.hipershot*/controleArmas.timer >= 1)
                            {
                                state = 1;
                                return;
                            }
                        }
                        else if(controleArmas.idArma==0)
                        {
                            state = 11;
                            return;
                        }

                    }      
                    if (moveInput != 0 && !isonwall)
                    {
                        state = 2;
                        return;
                    }
                    if (Input.GetButtonDown("L1") && canDash && dTimer > 0 )
                    {
                        Debug.Log("L1");
                        speed *= dashSpeed;
                        audioman.PlaySound("dashsound");
                        state = 8;
                        return;
                    }
                    if (Input.GetButtonUp("L1") && !canDash)
                    {
                        canDash = true;
                        dTimer = dTimerB;
                        return;
                    }
                    break;
                case 1:
                    box.enabled = true;
                    box2.enabled = false;
                    jumps = 1;
                    jumptimer = jumptimerB;
                    if (Input.GetButtonDown("Jump"))
                    {
                        state = 3;
                        return;
                    }
                    if (isgrounded && Input.GetAxisRaw("Vertical") < 0 && canReceiveDamage)
                    {
                        state = 4;
                        return;
                        //animação agachado;
                    }
                    if (Input.GetButtonDown("R1") && canHiperAtack && _HiperAtackGet && armorControl.hyperStrikeSlider.value == 16)
                    {
                        state = 9;
                        return;
                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            state = 15;
                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        return;
                    }
                    if (Input.GetButton("Horizontal"))
                    {
                        if (moveInput !=0)
                        {
                            state = 2;
                            return;
                        }
                    }
                    if (Input.GetButtonDown("L1") && dTimer > 0)
                    {
                        audioman.PlaySound("dashsound");
                        state = 8;
                        return;
                    }
                    state = 0;
                    break;
                case 2:
                    box.enabled = true;
                    box2.enabled = false;
                    canmove = true;
                    speed = baseSpeed;
                    jumps = 1;
                    if (!isgrounded)
                    {
                        if (isOnPlatform)
                        {
                            ExitPlatform();
                        }
                        state = 7;
                        return;
                    }
                    if (moveInput == 0)
                    {
                        state = 0;
                        return;
                    }
                    if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal") || isonwall)
                    {
                        state = 0;
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.S) && canHiperAtack && _HiperAtackGet && armorControl.hyperStrikeSlider.value == 16)
                    {
                        state = 9;
                        return;
                    }
                    if (Input.GetButtonDown("Jump"))
                    {
                        
                        state = 3;

                        return;
                    }
                    if (Input.GetAxisRaw("Vertical")<0)
                    {
                        state = 4;
                        return;
                    }
                    if (Input.GetButtonDown("L1") && dTimer >0)
                    {
                        audioman.PlaySound("dashsound");
                        state = 8;
                        return;
                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            state = 15;
                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        else
                        if (_SwordSlash && controleArmas.idArma == 0)
                        {
                            state = 11;
                            return;
                        }
                    }
                    break;
                case 3:
                    box.enabled = true;
                    box2.enabled = false;
                    pressedJump = true;
                    if (isOnPlatform)
                    {
                        transform.SetParent(null);
                        rb.isKinematic = false;
                        isOnPlatform = false;
                    }
                    if (isonwall)
                    {
                        //rb.velocity = Vector2.right * 0;
                    }
                    if (Input.GetButtonUp("L1"))
                    {
                        dashing = false;
                        dTimer = dTimerB;
                        if (airdash)
                        {
                            canDash = true;
                            airdash = false;
                        } 
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.S) && canHiperAtack && _HiperAtackGet && armorControl.hyperStrikeSlider.value == armorControl.hyperStrikeSlider.maxValue)
                    {
                        state = 9;
                        return;
                    }
                     if (Input.GetButtonUp("Atirar") )
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            state = 15;
                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        else
                        if (_SwordSlash && controleArmas.idArma == 0)
                        {
                            dashing = false;
                            dTimer = dTimerB;
                            state = 13;
                            return;
                        }
                    }
                    if (!isjumping)
                    {
                        state = 7;
                        
                        return;
                    }
                   
                    if (isonwall)
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            state = 10;
                            return;
                        }
                    }
                    if (Input.GetButtonDown("L1") && !dashing && dTimer > 0 && airdash && canDash && _DashGet)
                    {
                        dTimer = dTimerB;
                        audioman.PlaySound("dashsound");
                        state = 8;
                        return;
                    }
                    break;
                case 4:
                    box2.enabled = true;
                    box.enabled = false;
                    StopPlayerMovement();
                    if (Input.GetAxisRaw("Vertical") == 0)
                    {
                        canmove = true;
                        canjump = true;
                        state = 0;
                        return;
                    }
                    if (Input.GetButton("Atirar"))
                    {
                        state = 5;
                        return;
                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            canmove = true;
                            state = 15;
                            
                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        else
                        if (!_SwordSlash)
                        {
                            if (controleArmas.hipershot)
                            {
                                state = 5;
                                return;
                            }
                        }
                        else
                        {
                            state = 12;
                            return;
                        }
                    }
                    break;
                case 5:
                    box2.enabled = true;
                    box.enabled = false;
                    StopPlayerMovement();
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            canmove = true;
                            state = 15;
                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        else
                        if (!_SwordSlash)
                        {
                            if (controleArmas.hipershot)
                            {
                                state = 5;
                                return;
                            }
                            else 
                            {
                                state = 4;
                                return;
                            }
                        }
                        else if (controleArmas.idArma == 0)
                        {
                            state = 12;
                            return;
                        }
                        
                    }
                    if (Input.GetAxisRaw("Vertical") == 0)
                    {
                        if (Input.GetButton("Horizontal"))
                        {
                            canmove = true;
                            state = 2;
                            return;
                        }
                        else
                        {
                            state = 1;
                            canmove = true;
                            return;
                        }   
                    }
                    
                    break;
                case 7:
                    /*if (isOnPlatform)
                    {
                        transform.SetParent(null);
                        rb.isKinematic = false;
                        isOnPlatform = false;
                    }*/
                    if (isonwall)
                    {
                     //   rb.velocity = Vector2.right * 0;
                    }
                    box.enabled = true;
                    box2.enabled = false;
                    if (!_DoubleJump)
                    {
                        jumps = 0;
                    }
                    speed = baseSpeed;
                    timerSlash = timerSlashB;
                    if (Input.GetButtonUp("L1"))
                    {
                        dashing = false;
                        canDash = true;
                        airdash = true;
                        dTimer = dTimerB;
                        return;
                    }
                    if (isjumping)
                    {
                        state = 3;
                        return;
                    }
                    if (isonwall)
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            //pos.position = new Vector2(transform.position.x * speed, transform.position.x) * speed;
                            state = 10;
                            print("teste");
                            return;
                        }
                    }
                    if (Input.GetButtonDown("L1") && !dashing && canDash && dTimer > 0 && _DashGet)
                    {
                        audioman.PlaySound("dashsound");
                        state = 8;
                        return;
                    }
                    if (isgrounded)
                    {
                        dashing = false;

                        if (!Input.GetButton("Jump") || !Input.GetButtonDown("Jump"))
                        {
                            pressedJump = true;
                        }
                        if (Input.GetAxisRaw("Vertical") <0)
                        {
                            if (Input.GetButtonDown("Atirar"))
                            {
                                audioman.PlaySound("landing");
                                state = 5;
                                return;
                            }
                            else
                            {
                                audioman.PlaySound("landing");
                                state = 4;
                                return;
                            }
                        }
                        else
                        if (Input.GetButtonDown("Horizontal"))
                        {
                            audioman.PlaySound("landing");
                            state = 2;
                            pressedJump = true;
                            return;
                        }
                        else
                        {
                            audioman.PlaySound("landing");
                            jumps = 1;
                            state = 0;
                            return;
                        }

                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            canmove = true;
                            state = 15;

                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        else
                        if (_SwordSlash && controleArmas.idArma == 0)
                        {
                            dashing = false;
                            dTimer = dTimerB;
                            state = 13;
                            return;
                        }
                    }
                    break;
                case 8://Dash
                    if (isOnPlatform)
                    {
                        ExitPlatform();
                    }
                    if (_OverHeated)
                    {
                        fireDashObj.SetActive(true);
                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            soundCrash = true;
                            canmove = true;
                            state = 15;

                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        if (isgrounded)
                        {
                            
                            if (_SwordSlash && controleArmas.idArma == 0)
                            {
                                dashing = false;
                                dTimer = dTimerB;
                                state = 11;
                                return;
                            }
                        }
                        else
                        {
                            if (_SwordSlash && controleArmas.idArma == 0)
                            {
                                dashing = false;
                                dTimer = dTimerB;
                                state = 13;
                                return;
                            }
                        }
                        
                    }
                        if (canDash && dTimer >0)
                        {
                            if (Input.GetButtonUp("L1") && canDash)
                            {
                                canDash = false;
                            
                            }
                            if (!isonwall)
                            {
                           
                                dTimer -= Time.deltaTime;
                                rb.velocity = Vector2.up * 0;
                                rb.gravityScale = 0;
                                dashing = true;
                                if (_HypDash)
                                {
                                        hypDashObj.SetActive(true);
                                }
                                if (Input.GetButtonUp("L1"))
                                    {
                                        rb.gravityScale = gravity;
                                        dashing = false;
                                        if (isgrounded)
                                        {
                                            if (Input.GetButtonDown("Horizontal"))
                                            {
                                                state = 2;
                                            }
                                            else
                                            {
                                                state = 0;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            dTimer = dTimerB;
                                            state = 7;
                                            return;
                                        }
                                }
                            }
                            else
                            {
                                rb.gravityScale = gravity;
                                dashing = false;
                                canDash = false;
                                state = 7;
                                return;
                            }
                        }
                        else if(dTimer <=0)
                        {
                            dashing = false;
                            dTimer = dTimerB;
                            if (isgrounded)
                            {
                                if (Input.GetButtonDown("Horizontal"))
                                {
                                    state = 2;
                                }
                                else
                                {
                                    state = 0;
                                    return;
                                }
                            }
                            else
                            {
                                state = 7;
                            }
                        }
                        if (isgrounded)
                        {
                            if (Input.GetButtonDown("Jump"))
                            {
                                speed = dashSpeed;
                                rb.gravityScale = gravity;
                                state = 3;
                                return;
                            }
                        }
                    break;
                case 9:
                    rb.gravityScale = 0;
                    hiperAtacking = true;
                    //isjumping = true;
                    ispaused = true;
                    break;
                case 10:

                    if (timerWall>0)
                    {
                        wallJumping = true;
                        timerWall -= Time.deltaTime;
                    }
                    else
                    {
                        timerWall = timerWallB;
                        wallJumping = false;
                        state = 7;
                        return;
                    }
                    if (Input.GetButtonUp("Atirar"))
                    {
                        if (cansoundCrash)
                        {
                            timerWall = timerWallB;
                            wallJumping = false;
                            soundCrash = true;
                            canmove = true;
                            state = 15;

                            return;
                        }
                        else if (canFireUppercut)
                        {
                            fireUppercut = true;
                            state = 14;
                            return;
                        }
                        else
                        if (_SwordSlash)
                        {
                            state = 13;
                            return;
                        }
                    }
                    break;
                case 11://SwordSlash
                    StopPlayerMovement();
                    _SwordSlash = false;
                    if (isgrounded && returnMovement)
                    {
                        if (moveInput != 0)
                        {
                            state = 2;
                        }
                        else
                        {
                            state = 0;
                        }
                        returnMovement = false;
                        return;   
                    }
                        break;
                case 12://CrouchSlash
                    _SwordSlash = false;
                    if (isgrounded)
                    {
                        state = 0;
                    }
                    break;
                case 13://JumpSlash
                    _SwordSlash = false;
                    if (timerSlash > 0)
                    {
                        rb.velocity = Vector2.up * 0;
                        timerSlash -= Time.deltaTime;

                    }
                    else
                    {
                        state = 7;
                    }
                    break;
                case 14: //FIRE UPPERCUT
                    rb.gravityScale = 0;
                    dashing = false;
                    if (!fireUppercut)
                    {
                        if (isgrounded)
                        {
                            state = 0;
                            return;
                        }
                        else
                        {
                            state = 7;
                            return;
                        }
                    }
                    break;
                case 15://SoundCrash
                    rb.gravityScale = 0;
                    if (!soundCrash)
                    {
                         if (isOnPlatform)
                        {
                            ExitPlatform();
                        }
                        if (isgrounded)
                        {
                            state = 0;
                            return;
                        }
                        else
                        {
                            state = 7;
                            return;
                        }
                    }
                    break;
                case 16://GachaBall

                    break;
            }
        }
        else
        {
            if (hiperAtacking)
            {
                if (canMoveup)
                {
                    rb.velocity = Vector2.up * 5;
                }
                else
                {
                    rb.velocity = Vector2.up * 1;
                }
            }else if (isgrounded)
            {
                state = 0;
                animationC.TocaAnimaçao(state);
            }
            else
            {
                state = 7;
                animationC.TocaAnimaçao(state);
            }

        }
        /*
         0 - idle
1 - idleshot
2 - walk
3 - jump
4 - crouch
5 - crouchShot
7 - fall
8 - dash
9 - gigaatack1_burst
10 - WallJump
         */
    }
    private void FixedUpdate()
    {
        if (!ispaused)
        {
            if (canmove && !rideArmor)
            {
                if (isfalling && rb.velocity.y<= -10)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -12f);
                }
                if (soundCrash)
                {
                    
                    if (isRight)
                    {
                        rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y * 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2((-1 * (baseSpeed * dashSpeed)), rb.velocity.y * 0);
                    }

                }else if (fireUppercut)
                {
                    if (isRight)
                    {
                        rb.velocity = new Vector2(1, dashSpeed*5);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-1,  dashSpeed*5);
                    }
                }
                else
                if (!wallJumping)
                {
                    if (moveInput != 0)
                    {
                        if (!onSlope)
                        {
                            if (dashing)
                            {
                                if (isRight)
                                {
                                    rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                }
                                else
                                {
                                    rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                }
                            }
                            else
                            {
                                rb.velocity = new Vector2((moveInput * 1) * speed, rb.velocity.y);
                            }
                        }
                        else if (onSlope && slopeInFront && !isjumping)
                        {
                            if (dashing)
                            {
                                if (isRight)
                                {
                                    rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                }
                                else
                                {
                                    rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                }
                            }
                            else
                            {
                                rb.velocity = new Vector2((moveInput * 1) * speed + 1, 0.0f);
                            }
                        }
                        else if (onSlope && !isjumping)
                        {
                            if (dashing)
                            {
                                if (!slopeInFront)
                                {

                                    if (isRight)
                                    {
                                        rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y - 20f);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2((-1 * (baseSpeed * dashSpeed))/2, rb.velocity.y - 20f);
                                        
                                    }
                                }
                                else
                                {
                                    if (isRight)
                                    {
                                        rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                    }
                                }

                            }
                            else
                            {
                                rb.velocity = new Vector2((moveInput * 1) * speed, rb.velocity.y - 10f);
                            }
                            
                        }
                    }
                    else
                    {
                        if (onSlope && !isjumping)
                        {
                            if (dashing)
                            {
                                if (!slopeInFront)
                                {
                                    if (isRight)
                                    {
                                        rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y - 10f);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y - 10f);
                                    }
                                }
                                else
                                {
                                    if (isRight)
                                    {
                                        rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                    }
                                }
                                
                            }
                            else
                            {
                                rb.velocity = new Vector2(0.0f, 0.0f);
                            }

                        }
                        else
                        {
                            if (dashing)
                            {
                                if (!slopeInFront)
                                {
                                    if (isRight)
                                    {
                                        if (!isjumping && !isfalling)
                                        {
                                            rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y - 10f);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                        }
                                        
                                    }
                                    else
                                    {
                                        if (!isjumping && !isfalling)
                                        {
                                            rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y - 10f);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                        }
                                    }
                                }
                                else
                                {
                                    if (isRight)
                                    {
                                        rb.velocity = new Vector2(1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-1 * (baseSpeed * dashSpeed), rb.velocity.y);
                                    }
                                }
                            }
                            else
                            {
                                rb.velocity = new Vector2((moveInput * 1) * speed, rb.velocity.y);
                            }
                            
                        }
                    }
                    
                }
                else
                {
                    if (isRight)
                    {
                        rb.velocity = new Vector2(1 - speed, jumpforce);
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed - 1, jumpforce);
                    }
                }
            }
        }
        else
        {
            if (hiperAtacking)
            {
                rb.velocity = new Vector2(rb.velocity.x * 0, rb.velocity.y);
            }
            
            
        }
    }

    public void PlayerImpulseUp(float impulse)
    {
        if (isgrounded)
        {
            rb.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
        }
    }
    public void SpawnGachaBall()
    {
        GameObject gachaball = Instantiate(controleArmas.gachaBall, new Vector2(transform.position.x, transform.position.y + 0.9f), Quaternion.identity);
        rideArmor = true;
        rb.simulated = false;
        transform.SetParent(gachaball.transform);
        gachaball.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        gachaball.GetComponent<GachaBallControll>().player = gameObject;
        //transform.position = new Vector2();
    }
    private void Flip()
    {
        if (moveInput < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isRight = false;
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            isRight = true;
        }
    }
    private void Teste()
    {
        //Debug.Log("teste");
    }
    public void PlayerApplyDamage(int dano)
    {
        if (!controleArmas.shieldUp)
        {
            
                if (canReceiveDamage)
                {
                    audioman.PlaySound("hurt");
                    wallJumping = false;
                    //timerWall = 0;
                    vida.ReceiveDamage(dano);
                    vida.DamageFX();
                    state = 0;
                    dashing = false;

                    canReceiveDamage = false;
                }
            
            
        }
        else
        {
            //shieldObj.GetComponent<ShieldScript>().hp--;
        }
        

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
       
       
        if (!blink.canblink)
        {
            switch (collision.transform.tag)
            {
                case "inimigo":
                    if (soundCrash)
                    {
                        collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(controleArmas.soundCrashdmg);
                    }
                    else if (fireUppercut)
                    {
                        collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(controleArmas.fireUppercutDmg);
                    }
                    if (canReceiveDamage && !controleArmas.shieldUp)
                    {
                        int danoAtualizado = ApplyDifficultyDamage(collision.GetComponent<Inimigo_Colider>().dano);

                        PlayerApplyDamage(danoAtualizado);
                    }
                    break;
                case "dmgobj":
                    if (soundCrash)
                    {
                        collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(controleArmas.soundCrashdmg);
                    }
                    else if (fireUppercut)
                    {
                        collision.GetComponent<Inimigo_Basico_Hp_Control>().ReceiveDamage(controleArmas.fireUppercutDmg);
                    }
                    if (canReceiveDamage && !controleArmas.shieldUp)
                    {
                        int danoAtualizado = ApplyDifficultyDamage(collision.GetComponent<Inimigo_Colider>().dano);

                        PlayerApplyDamage(danoAtualizado);
                    }
                    break;
                case "inimigoBullet":

                    if (soundCrash)
                    {
                        //collision.GetComponent<Inimigo_Basico_Hp_Control>().vida -= controleArmas.soundCrashdmg;
                    }
                    else
                    if (canReceiveDamage && !controleArmas.shieldUp)
                    {
                        int danoAtualizado = ApplyDifficultyDamage(collision.GetComponent<EnemyBullet>().damage);
                        PlayerApplyDamage(danoAtualizado);
                        collision.GetComponent<EnemyBullet>().hp--;
                    }
                    break;
                case "platform":
                    /*
                    isOnPlatform = true;
                    gameObject.transform.SetParent(collision.transform.parent);
                    collision.GetComponentInParent<ChlorineApplicator>().playerRB = gameObject.GetComponent<Rigidbody2D>();
                    collision.GetComponentInParent<ChlorineApplicator>().timer = collision.GetComponentInParent<ChlorineApplicator>().timerB;*/
                    if (!dashing && canReceiveDamage)
                    {
                        transform.SetParent(collision.transform);
                        rb.isKinematic = true;
                        isOnPlatform = true;
                        isgrounded = true;
                        isonwall = false;
                    }
                    
                    break;
                case "checkpoint":
                    gameManager.canCallSaveMenu = true;
                    
                    break;
            }
        }
        
    }
    public void AtachToPlatform()
    {

    }
    public int ApplyDifficultyDamage(int dano)
    {
        if (gameManager.dificulty == 0)
        {
            dano += 0;
        }else if (gameManager.dificulty == 1)
        {
            dano += 1;
        }
        else if(gameManager.dificulty == 2)
        {
            dano += 2;
        }

        return dano;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "bossDoor":
                collision.GetComponent<BossTrigger>().startBossFight = true;
                break;
            case "hp_pack1":
                vida.hp += 2;
                Destroy(collision.gameObject);
                break;
            case "hp_up":
                vida.AddHP();
                Destroy(collision.gameObject);
                break;
            case "water":
                onWater = true;
                gravity = waterGravity;
                rb.gravityScale = 1;
                rb.velocity = Vector2.up * (rb.velocity.y / 1.15f);
                break;
            
            case "RideArmor":
                transform.SetParent(collision.transform);
                Vector2 position = new Vector2(collision.transform.position.x, collision.transform.position.y - 0.3f);
                transform.position = position;
                rb.simulated = false;
                rideArmor = true;
                collision.GetComponent<RideArmorMove>().player = gameObject;
                collision.GetComponent<RideArmorMove>().onRide = true;
                gameManager.PlayerInvisible();
                gameManager.EnableRideHP();
                break;
            case "instantkill":
                vida.hp = 0;
                break;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "inimigoBullet":
                if (canReceiveDamage && !controleArmas.shieldUp)
                {
                    int danoAtualizado = ApplyDifficultyDamage(collision.gameObject.GetComponent<EnemyBullet>().damage);
                    PlayerApplyDamage(danoAtualizado);
                    collision.gameObject.GetComponent<EnemyBullet>().hp--;
                }
                break;
            case "platform":
                Vector2 pos = collision.contacts[0].point;
                Debug.Log(pos);
                
                if (pos.y < transform.position.y + 0.5f)
                {
                    Debug.Log("plataforma");
                    
                }
                break;


        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {

            case "wall":
                if (groundDetected)
                {
                    isgrounded = true;
                }
                break;
            case "ground":
                if (groundDetected)
                {
                    isgrounded = true;
                }
                break;
            case "breakObject":
                if (groundDetected)
                {
                    isgrounded = true;
                }
                break;
           
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            
            case "wall":             
                    //isgrounded = false;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case"water":
                onWater = false;
                gravity = originalgravity;
                rb.gravityScale = gravity;
                break;
            case "callBossText":
                ispaused = true;
                break;
            case "platform":
                /*
                gameObject.transform.SetParent(null);
                collision.GetComponentInParent<ChlorineApplicator>().playerRB = new Rigidbody2D();
                isOnPlatform = false;*/
             
                break;
            case "checkpoint":
                gameManager.canCallSaveMenu = false;
                break;
        }
    }
    public void ExitPlatform()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        isOnPlatform = false;
        isgrounded = false;
        isonwall = false;
    }
    public void ExitGigagaAtack()
    {
       hiperAtacking = false;
       ispaused = false;
       canMoveup = true;
       jumped = false;
       state = 7;
       rb.gravityScale = gravity;
   }
   public void GigaAtackStopMovingUp()
   {
       canMoveup = false;
   }
   public void CallGiga()
   {
        giga.SetTrigger("giga");
        armorControl.startRefillGiga = false;
   }
    public void StopPlayerMovement()
    {
        canmove = false;
        speed = 0;
        moveInput = 0;
        jumps = 0;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    public void ReturnPlayerMovement()
    {
        returnMovement = true;
    }
    public void SpawnSwordProjectile()
    {
        Vector2 playerPos;
        playerPos.y = transform.position.y + 1;
        if (isRight) { playerPos.x = transform.position.x + 2; } else { playerPos.x = (transform.position.x - 2); }
        if (_SwordProjectile)
        {
            GameObject bullet = Instantiate(_SwordProjectileObj, playerPos, Quaternion.identity);
            if (isRight)
            {
                bullet.GetComponent<BulletScript>().dir = 1;
            }
            else { bullet.GetComponent<BulletScript>().dir = -1; }
        }
        
    }
    public void ExitSpecials()
    {
        soundCrash = false;
        cansoundCrash = false;
        canFireUppercut = false;
        fireUppercut = false;
    }

}
