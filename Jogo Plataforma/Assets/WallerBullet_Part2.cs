using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class WallerBullet_Part2 : MonoBehaviour
{
    public GameObject bulletpart3;
    public void Shot()
    {
        EnemyBullet bullet1 = Instantiate(bulletpart3, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet1.rb.velocity = Vector2.right * bullet1.speed;
        EnemyBullet bullet2 = Instantiate(bulletpart3, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet2.rb.velocity = Vector2.left * bullet1.speed;
        EnemyBullet bullet3 = Instantiate(bulletpart3, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet3.rb.velocity = Vector2.up * bullet1.speed;
        EnemyBullet bullet4 = Instantiate(bulletpart3, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet4.rb.velocity = Vector2.down * bullet1.speed;
    }
}
