using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallerEye : MonoBehaviour
{
    public GameObject bulletPrefab;
    public void Shot()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet.rb.velocity = Vector2.right * bullet.speed;
    }
}
