using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteract : MonoBehaviour
{
    [SerializeField] private bool playerDetected;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    public Dialogue dialogueScript;
    private KeyCode interactKey = KeyCode.Mouse0;


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
            playerDetected = false;
        }
    }

    private void OnMouseOver()
    {
        
        if (playerDetected)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

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
