using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarSateliteControl : MonoBehaviour
{
    public int state, teleportNum, teleportTo, lastTeleportTo;
    Animator anim;
    Rigidbody2D rb;
    public Transform midOfRoom;
    public bool teleportDone;
    public Vector2[] teleportPoints, meteorPoints;


    //Auto-work
    public float timer, timerB;
        //
    //Weapons
    public GameObject _GalaxySphere, _GalaxyLaser, _Meteor, _Planet, _ChasingStar;
    public Transform[] galaxySpots;
    public Transform laserSpot;
    public int chasingStarCount = 0;

    public bool galaxyLaser, teleportedLeft, move=false, meteorShower=false, canMeteor = true, starShot = false, chasingStar = false, canChaseStar;

        //

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Intro");
        }
        
        switch (state)
        {
            case 0://Idle
                
                timer -= Time.deltaTime;
                if (timer<=0)
                {
                    teleportNum = 3;
                    teleportTo = Random.Range(0, 2);
                    state = 1;
                    timer = timerB;
                }
                break;
            case 1://Teleport entra
                break;
            case 2://Teleport sai
                break;
            case 4:
                if (move)//Ao ligar esse bool no state 4, move o jogador para direita/esquerda
                {
                    if (teleportedLeft)//aqui vai para direita
                    {
                        Vector2 newPoint;//posição aonde o jogador ira se mover
                        newPoint.x = midOfRoom.position.x + (teleportPoints[5].x - 1);
                        newPoint.y = midOfRoom.position.y + teleportPoints[5].y;
                        transform.position = Vector2.MoveTowards(transform.position, newPoint, 6.5f * Time.deltaTime);
                        if (transform.position.x == newPoint.x)
                        {
                            anim.SetTrigger("stopLaser");
                        }
                    }
                    else//aqui vai para esquerda
                    {
                        Vector2 newPoint;//posição aonde o jogador ira se mover
                        newPoint.x = midOfRoom.position.x + (teleportPoints[4].x + 1);
                        newPoint.y = midOfRoom.position.y + teleportPoints[4].y;
                        transform.position = Vector2.MoveTowards(transform.position, newPoint, 6.5f * Time.deltaTime);
                        if (transform.position.x == newPoint.x)
                        {
                            anim.SetTrigger("stopLaser");
                        }
                    }
                }
                break;
            case 5:
                if (canMeteor)
                {
                    Invoke("Meteors1", 0.3f);
                    Invoke("Meteors2", 1.0f);
                    Invoke("Meteors1", 1.7f);
                    Invoke("Meteors2", 2.4f);
                    meteorShower = false;
                    canMeteor = false;
                }
                break;
            case 7:
                if (canChaseStar)
                {
                    Invoke("ShotChasingStar", 0.3f);
                    Invoke("ShotChasingStar", 1.0f);
                    Invoke("ShotChasingStar", 1.7f);
                    Invoke("ShotChasingStar", 2.4f);
                    Invoke("ShotChasingStar", 3.1f);
                    Invoke("ShotChasingStar", 3.8f);
                    Invoke("ResetState", 3.9f);
                    canChaseStar = false;
                    chasingStar = false;
                }
                
                break;
        }
        anim.SetInteger("state", state);//Muda o state no animator
    }
    public void EnableMeteors()
    {
        canMeteor = true;
    }
    public void ShotChasingStar()
    {
        GameObject star = Instantiate(_ChasingStar, transform.position, Quaternion.identity);
        if (teleportedLeft)
        {
            switch (chasingStarCount)
            {
                case 0:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + -4.5f, midOfRoom.position.y + -3.5f);
                    break;
                case 1:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + -2f, midOfRoom.position.y + -3.5f);
                    break;
                case 2:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x, midOfRoom.position.y + -3.5f);
                    break;
                case 3:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + 2f, midOfRoom.position.y + -3.5f);
                    break;
                case 4:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + 4.5f, midOfRoom.position.y + -3.5f);
                    break;
                case 5:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + 5.5f, midOfRoom.position.y + -1.5f);
                    break;
            }
        }
        else
        {
            switch (chasingStarCount)
            {
                case 0:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + 4.5f, midOfRoom.position.y + -3.5f);
                    break;
                case 1:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + 2f, midOfRoom.position.y + -3.5f);
                    break;
                case 2:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x, midOfRoom.position.y + -3.5f);
                    break;
                case 3:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + -2f, midOfRoom.position.y + -3.5f);
                    break;
                case 4:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + -4.5f, midOfRoom.position.y + -3.5f);
                    break;
                case 5:
                    star.GetComponent<BossChasingStar>().target = new Vector2(midOfRoom.position.x + -5.5f, midOfRoom.position.y + -1.5f);
                    break;
            }
        }
        chasingStarCount++;
        if (chasingStarCount >= 6)
        {
            chasingStarCount = 0;
        }
    }
    public void Meteors1()
    {
        Vector2 newPoint;//posição aonde o jogador ira se mover
        newPoint.x = midOfRoom.position.x + (meteorPoints[0].x);
        newPoint.y = midOfRoom.position.y + meteorPoints[0].y;
        Instantiate(_Meteor, newPoint, Quaternion.identity);
        //
        newPoint.x = midOfRoom.position.x + (meteorPoints[1].x);
        Instantiate(_Meteor, newPoint, Quaternion.identity);
        //
        newPoint.x = midOfRoom.position.x + (meteorPoints[2].x);
        Instantiate(_Meteor, newPoint, Quaternion.identity);
    }
    public void Meteors2()
    {
        Vector2 newPoint;//posição aonde o jogador ira se mover
        newPoint.x = midOfRoom.position.x + meteorPoints[3].x;
        newPoint.y = midOfRoom.position.y + meteorPoints[3].y;
        Instantiate(_Meteor, newPoint, Quaternion.identity);
        //
        newPoint.x = midOfRoom.position.x + meteorPoints[4].x;
        Instantiate(_Meteor, newPoint, Quaternion.identity);
        //
        canMeteor = true;
        ResetState();
    }
    public void CheckIfKeepTeleport()
    {
        if (chasingStar)
        {
            canChaseStar = true;
            state = 7;
        }
        else if (starShot)
        {
            state = 6;
        }
        else if(galaxyLaser)//Ao sair do teleporte com esse bool ativo, entra no state 4
        {
            state = 4;
        }
        else if (meteorShower)//Ao sair do teleporte com esse bool ativo, entra no state 5
        {
            state = 5;
        }
        else { //Caso nenhum bool esteja ativo, só faz o esquema padrão do teleporte
            teleportNum--;
            if (teleportNum < 0)//ao terminar os 4 teleportes padrão, escolhe um estado aleatório
            {
                state = Random.Range(0, 101);//Esse rand escolhe qual ação fazer depois do teleporte
                //De 0 a 100
                /*
                 0 a 20 = meteoro
                 21 a 60 = laser
                 61 a 100 = star shot
                 
                 */
                if (state >= 16 && state <= 40)
                {
                    galaxyLaser = true;
                    
                }
                else if (state >= 0 && state <= 15)
                {
                    meteorShower = true;
                    
                }
                else if(state >= 41 && state <= 70)
                {
                    starShot = true;
                   
                }
                else if (state >= 71 && state <= 100)
                {
                    chasingStar = true;
                   
                }
                teleportNum = 0;
                state = 1;
            }
            else
            {
                state = 1;
            }
        }
    }
    public void TeleportChar()
    {
        if (chasingStar)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + -5f;
                newPoint.y = midOfRoom.position.y + 2f;
                transform.position = newPoint;
                teleportedLeft = true;

            }
            else if (rand == 1)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + 5f;
                newPoint.y = midOfRoom.position.y + 2f;
                transform.position = newPoint;
                teleportedLeft = false;
            }
        }
        else if (starShot)//Se for teleportar com esse bool, vai para os pontos abaixo
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + -5f;
                newPoint.y = midOfRoom.position.y + -2f;
                transform.position = newPoint;
                teleportedLeft = true;

            }
            else if (rand == 1)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + 5f;
                newPoint.y = midOfRoom.position.y + -2f;
                transform.position = newPoint;
                teleportedLeft = false;
            }
        }
        else if(galaxyLaser)//Se for teleportar com esse bool, vai para os pontos abaixo
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + teleportPoints[4].x;
                newPoint.y = midOfRoom.position.y + teleportPoints[4].y;
                transform.position = newPoint;
                teleportedLeft = true;
                
            }else if (rand == 1)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + teleportPoints[5].x;
                newPoint.y = midOfRoom.position.y + teleportPoints[5].y;
                transform.position = newPoint;
                teleportedLeft = false;
            }
            
        }
        else if (meteorShower)//Se for teleportar com esse bool ativo, vai para o ponto abaixo
        {
            Vector2 newPoint;
            newPoint.x = midOfRoom.position.x + teleportPoints[6].x;
            newPoint.y = midOfRoom.position.y + teleportPoints[6].y;
            transform.position = newPoint;
        }
        else if (teleportTo == 0)//caso nenhum bool esteja ativo, só vai teleportando aleatório
        {
                if (teleportNum == 0)
                {
                    Vector2 newPoint;
                    newPoint.x = midOfRoom.position.x + teleportPoints[0].x;
                    newPoint.y = midOfRoom.position.y + teleportPoints[0].y;
                    transform.position = newPoint;
                }
                else if (teleportNum == 1)
                {
                    Vector2 newPoint;
                    newPoint.x = midOfRoom.position.x + teleportPoints[2].x;
                    newPoint.y = midOfRoom.position.y + teleportPoints[2].y;
                    transform.position = newPoint;
                }
                else if (teleportNum == 2)
                {
                    Vector2 newPoint;
                    newPoint.x = midOfRoom.position.x + teleportPoints[1].x;
                    newPoint.y = midOfRoom.position.y + teleportPoints[1].y;
                    transform.position = newPoint;
                }
                else if (teleportNum == 3)
                {
                    Vector2 newPoint;
                    newPoint.x = midOfRoom.position.x + teleportPoints[3].x;
                    newPoint.y = midOfRoom.position.y + teleportPoints[3].y;
                    transform.position = newPoint;
                }
        }else if(teleportTo == 1)
        {
            if (teleportNum == 0)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + teleportPoints[2].x;
                newPoint.y = midOfRoom.position.y + teleportPoints[2].y;
                transform.position = newPoint;
            }
            else if (teleportNum == 1)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + teleportPoints[0].x;
                newPoint.y = midOfRoom.position.y + teleportPoints[0].y;
                transform.position = newPoint;
            }
            else if (teleportNum == 2)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + teleportPoints[3].x;
                newPoint.y = midOfRoom.position.y + teleportPoints[3].y;
                transform.position = newPoint;
            }
            else if (teleportNum == 3)
            {
                Vector2 newPoint;
                newPoint.x = midOfRoom.position.x + teleportPoints[1].x;
                newPoint.y = midOfRoom.position.y + teleportPoints[1].y;
                transform.position = newPoint;
            }
        }
        
        state = 2;
    }
    public void ShootGalaxySphere()//Atira as bolota pros lado
    {
        GameObject bullet1 = Instantiate(_GalaxySphere, galaxySpots[0].position, Quaternion.identity);
        //bullet1.GetComponent<EnemyBullet>().dir = -1;
        bullet1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bullet1.GetComponent<EnemyBullet>().speed, 0), ForceMode2D.Impulse);
        GameObject bullet2 = Instantiate(_GalaxySphere, galaxySpots[1].position, Quaternion.identity);
        //bullet2.GetComponent<EnemyBullet>().dir = 1;
        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(bullet1.GetComponent<EnemyBullet>().speed, 0), ForceMode2D.Impulse);

    }
    public void StarShot()
    {
        GameObject bullet1 = Instantiate(_Planet, galaxySpots[0].position, Quaternion.identity);
        if (teleportedLeft)
        {
            bullet1.GetComponent<EnemyBullet>().dir = 1;
        }
        else
        {
            bullet1.GetComponent<EnemyBullet>().dir = -1;
        }
        starShot = false;
    }
    public void GalaxyLaser()//Atira o laser pra baixo
    {
        GameObject laser = Instantiate(_GalaxyLaser, laserSpot.position, Quaternion.identity);
        laser.transform.SetParent(laserSpot);
    }
    public void ResetState()//Reseta o estado para idle
    {
        state = 0;
    }
    public void StopLaserShot()
    {
        GetComponentInChildren<LaserShot>().EndLaser();
    }
    
    public void DisableLaserMove()
    {
        move = false;
        galaxyLaser = false;
    }
}
