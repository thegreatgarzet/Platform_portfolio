using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleArmas : MonoBehaviour
{
    public List<GameObject> armas, medium,hiper, fx, hfx;
    public List<int> idArmas;
    public List<AmmoRefillControl> ammoRefillControls;
    public int idArma, idColor, idPosiçaoArma;
    public MovementController movement;
    public Transform pontoTiro;
    public bool hipershot=false, shieldUp=false, suporterSpawned=false,  canshot=true, canCharge=false, _HypeCharge, _SwordSlash;
    public bool plasmaBuster;
    public float timer, timerCharge2, timerCharge3;
    public bool canUseFx = true, canUseFx2 = true, canUseFx3 = true;
    public GameObject shield, chargeFX, fxReady, fxReady2, fxReady3;
    OverHeatControl _overHeatControl;
    public OutlineColor outlineColor;
    public GameObject toyChargedObj;
    public List<GameObject> toyCharged;
    public bool toyCanShot;
    public GameObject newFireBall;
    Vector2 pos, pos2;
    public int soundCrashdmg, fireUppercutDmg;
    public GameObject gachaBall;

    AudioControl audioman;
    private void Awake()
    {
        audioman = FindObjectOfType<AudioControl>();
        idPosiçaoArma = 0;
        idArma = 0;
        idColor = 0;
        timer = 0; ;
        _overHeatControl = GetComponentInParent<OverHeatControl>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        idColor = idArmas[idPosiçaoArma];
      
        if (!movement.ispaused && !movement.rideArmor)
        {
            if (movement.isRight)
            {
                pos = new Vector2(GetComponentInParent<Transform>().position.x - 1.4f, GetComponentInParent<Transform>().position.y + 0.3f);
                pos2 = new Vector2(GetComponentInParent<Transform>().position.x - 1.4f, GetComponentInParent<Transform>().position.y - 0.3f);
            }
            else
            {
                pos = new Vector2(GetComponentInParent<Transform>().position.x + 1.4f, GetComponentInParent<Transform>().position.y + 0.3f);
                pos2 = new Vector2(GetComponentInParent<Transform>().position.x + 1.4f, GetComponentInParent<Transform>().position.y - 0.3f);
            }
            toyCharged[0].transform.position = Vector2.MoveTowards(toyCharged[0].transform.position, pos, 1 * Time.deltaTime);
            toyCharged[1].transform.position = Vector2.MoveTowards(toyCharged[1].transform.position, pos2, 1 * Time.deltaTime);
            /*
            for (int i = 0; i < toyCharged.Count; i++)
            {
               
                if (toyCharged[i].activeSelf)
                {
                    toyCharged[i].transform.position = Vector2.MoveTowards(toyCharged[i].transform.position, pos, 2 * Time.deltaTime);
                }
            }*/
            if (idArmas.Count != 1)
            {
                if (Input.GetButtonDown("TrocarArmaDir"))
                {
                    ChangeWeapon(true);
                }
                if (Input.GetButtonDown("TrocarArmaEsq"))
                {
                    ChangeWeapon(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                idArmas.Sort();
            }
            if (Input.GetButtonDown("Atirar"))
            {
                if (canshot && ammoRefillControls[idArma].canShot)
                {
                    if (ammoRefillControls[idArma].canReduce)
                    {
                        ammoRefillControls[idArma].ReduceValue();
                        for (int i = 0; i < toyCharged.Count; i++)
                        {
                            if (toyCharged[i].activeSelf)
                            {
                                toyCharged[i].GetComponent<ToyCharged>().ShotProjectile();
                            }
                        }
                        switch (idArma)
                        {
                            case 0:
                                if (!_HypeCharge)
                                {
                                    Instantiate(fx[0], transform);
                                    GameObject bullet = Instantiate(armas[idArma], transform.position, Quaternion.identity);
                                    if (movement.isRight)
                                    {
                                        bullet.GetComponent<BulletScript>().dir = 1;
                                    }
                                    else { bullet.GetComponent<BulletScript>().dir = -1; }
                                    audioman.PlaySound("charge1");
                                }
                                else
                                {
                                    GameObject bulletMedium = Instantiate(medium[idArma], transform.position, Quaternion.identity);
                                    if (movement.isRight)
                                    {
                                        bulletMedium.GetComponent<BulletScript>().dir = 1;
                                    }
                                    else { bulletMedium.GetComponent<BulletScript>().dir = -1; }
                                }
                                
                                break;
                            case 1:
                                InstantiateFireBall();
                                
                                break;
                            case 2:
                                GameObject soundring = Instantiate(armas[idArma], transform.position, Quaternion.identity);
                                if (movement.isRight)
                                {
                                    soundring.GetComponent<BulletScript>().dir = 1;
                                }
                                else { soundring.GetComponent<BulletScript>().dir = -1; }
                                break;
                            case 3:
                                
                                    GameObject ball1 = Instantiate(armas[idArma], transform.position, Quaternion.identity);
                                    GameObject ball2 = Instantiate(armas[idArma], transform.position, Quaternion.identity);
                                    if (movement.isRight)
                                    {
                                        ball1.GetComponent<BulletScript>().dir = 1;
                                        ball2.GetComponent<BulletScript>().dir = 1;
                                    }
                                    else
                                    {
                                        ball1.GetComponent<BulletScript>().dir = -1;
                                        ball2.GetComponent<BulletScript>().dir = -1;
                                    }
                                    ball2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ball2.GetComponent<BulletScript>().speed * 20);
                                   
                                
                                break;
                            case 4:
                                GameObject star = Instantiate(armas[idArma], new Vector2(transform.position.x, transform.position.y + 0.2f), Quaternion.identity);
                                GameObject star2 = Instantiate(armas[idArma], new Vector2(transform.position.x, transform.position.y - 0.2f), Quaternion.identity);
                                if (movement.isRight)
                                {
                                    star.GetComponent<ChasingStar>().dir = 1;
                                    star2.GetComponent<ChasingStar>().dir = 1;
                                }
                                else { star.GetComponent<ChasingStar>().dir = -1; star2.GetComponent<ChasingStar>().dir = -1; }
                                break;
                        }
                    }
                }

            }
            if (Input.GetButton("Atirar"))
            {
                if (idArma == 0)
                {
                    chargeFX.SetActive(true);
                    timer += Time.deltaTime;
                    if (timer >1 && timer < timerCharge2)
                    {
                        if (canUseFx)
                        {
                            GameObject fx = Instantiate(fxReady, chargeFX.transform.position, Quaternion.identity);
                            fx.transform.SetParent(chargeFX.transform.parent);
                            audioman.PlaySound("chargedone");
                            outlineColor.charge = 1;
                            canUseFx = false;
                        }
                    }
                    if (timer >= timerCharge2)
                    {
                        
                        if (canUseFx2)
                        {
                            GameObject fx = Instantiate(fxReady2, chargeFX.transform.position, Quaternion.identity);
                            fx.transform.SetParent(chargeFX.transform.parent);
                            audioman.PlaySound("chargedone");
                            outlineColor.charge = 2;
                            canUseFx2 = false;
                        }
                        if (!_SwordSlash)
                        {
                            hipershot = true;
                        }
                        else
                        {
                            movement._SwordSlash = true;
                        }
                        

                    }
                    if(timer >= timerCharge3)
                    {
                        if (canUseFx3)
                        {
                            GameObject fx = Instantiate(fxReady3, chargeFX.transform.position, Quaternion.identity);
                            fx.transform.SetParent(chargeFX.transform.parent);
                            canUseFx3 = false;
                        }
                    }
                }
                else if (canCharge)
                {
                    chargeFX.SetActive(true);
                    timer += Time.deltaTime;
                    if (timer >= timerCharge2 && timer < timerCharge3)
                    {
                        //chargeFX.SetActive(false);
                        if (canUseFx2)
                        {
                            GameObject fx = Instantiate(fxReady2, chargeFX.transform.position, Quaternion.identity);
                            fx.transform.SetParent(chargeFX.transform.parent);
                            audioman.PlaySound("chargedone");
                            outlineColor.charge = 1;
                            canUseFx2 = false;
                        }
                        if (!_SwordSlash)
                        {
                            if (idArma == 2)
                            {
                                movement.cansoundCrash = true;
                            }
                            else
                            {
                                hipershot = true;
                            }
                                
                        }
                        else if(_SwordSlash)
                        {
                            movement._SwordSlash = true;
                        }

                    }else if (timer >= timerCharge3)
                    {
                        if (canUseFx3)
                        {
                            GameObject fx = Instantiate(fxReady3, chargeFX.transform.position, Quaternion.identity);
                            fx.transform.SetParent(chargeFX.transform.parent);
                            audioman.PlaySound("chargedone");
                            canUseFx3 = false;
                        }
                        if (idArma == 1)
                        {
                            movement.canFireUppercut = true;
                        }else
                        if(idArma == 3)
                        {
                            movement.canGachaBall = true;
                        }
                    }
                }

            }
            if (Input.GetButtonUp("Atirar"))
            {
                chargeFX.SetActive(false);
                canUseFx = true;
                canUseFx2 = true;
                canUseFx3 = true;
                outlineColor.charge = 0;
                if (hipershot)//ChargeShot 3
                {
                    //Instantiate(fxReady, pontoTiro.transform);
                    if (!canCharge)
                    {
                        if (idArma == 0)
                        {
                            if (timer > 1 && timer <= timerCharge2)//Charge shot 2
                            {
                                switch (idArma)
                                {
                                    case 0:
                                        print("tiro2");
                                        GameObject bulletMedium = Instantiate(medium[idArma], transform.position, Quaternion.identity);
                                        if (movement.isRight)
                                        {
                                            bulletMedium.GetComponent<BulletScript>().dir = 1;
                                        }
                                        else { bulletMedium.GetComponent<BulletScript>().dir = -1; }
                                        audioman.PlaySound("charge2");
                                        break;
                                }
                            }else if(timer >= timerCharge2)
                            {
                                GameObject bulletHiper = Instantiate(hiper[0], transform.position, Quaternion.identity);
                                if (movement.isRight)
                                {
                                    bulletHiper.GetComponent<BulletScript>().dir = 1;
                                }
                                else { bulletHiper.GetComponent<BulletScript>().dir = -1; }
                                audioman.PlaySound("charge3");
                            }
                            
                        }
                    }
                    else
                    {
                        switch (idArma)
                        {
                            case 0:
                                if (timer > 1 && timer <= timerCharge2)//Charge shot 2
                                {
                                   
                                    print("tiro2");
                                    GameObject bulletMedium = Instantiate(medium[idArma], transform.position, Quaternion.identity);
                                    if (movement.isRight)
                                    {
                                        bulletMedium.GetComponent<BulletScript>().dir = 1;
                                    }
                                    else { bulletMedium.GetComponent<BulletScript>().dir = -1; }
                                    audioman.PlaySound("charge2");
                                    break;
                                    
                                }
                                else if (timer >= timerCharge2)
                                {
                                    GameObject bulletHiper = Instantiate(hiper[idArma], transform.position, Quaternion.identity);
                                    if (plasmaBuster) { bulletHiper.GetComponent<BulletScript>().isHyper = true; }
                                    if (movement.isRight)
                                    {
                                        bulletHiper.GetComponent<BulletScript>().dir = 1;
                                    }
                                    else { bulletHiper.GetComponent<BulletScript>().dir = -1; }
                                    audioman.PlaySound("charge3");
                                }

                                
                                break;
                            case 1:
                                //Instantiate(hiper[idArma], transform.position, Quaternion.identity);
                                InstantiateFireBallHyper();
                                
                                
                                break;
                            case 2:
                                /*if (!shieldUp)
                                {
                                    shield.SetActive(true);
                                    shieldUp = true;
                                }*/
                                

                                break;
                            case 3:
                                /*
                                if (!suporterSpawned)
                                {
                                    Instantiate(hiper[idArma - 1], transform.position, Quaternion.identity);
                                    suporterSpawned = true;
                                }
                                else
                                {
                                    GameObject.Find("SuporterBase(Clone)").GetComponentInChildren<Animator>().SetTrigger("Timeout");
                                    Instantiate(hiper[idArma], transform.position, Quaternion.identity);
                                    suporterSpawned = true;
                                }*/
                                //toyCharged.Add(Instantiate(toyChargedObj, pontoTiro.position, Quaternion.identity));
                                /*toyCharged[0].SetActive(true);
                                toyCharged[0].GetComponent<ToyCharged>().id = 0;
                                toyCharged[1].SetActive(true);
                                toyCharged[1].GetComponent<ToyCharged>().id = 1;*/
                                break;
                            case 4:
                                GameObject blackhole = Instantiate(hiper[idArma-1], transform.position, Quaternion.identity);
                                if (movement.isRight)
                                {
                                    blackhole.GetComponent<BlackHoleControl>().dir = 1;
                                }
                                else
                                {
                                    blackhole.GetComponent<BlackHoleControl>().dir = -1;
                                }
                                break;
                        }
                    }

                }
                else if(timer >1  && timer <= timerCharge2)//Charge shot 2
                {
                    switch (idArma)
                    {
                        case 0:
                            print("tiro2");
                            GameObject bulletMedium = Instantiate(medium[idArma], transform.position, Quaternion.identity);
                            if (movement.isRight)
                            {
                                bulletMedium.GetComponent<BulletScript>().dir = 1;
                            }
                            else { bulletMedium.GetComponent<BulletScript>().dir = -1; }
                            audioman.PlaySound("charge2");
                            break;
                    }
                }

                hipershot = false;
                timer = 0;
            }
        }

    }

    public void InstantiateFireBall()
    {
        int getDir;
        if (movement.isRight)
        {
            getDir = 1;
        }
        else
        {
            getDir = -1;
        }
        //Fireball 1
        GameObject fireBall1 = Instantiate(armas[idArma], transform.position, Quaternion.identity);
        fireBall1.GetComponent<FireBallScript>().dir = getDir;
        fireBall1.GetComponent<FireBallScript>().angle = fireBall1.GetComponent<FireBallScript>().angleDown;
  
        //Fireball 2
        GameObject fireBall2 = Instantiate(armas[idArma], transform.position, Quaternion.identity);
        fireBall2.GetComponent<FireBallScript>().dir = getDir;
        fireBall2.GetComponent<FireBallScript>().angle = 0;
        
        //Fireball 3
        GameObject fireBall3 = Instantiate(armas[idArma], transform.position, Quaternion.identity);
        fireBall3.GetComponent<FireBallScript>().dir = getDir;
        fireBall3.GetComponent<FireBallScript>().angle = fireBall1.GetComponent<FireBallScript>().angleUp;
    }

    public void ChangeWeapon(bool frente)
    {
        if (frente)
        {
            ammoRefillControls[idArma].gameObject.SetActive(false);
            if (idPosiçaoArma == idArmas.Count - 1)
            {
                idPosiçaoArma = 0;
            }
            else
            {
                idPosiçaoArma++;
            }
            idArma = idArmas[idPosiçaoArma];
            ammoRefillControls[idArma].gameObject.SetActive(true);
        }
        else
        {
            ammoRefillControls[idArma].gameObject.SetActive(false);
            if (idPosiçaoArma == 0)
            {
                idPosiçaoArma = idArmas.Count - 1;
            }
            else
            {
                idPosiçaoArma--;
            }
            idArma = idArmas[idPosiçaoArma];
            ammoRefillControls[idArma].gameObject.SetActive(true);
        }
    }
    public void DirectChangeWeapon(int id)
    {
        ammoRefillControls[idArma].gameObject.SetActive(false);
        idPosiçaoArma = id;
        idArma = idArmas[idPosiçaoArma];
        ammoRefillControls[idArma].gameObject.SetActive(true);
    }
    public void InstantiateFireBallHyper()
    {
        _overHeatControl.timer = _overHeatControl.timerB;
        _overHeatControl.overHeat = true;

        GameObject fireBall = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall.GetComponent<FireBallScript>().dir = 1;
        
        GameObject fireBall1 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall1.GetComponent<FireBallScript>().dir = -1;
        
        GameObject fireBall2 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall2.GetComponent<FireBallScript>().dir = -1;
        fireBall2.GetComponent<FireBallScript>().angle = fireBall2.GetComponent<FireBallScript>().angleDown;

        GameObject fireBall3 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall3.GetComponent<FireBallScript>().dir = 1;
        fireBall3.GetComponent<FireBallScript>().angle = fireBall3.GetComponent<FireBallScript>().angleDown;

        GameObject fireBall4 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall4.GetComponentInChildren<Transform>().eulerAngles = Vector3.forward * -90;
        fireBall4.GetComponent<FireBallScript>().dir = 0;
        fireBall4.GetComponent<FireBallScript>().angle = fireBall4.GetComponent<FireBallScript>().angleDown;

        GameObject fireBall5 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall5.GetComponentInChildren<Transform>().eulerAngles = Vector3.forward * 90;
        fireBall5.GetComponent<FireBallScript>().dir = 0;
        fireBall5.GetComponent<FireBallScript>().angle = fireBall5.GetComponent<FireBallScript>().angleUp;

        GameObject fireBall6 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall6.GetComponent<FireBallScript>().dir = -1;
        fireBall6.GetComponent<FireBallScript>().angle = fireBall6.GetComponent<FireBallScript>().angleUp;

        GameObject fireBall7 = Instantiate(newFireBall, transform.position, Quaternion.identity);
        fireBall7.GetComponent<FireBallScript>().dir = 1;
        fireBall7.GetComponent<FireBallScript>().angle = fireBall7.GetComponent<FireBallScript>().angleUp;
    }
}
