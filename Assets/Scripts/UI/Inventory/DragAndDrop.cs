
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

    #region iTween
    private readonly float time = .85f;
    private readonly iTween.EaseType easeType = iTween.EaseType.easeOutQuint;
    #endregion

    //Range of circle2D collider
    [SerializeField] private float range = 26;

    //rectTransform needed to change size and position on screen
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    //maybe set this value in code
    [SerializeField] private Canvas canvas;
    private Transform workspaceTransform;
    
    //reference to draw script
    private Draw draw;
   

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        item = GetComponent<Item>();
        itemImage = GetComponent<Image>();
        workspaceTransform = GameObject.FindGameObjectWithTag(workspaceTag).transform;
        draw = GameObject.FindGameObjectWithTag(workspaceTag).GetComponent<Draw>();
        //disables the scrip until the item is picked up
        this.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draw.drawing || draw.erasing)
            return;

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
        if (draw.drawing || draw.erasing)
            return;

        Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draw.drawing || draw.erasing)
            return;

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

                //swap item function
                if(hit.gameObject.CompareTag(slotTag) && hit.gameObject.transform.childCount == 1 && !priorParent.gameObject.CompareTag(workspaceTag))
                {

                    //move hit game obect to our game objects position
                    iTween.MoveTo(hit.gameObject.transform.GetChild(0).gameObject, iTween.Hash("position", priorParent.position, "time", time, "easetype", easeType));
                    //Change parent
                    hit.gameObject.transform.GetChild(0).SetParent(priorParent);


                    //Change our gameObjects parent to the hit one
                    transform.SetParent(hit.gameObject.transform);

                    //Move our game object to the hits position
                    transform.position = hit.transform.position;

                    return;
                }
            }


                //BELOW is the else, if none of the cases above then this will occur

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
