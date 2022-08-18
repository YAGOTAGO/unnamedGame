
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    [SerializeField] private int range = 26;
    private bool inSlot = false;
    private readonly string slotTag = "Slot";
   
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        //disables the scrip until the item is picked up
        this.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        
        startPosition = transform.position;
        //Debug.Log(startPosition);

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;


        //Setting the parent to canvas so won't move with inventory gameObject
        if(!inSlot)
            transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;   
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("On End Drag + " + eventData.pointerDrag);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        
        if(hitColliders != null)
        {
            foreach (Collider2D hit in hitColliders)
            {
                
                if (hit.gameObject.CompareTag(slotTag) && hit.gameObject.transform.childCount<1)
                {
                    //Sets parent and position
                    transform.SetParent(hit.gameObject.transform);
                    transform.position = hit.transform.position;

                    inSlot = true;
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
