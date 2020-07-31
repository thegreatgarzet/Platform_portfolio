using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool boss;
    public GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void TriggerDialogue()
    {
        if (gameManager.canCutscene)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, boss);
        }
    }
}
