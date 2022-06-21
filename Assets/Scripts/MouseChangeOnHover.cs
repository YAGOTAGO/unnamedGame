using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChangeOnHover : MonoBehaviour
{
    [SerializeField] private bool playerDetected;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;


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
