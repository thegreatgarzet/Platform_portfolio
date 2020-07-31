using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    public DialogueTrigger[] dialogues;
    DialogueManager dialogeManager;
    public int dialogueNum;
    public Vector2 range;
    public SpriteRenderer playerCloseSprite;
    public bool playerClose;
    public LayerMask playerlayer;
    private void Start()
    {
        dialogeManager = FindObjectOfType<DialogueManager>();
    }
    private void Update()
    {
        playerClose = Physics2D.OverlapBox(transform.position, range, 0, playerlayer);
        if (playerClose)
        {
            if (Input.GetAxisRaw("Vertical") > 0 && !dialogeManager.talking)
            {
                dialogues[dialogueNum].TriggerDialogue();
                dialogueNum++;
                if(dialogueNum > dialogues.Count() - 1)
                {
                    dialogueNum=0;
                }
            }
            playerCloseSprite.enabled = true;
        }
        else
        {
            playerCloseSprite.enabled = false;
        }
        
    }
}
