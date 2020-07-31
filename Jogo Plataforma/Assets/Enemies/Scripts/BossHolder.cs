using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHolder : MonoBehaviour
{
    public GameObject boss;
    GameManager gm;
    public bossDoor[] bossDoors;
    public bool bossAlive=true;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (bossAlive)
        {
            if (boss == null)
            {
                foreach (bossDoor bossDoor in bossDoors)
                {
                    bossDoor.bossBeated = true;
                }
                gm.ExitBossRoom();
                bossAlive = false;
                Destroy(gameObject, 1f);
            }
        }
        
    }
}
