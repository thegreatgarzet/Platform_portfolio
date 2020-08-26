using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class WallerBullet_Part2 : MonoBehaviour
{
    public GameObject bulletpart3, bulletpart3_2;
    public bool _X;
    public void Shot()
    {
        if (!_X)
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
        else
        {
            EnemyBullet bullet1 = Instantiate(bulletpart3_2, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet1.rb.velocity = new Vector2(bullet1.speed / 2, bullet1.speed / 2);
            EnemyBullet bullet2 = Instantiate(bulletpart3_2, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet2.rb.velocity = new Vector2(-bullet1.speed / 2, bullet1.speed / 2);
            EnemyBullet bullet3 = Instantiate(bulletpart3_2, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet3.rb.velocity = new Vector2(-bullet1.speed / 2, -bullet1.speed / 2);
            EnemyBullet bullet4 = Instantiate(bulletpart3_2, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet4.rb.velocity = new Vector2(bullet1.speed / 2, -bullet1.speed / 2);
        }
        
    }
}
