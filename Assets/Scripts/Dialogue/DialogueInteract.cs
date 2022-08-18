using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    private bool playerDetected;
    [SerializeField] private Texture2D cursorHover;
    private readonly CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    public Dialogue dialogueScript;
    private readonly KeyCode interactKey = KeyCode.Mouse0;


    /**
     * If wanna make it so when click outside dialogue doesnt trigger, look into adding a bool that is set on mouse enter and exit
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            dialogueScript.EndDialogue();
            playerDetected = false;
        }
    }

    private void OnMouseOver()
    {
        
        if (playerDetected)
        {
            Cursor.SetCursor(cursorHover, hotSpot, cursorMode);

            if (Input.GetKeyDown(interactKey)){
                dialogueScript.StartDialogue();
                //playerDetected = false;
            }
        }
        else if (!playerDetected)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

}
