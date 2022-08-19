
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Item))]
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    #region CursorSettings
    [SerializeField] private Texture2D grabCursor;
    [SerializeField] private Texture2D defaultCursor;
    #endregion

    #region Tags
    private readonly string slotTag = "Slot";
    private readonly string workspaceTag = "Workspace";
    #endregion

    #region Item
    private Item item;
    private Image itemImage;

    [SerializeField]
    private float iconWidth = 93;
    [SerializeField]
    private float iconHeight = 85;
    #endregion

    #region Prior Values
    private Sprite previousSprite;
    private Transform priorParent;
    private Vector2 priorSize;
    private Vector2 startPosition;
    #endregion

    //Range of circle2D collider
    [SerializeField] private int range = 26;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    //maybe set this value in code
    [SerializeField] private Canvas canvas;
    private Transform workspaceTransform;
    
   

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        item = GetComponent<Item>();
        itemImage = GetComponent<Image>();
        workspaceTransform = GameObject.FindGameObjectWithTag(workspaceTag).transform;

        //disables the scrip until the item is picked up
        this.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        //Hold previous parent
        priorParent = transform.parent;
        
        //Hold preious sprite
        previousSprite = GetComponent<Image>().sprite;

        //Holds previous size
        priorSize = new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x, transform.GetComponent<RectTransform>().sizeDelta.y);

        //set image to icon
        itemImage.sprite = item.GetIcon();

        //set image size to icon
        GetComponent<RectTransform>().sizeDelta = new Vector2(iconWidth, iconHeight);

        //cursor changed to the grab cursor
        Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);

        //remember start position
        startPosition = transform.position;
        
        //Canvas variables change so drag is posible
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        //Makes so draggable shows over the inventory
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Sets default cursor
        SetCursorToDefault();

        //Canvas settings needed for dragging
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        //Check hit colliders so we know what to do
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        
        if(hitColliders != null)
        {
            foreach (Collider2D hit in hitColliders)
            {
                
                //If slot is hit and open
                if (hit.gameObject.CompareTag(slotTag) && hit.gameObject.transform.childCount == 0)
                {
                    //Sets image back to icon
                    itemImage.sprite = item.GetIcon();

                    //Sets parent and position
                    transform.SetParent(hit.gameObject.transform);
                    transform.position = hit.transform.position;

                    return;
                }

                //If we drop the item into the workspace
                if (hit.gameObject.CompareTag(workspaceTag))
                {
                    //Set to full image
                    itemImage.sprite = item.GetFullItem();

                    //Make image large
                    GetComponent<RectTransform>().sizeDelta = new Vector2(item.GetWidth(), item.GetHeight());

                    //Set the position to where the cursor is
                    transform.position = eventData.pointerDrag.transform.position;

                    //make workspace the parent
                    transform.SetParent(workspaceTransform);

                
                    return;
                }
            }

                //If dragged to non acceptable region set sprite and position to the prior
                itemImage.sprite = previousSprite;
                transform.position = startPosition;

                //Sets prior size
                GetComponent<RectTransform>().sizeDelta = priorSize;

                //Set to original parent
                transform.SetParent(priorParent);
        }
       

    }
   
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }
  
    private void SetCursorToDefault()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
