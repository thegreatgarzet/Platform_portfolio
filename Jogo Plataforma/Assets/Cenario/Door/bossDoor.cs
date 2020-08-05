using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDoor : MonoBehaviour
{
    public Transform[] movePoints;
    GameObject playerTransf;
    GameManager gameManager;
    AudioControl audioman;
    public DialogueTrigger trigger;
    public Animator anim;
    public BoxCollider2D box, triggerBox;
    public bool movePlayer=false, getPos, moveRight;
    public bool leadsToBoss, bossRight, text_played=false, bossBeated=false;
    public int bossId;
    public float speed;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerTransf = GameObject.Find("MainChar");
        trigger = GetComponent<DialogueTrigger>();
        gameManager = FindObjectOfType<GameManager>();
        audioman = FindObjectOfType<AudioControl>();
    }
    private void Update()
    {
        if (movePlayer)
        {
            
            box.enabled = false;
            if (!moveRight)
            {
                //playerTransf.position = Vector2.MoveTowards(playerTransf.position, movePoints[0].position, speed * Time.deltaTime);
                playerTransf.transform.position += Vector3.right* -speed *Time.deltaTime;
                if (playerTransf.transform.position == movePoints[0].position)
                {

                    movePlayer = false;
                    getPos = true;
                }
            }
            else
            {
                //playerTransf.position = Vector2.MoveTowards(playerTransf.position, movePoints[1].position, speed * Time.deltaTime);
                playerTransf.transform.position += Vector3.right * speed * Time.deltaTime;
                if (playerTransf.transform.position == movePoints[1].position)
                {
                   
                    movePlayer = false;
                    
                }
            }
        }
        else
        {
            
        }
  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && getPos)
        {
            if(transform.position.x < playerTransf.transform.position.x)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
            anim.SetInteger("state", 1);
            audioman.PlaySound("dooropen");
            playerTransf.GetComponent<MovementController>().ispaused = true;
            playerTransf.GetComponent<MovementController>().dashing = false;
            playerTransf.GetComponent<MovementController>().state=2;
            gameManager.canCallMenu = false;
            getPos = false;
        }
    }
    public void MovePlayer()
    {
        movePlayer = true;
    }
    public void ReleasePlayer()
    {
        if (!leadsToBoss)
        {
            playerTransf.GetComponent<MovementController>().ispaused = false;
            gameManager.canCallMenu = true;
        }
        else if (leadsToBoss &&  bossBeated)
        {
            playerTransf.GetComponent<MovementController>().ispaused = false;
            gameManager.canCallMenu = true;
        }else if(leadsToBoss && !bossBeated && !gameManager.canCutscene)
        {
            playerTransf.GetComponent<MovementController>().ispaused = false;
            gameManager.canCallMenu = true;
        } 

     }
}
