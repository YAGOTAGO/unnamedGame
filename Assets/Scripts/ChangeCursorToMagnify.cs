using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorToMagnify : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    private bool playerDetected = false;

    /**
     * Purpose is to change the mouse look when an object can be clicked
     * The onTriggered are to check if player is in correct area
     * The on hover is to check that mouse is in correct area
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

    void OnMouseOver()
    {
        
        if (playerDetected)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
           
        }else if (!playerDetected)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
            
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

}
