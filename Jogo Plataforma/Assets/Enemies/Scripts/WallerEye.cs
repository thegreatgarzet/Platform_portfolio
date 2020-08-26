using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallerEye : MonoBehaviour
{
    public GameObject bulletPrefab, bulletPrefab2;
    public int dir;
    public void Shot()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            EnemyBullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.gameObject.GetComponent<WallerBullet_DetectPlayer>().dir = dir;
            bullet.rb.velocity = Vector2.right * bullet.speed * dir;
        }
        else
        {
            EnemyBullet bullet = Instantiate(bulletPrefab2, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.gameObject.GetComponent<WallerBullet_DetectPlayer>().dir = dir;
            bullet.rb.velocity = Vector2.right * bullet.speed * dir;
        }
        
    }
}
