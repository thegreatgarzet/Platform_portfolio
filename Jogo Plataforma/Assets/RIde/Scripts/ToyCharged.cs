using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCharged : MonoBehaviour
{
    public GameObject projectile;
    public MovementController movementController;
    public ControleArmas controleArmas;
    public int id;
    public float timer, timerB;
    private void Awake()
    {
        movementController = FindObjectOfType<MovementController>();
        controleArmas = GetComponentInParent<ControleArmas>();
        
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            DeactivateObj();
        }
    }
    public void ShotProjectile()
    {
        int direction;
        if (movementController.isRight)
        {
            direction = 1;
        }
        else { direction = -1; }
        GameObject bullet = Instantiate(projectile, new Vector2(transform.position.x + 0.6f, transform.position.y), Quaternion.identity);
        bullet.GetComponent<BulletScript>().dir = direction;
    }
    public void DeactivateObj()
    {
        timer = timerB;
        controleArmas.toyCharged[id].transform.position = controleArmas.gameObject.transform.position;
        controleArmas.toyCharged[id].SetActive(false);
    }
}
