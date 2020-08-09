using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallerBullet_DetectPlayer : MonoBehaviour
{
    public Transform player;
    public GameObject part2;
    public bool canSpawn;
    private void Awake()
    {
        player = GameObject.Find("MainChar").GetComponent<Transform>();
    }
    private void Update()
    {
        if(transform.position.x >= player.position.x && canSpawn)
        {
            Instantiate(part2, transform.position, Quaternion.identity);
            canSpawn = false;
            Destroy(gameObject);
        }
    }
}
