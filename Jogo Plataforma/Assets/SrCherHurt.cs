using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrCherHurt : MonoBehaviour
{
    DialogueTrigger dialogue;
    GameManager gameManager;
    Animator anim;
    public GameObject SrCher;
    public bool player;
    private void Awake()
    {
        dialogue = GetComponent<DialogueTrigger>();
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(player && !gameManager.onTyping)
        {
            anim.SetTrigger("teleport");
            ActivateSrCher(SrCher);
        }
    }
    public void ActivateSrCher(GameObject srcher)
    {
        srcher.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = true;
            dialogue.TriggerDialogue();
        }
    }
}
