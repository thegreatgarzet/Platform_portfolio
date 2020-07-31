using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public int bossNum;
    public float timer;
    public bool startBossFight=false, callNewCam=true;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (startBossFight)
        {
            if (callNewCam)
            {
                gameManager.EnterBossRoom(bossNum);
                callNewCam = false;
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                StartBossFight();
            }
        }
    }
    public void StartBossFight()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        Destroy(gameObject);
    }
}
