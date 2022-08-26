
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(iTweenPath))]
public class InventoryItemPickup : MonoBehaviour
{

    #region CursorSettings
    [SerializeField] private Texture2D cursorHover;
    private readonly CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    #endregion

    #region iTween
    [SerializeField] private string mapPath;
    [SerializeField] private float pathTime;
    [SerializeField] private iTween.EaseType easeType;
    #endregion

    #region ReadOnly
    private readonly KeyCode interactKey = KeyCode.Mouse0;
    private readonly string playerTag = "Player";
    private readonly string inventoryName = "Inventory";
    #endregion

    #region Private
    [SerializeField] private bool playerDetected = false;
    [SerializeField] private GameObject UIImage;
    private Inventory inventory;
    #endregion

    private void Awake()
    {
        inventory = GameObject.Find(inventoryName).GetComponent<Inventory>();
        UIImage.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (!this.enabled)
            return;

        Cursor.SetCursor(cursorHover, hotSpot, cursorMode);
        if (Input.GetKeyDown(interactKey) && playerDetected)
        {
            //set off an animation
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(mapPath), "time", pathTime, "easetype", easeType, "oncomplete", "addInventory"));

        }
    }


    private void OnMouseExit()
    {
        if(this.enabled)
            SetCursorToDefault();
    }

    //This is called right after iTween
    private void addInventory()
    {

        UIImage.SetActive(true);

        // transform item position to an empty slot
        inventory.AddItem(UIImage);

        //disable this script
        this.enabled = false;
        this.gameObject.SetActive(false);
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

    private void OnDisable()
    {
        SetCursorToDefault();
    }
    
    private void SetCursorToDefault()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}