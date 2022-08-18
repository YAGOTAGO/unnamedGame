
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
    private int iconWidth = 93;
    [SerializeField]
    private int iconHeight = 85;
    #endregion

    
    //Range of circle2D collider
    [SerializeField] private int range = 26;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 startPosition;
    private Transform workspaceTransform;
    
    private bool inSlot = false;

    

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

        //Setting the parent to canvas so won't move with inventory gameObject
        if(!inSlot)
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
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

        //Canvas settings needed for dragging
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        //Check hit colliders so we know what to do
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        
        if(hitColliders != null)
        {
            foreach (Collider2D hit in hitColliders)
            {
                Debug.Log(hit);
                //If slot is hit and open
                if (hit.gameObject.CompareTag(slotTag) && hit.gameObject.transform.childCount == 0)
                {
                    //Sets image back to icon
                    itemImage.sprite = item.GetIcon();

                    //Sets parent and position
                    transform.SetParent(hit.gameObject.transform);
                    transform.position = hit.transform.position;

                    inSlot = true;
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

                    inSlot = false;
                    return;
                }
            }

            
                transform.position = startPosition;
            
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
  
}
