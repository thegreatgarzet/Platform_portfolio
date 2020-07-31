using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralFunctions : MonoBehaviour
{
    
    GameManager gameManager;
    bool doorOpen = false;
    public bool moveUp, destroyOnLeave, destroyOnCol;
    public LayerMask layer;
    public bool hasRB;
    public float speed, destroyOnColTimer;
    Animator fades;
    MovementController player;
    Rigidbody2D rb;
    HiperAtack hiper;
    private void Start()
    {
        if (hasRB)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        fades = GameObject.Find("Fade").GetComponent<Animator>();
        player = GameObject.Find("MainChar").GetComponent<MovementController>();
        hiper = GetComponentInParent<HiperAtack>();
    }
    private void Update()
    {
        if (moveUp)
        {
            rb.velocity = Vector2.up * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnCol)
        {
            Destroy(gameObject.transform.parent.gameObject, destroyOnColTimer);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyOnLeave && collision.CompareTag("water"))
        {
            
        }
    }
    public void FadeEnd()
    {
        gameManager.fading = false;
    }
    public void ActivateGameObj(GameObject target)
    {
        target.SetActive(true);
    }
    public void AutoDestruirObj(float tempo)
    {
        Destroy(gameObject, tempo);
    }
    public void DesativaObj()
    {
        gameObject.SetActive(false);
    }
    public void DestroyParent()
    {
        
        Destroy(transform.parent.gameObject);
    }
    public void DesativaBoxCollider()
    {
        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
        box.enabled = false;
    }
    public void AtivaBoxCollider()
    {

        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
        box.enabled = true;
    }
    public void SafeRoomCall()
    {
        gameManager.SafeRoom();
        ResetPlayerPause();
        Animator anim = GetComponent<Animator>();
        
    }
    public void CloseDoor()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("open", false);
    }
    public void CallFade()
    {
        fades.SetTrigger("Fade1");
    }
    public void ResetPlayerPause()
    {
        player.ispaused = false;
    }
    public void PlayerFreeze()
    {
        player.canmove = true;
        
    }
    public void PlayerUnFreeze()
    {
        player.canmove = false;
    }
    public void BossFightStart()
    {
        Debug.Log("Boss");
        player.ispaused = false;
    }
    public void BossfightEnter()
    {
        player.ispaused = false;
    }
    public void GMEnablePlayer()
    {
        gameManager.EnablePlayer();
    }
    public void DestroyOnExitLayer(LayerMask layer)
    {

    }
    public void DesativaCircleCollider()
    {
        CircleCollider2D box = gameObject.GetComponent<CircleCollider2D>();
        box.enabled = false;
    }
    public void AtivaCircleCollider()
    {

        CircleCollider2D box = gameObject.GetComponent<CircleCollider2D>();
        box.enabled = true;
    }
    /*public void ExitGigagaAtack()
    {
        hiper.hiperAtacking = false;
        player.ispaused = false;
        hiper.canMoveup = true;
        player.jumped = false;
        
        player.rb.gravityScale = player.gravity;
        
    }
    public void GigaAtackStopMovingUp()
    {
        hiper.canMoveup = false;
    }*/
}
