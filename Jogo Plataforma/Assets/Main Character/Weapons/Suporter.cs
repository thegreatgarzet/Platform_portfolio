using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suporter : MonoBehaviour
{
    public Transform[] shotspots ;
    public List<Transform> enemiesList;
    public Transform follow, target;
    public GameObject bullet;
    public float speedTravel, lifetime;
    public float shotTimer, shotTimerB;
    public int shotCount = 0, localNaLista;
    public bool hasEnemy;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        follow = GameObject.Find("SuporterFollowSpot").GetComponent<Transform>();
        shotTimer = shotTimerB;
        rb.velocity = Vector2.right * (speedTravel * 2);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, follow.position, speedTravel);
        lifetime -= Time.deltaTime;
        if (lifetime<=0)
        {
            GetComponentInChildren<Animator>().SetTrigger("Timeout");
        }
        if (enemiesList.Count > 0)
        {
            if(target == null)
            {
                
                if (!hasEnemy)
                {
                    localNaLista = Random.Range(0, enemiesList.Count);
                    target = enemiesList[localNaLista];
                    hasEnemy = true;
                }
                else
                {
                    enemiesList.Remove(enemiesList[localNaLista]);
                    if (enemiesList.Count > 0)
                    {
                        localNaLista = Random.Range(0, enemiesList.Count);
                        target = enemiesList[localNaLista];
                    }
                    Debug.Log("pulei");
                    
                }
                
                

            }
            else
            {
                
                shotTimer -= Time.deltaTime;
                if (shotTimer <= 0)
                {
                    if (shotCount < 2)
                    {
                        if (shotCount ==0)
                        {

                            GameObject tiro = Instantiate(bullet, shotspots[0].transform.position, Quaternion.identity);
                            tiro.GetComponent<SuporterBullet1>().targetPos = target;
                        }
                        else
                        {
                            GameObject tiro = Instantiate(bullet, shotspots[1].transform.position, Quaternion.identity);
                            tiro.GetComponent<SuporterBullet1>().targetPos = target;
                        }
                        
                        shotCount++;
                    }
                    else
                    {
                        GameObject tiro = Instantiate(bullet, shotspots[0].transform.position, Quaternion.identity);
                        tiro.GetComponent<SuporterBullet1>().targetPos = target;
                        GameObject tiro2 = Instantiate(bullet, shotspots[1].transform.position, Quaternion.identity);
                        tiro2.GetComponent<SuporterBullet1>().targetPos = target;
                        shotCount = 0;
                    }
                    shotTimer = shotTimerB;
                }

                
            }
           
        }
        else if(enemiesList.Count == 0)
        {
            shotTimer = shotTimerB;
            hasEnemy = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            enemiesList.Add(collision.transform);
            Debug.Log("Add");
        }
    }
}
