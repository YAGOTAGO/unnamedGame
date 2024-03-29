
using UnityEngine;
using UnityEngine.EventSystems;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(DragAndDrop))]
[RequireComponent(typeof(iTweenPath))]
public class ItemPickup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    private Inventory inventory;
    private bool mouseOver = false;
    #endregion

    private void Awake()
    {
        inventory = GameObject.Find(inventoryName).GetComponent<Inventory>();

    }

    private void Update()
    {
        if (mouseOver)
        {
            if (Input.GetKeyDown(interactKey))
            {
                //set off an animation
                iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(mapPath), "time", pathTime, "easetype", easeType, "oncomplete", "addIntoInventory"));

            }
        }
    }

    //This is called right after iTween
    private void addIntoInventory()
    {
        //enable drag script
        GetComponent<DragAndDrop>().enabled = true;

        // transform item position to an empty slot
        inventory.AddItem(gameObject);

        //disable this script
        this.enabled = false;
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
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}