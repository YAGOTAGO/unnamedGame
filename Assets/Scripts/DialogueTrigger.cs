using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerDetected;
    public Dialogue dialogueScript;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if triggered the player enable player detected and shows indicator
        if(collision.CompareTag("Player"))
        {
            playerDetected = true;
            //dialogueScript.ToggleIndicator(playerDetected);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            //dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();   
        }
    }

    //while detected if we interact start dialogue
    private void Update()
    {
        if(playerDetected && Input.GetKeyDown(interactKey))
        {
            dialogueScript.StartDialogue();
        }
    }
}
