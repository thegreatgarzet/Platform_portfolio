using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDialogueTrigger : MonoBehaviour
{
    DialogueTrigger dialogue;
    public bool onlyOnce, triggered;
    private void Awake()
    {
        dialogue = GetComponent<DialogueTrigger>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (onlyOnce && !triggered)
            {
                dialogue.TriggerDialogue();
                triggered = true;
            }
          
            
        }
    }
}
