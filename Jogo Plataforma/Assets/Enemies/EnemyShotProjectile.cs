using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotProjectile : MonoBehaviour
{
    public Vector2 spawnSpot, bulletdir;
    public GameObject bullet;
    public void Shot()
    {
        Vector2 newpos;
        newpos.x = spawnSpot.x + transform.position.x;
        newpos.y = spawnSpot.y + transform.position.y;
        Instantiate(bullet, newpos, Quaternion.identity);
    }
}
