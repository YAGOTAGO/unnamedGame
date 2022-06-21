using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCupScript : MonoBehaviour
{
    private bool playerDetected;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    //sets the key for interaction
    private KeyCode interactKey = KeyCode.Mouse0;

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
        //if mouse over and click do something
        if (playerDetected && Input.GetKeyDown(interactKey))
        {
            Debug.Log("Tea was clicked");
        }

        //if mouse over change mouse texture
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
