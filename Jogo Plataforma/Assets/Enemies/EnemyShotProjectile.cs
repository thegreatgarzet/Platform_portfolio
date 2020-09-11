using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotProjectile : MonoBehaviour
{
    public Vector2 spawnSpot, bulletdir;
    public Transform spot;
    public bool spawnOnObject;
    public float speed;
    public FlipToPlayer flip;
    public GameObject bullet;

    public void Shot()
    {
        Vector2 newpos;
        if (flip.isRight)
        {
            bulletdir.x = 1 * speed;
        }
        else
        {
            bulletdir.x = -1 * speed;
        }
        bulletdir.y *= speed;
        newpos.x = spawnSpot.x + transform.position.x;
        newpos.y = spawnSpot.y + transform.position.y;
        if (!spawnOnObject)
        {
            EnemyBullet bulletobj = Instantiate(bullet, newpos, Quaternion.identity).GetComponent<EnemyBullet>();
            bulletobj.isBounce = true;
            bulletobj.rb.velocity = bulletdir;
        }
        else
        {
            EnemyBullet bulletobj = Instantiate(bullet, spot.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bulletobj.isBounce = true;
            bulletobj.rb.velocity = bulletdir;
        }
        
    }
}
