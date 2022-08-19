using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region LineSettings
    //Material is required to change line color
    public Material lineMaterial;
    [SerializeField]
    private float lineSeperationDistance = .2f;
    [SerializeField]
    private float lineWidth = .1f;
    [SerializeField]
    private Color lineColor = Color.black;
    [SerializeField]
    private int lineCapVertices = 5;
    [SerializeField]
    LayerMask lineLayer;
    #endregion

    #region Cursor
    [SerializeField] private Texture2D cursorErase;
    [SerializeField] private Texture2D cursorDraw;
    [SerializeField] private Texture2D cursorDefault;
    private CursorMode cursorMode = CursorMode.Auto;
     private Vector2 hotSpot = Vector2.zero;
    #endregion

    #region Private
    //line layer needs to match layer number
    //private List<GameObject> lines;
    private List<Vector2> currentLine;
    private LineRenderer currentLineRenderer;
    private EdgeCollider2D currentLineEdgeCollider;
    private WorkspaceEnable workspace;
    private Camera mainCamera;
    private bool pointerIsInsideWorkspace = false;
    private readonly string lineName = "Line";
    #endregion

    //Public because needed for references
    public bool drawing = false;
    public bool erasing = false;

    private void Awake()
    {
        workspace = GetComponent<WorkspaceEnable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        this.enabled = false;
        
    }

    public void activateDraw()
    {
        if (!this.enabled && !workspace.workspaceUp)
        {
            Debug.Log("Workspace must be up");
            return;
        }

        if (!this.enabled && workspace.workspaceUp)
        {
            this.enabled = true;
            return;
        }
       

           this.enabled = false;
        
       
    }


    private void OnDisable()
    {
        erasing = false;
        drawing = false;
        SetCursorToDefault();
    }

    // Update is called once per frame
    void Update()
    {
           
            if (Input.GetMouseButtonDown(0) && pointerIsInsideWorkspace)
            {
                if (!erasing)
                {
                SetCursorToDraw();
                StartCoroutine(Drawing());
                return;
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (!erasing)
                {
                SetCursorToDefault();
                }
                drawing = false;
                
            }

            if (Input.GetMouseButtonDown(1) && pointerIsInsideWorkspace)
            {
                if (!drawing)
                {
                    Cursor.SetCursor(cursorErase, hotSpot, cursorMode);
                    StartCoroutine(Erasing());
                }

            }
            if (Input.GetMouseButtonUp(1))
            {
                if (!drawing)
                {
                SetCursorToDefault();
            }
                erasing = false;
            }

    }

    IEnumerator Drawing()
    {
        drawing = true;
        StartLine();
        while (drawing)
        {
            AddPoint(GetCurrentWorldPoint());
            yield return null;
        }
        EndLine();
    }

    private void StartLine()
    {
        //line
        currentLine = new List<Vector2>();
        GameObject currentLineObject = new GameObject();
        currentLineObject.name = lineName;
        currentLineObject.transform.SetParent(transform);
        currentLineRenderer = currentLineObject.AddComponent<LineRenderer>();
        currentLineEdgeCollider = currentLineObject.AddComponent<EdgeCollider2D>();

        //settings
        currentLineRenderer.positionCount = 0;
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.numCapVertices = lineCapVertices;
        currentLineRenderer.material = lineMaterial;
        currentLineRenderer.startColor = lineColor;
        currentLineRenderer.endColor = lineColor;
        currentLineEdgeCollider.edgeRadius = .1f;
        currentLineRenderer.useWorldSpace = false;

        //this is where we define layer of lines
        currentLineObject.layer = LayerMask.NameToLayer(lineName);


    }

    private void AddPoint(Vector2 point)
    {
        if (PlacePoint(point))
        {
            currentLine.Add(point);
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, point);
        }
    }

    private bool PlacePoint(Vector2 point)
    {
        if (currentLine.Count == 0) return true;
        if (Vector2.Distance(point, currentLine[currentLine.Count - 1]) < lineSeperationDistance)
            return false;
        return true;
    }
    private Vector2 GetCurrentWorldPoint()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void EndLine()
    {
        if(currentLineEdgeCollider != null)
            currentLineEdgeCollider.SetPoints(currentLine);
    }



    IEnumerator Erasing()
    {
        erasing = true;
        while (erasing)
        {
            Vector2 screenMousePosition = GetCurrentScreenPoint();
            GameObject g = Utils.Raycast(mainCamera, screenMousePosition, lineLayer);
            if (g != null) DestroyLine(g);
            yield return null;

        }
    }

    private void DestroyLine(GameObject g)
    {
        Destroy(g);
    }
    private Vector2 GetCurrentScreenPoint()
    {
        return Input.mousePosition;
    }

    public void ClearAllLines()
    {
        if(transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if(child.name == lineName)
                    GameObject.Destroy(child.gameObject);
            }
                
            
        }
       
    }

    private void SetCursorToDefault()
    {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
    }

    private void SetCursorToDraw()
    {
        Cursor.SetCursor(cursorDraw, hotSpot, cursorMode);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       pointerIsInsideWorkspace = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.enabled = false;
        pointerIsInsideWorkspace= false;
    }
}
