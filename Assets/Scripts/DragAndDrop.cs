using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;


        //Setting the parent to canvas so won't move with inventory gameObject
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
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Data: " + eventData.pointerDrag);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
    }

   
}
