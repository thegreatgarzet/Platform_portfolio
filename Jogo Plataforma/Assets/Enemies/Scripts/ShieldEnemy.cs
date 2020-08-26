using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    public GameObject shield, bullet;
    public float timer, timerb;
    public BoxCollider2D box;
    public FlipToPlayer flip;
    public Transform shotspot;
    private void Update()
    {
        if(shield == null)
        {
            box.enabled = true;
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                GetComponent<Animator>().SetInteger("Shot", 1);
                timer = timerb;
            }
        }
    }
    public void ResetTimer()
    {
        timer = timerb;
        GetComponent<Animator>().SetInteger("Shot", 0);
    }
    public void Shot()
    {
        GameObject objBullet = Instantiate(bullet, shotspot.position, Quaternion.identity);
        if (flip.isRight)
        {
            objBullet.GetComponent<EnemyBullet>().dir = 1;
        }
        else
        {
            objBullet.GetComponent<EnemyBullet>().dir = -1;
        }
    }
}
