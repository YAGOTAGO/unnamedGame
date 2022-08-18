using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DragAndDrop))]
public class ItemPickup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Texture2D cursorHover;
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private bool playerDetected = false;
    private readonly CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private readonly KeyCode interactKey = KeyCode.Mouse0;
    private readonly string playerTag = "Player";
    [SerializeField] private Inventory inventory;
    private bool mouseOver = false;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (mouseOver){
            if (Input.GetKeyDown(interactKey))
            {
                Debug.Log("clicked");
                //set off an animation

                //enable drag script
                GetComponent<DragAndDrop>().enabled = true;

                // transform item position to an empty slot
                inventory.AddItem(gameObject);

                //disable this script
                this.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(playerTag))
            playerDetected = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag(playerTag))
            playerDetected = false;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playerDetected)
        {
            Debug.Log("Hovered");
            Cursor.SetCursor(cursorHover, hotSpot, cursorMode);
            mouseOver = true;
        }
    }

    private void OnDisable()
    {
        SetCursorToDefault();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;   
        SetCursorToDefault();
    }

    private void SetCursorToDefault()
    {
        Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
    }
}
