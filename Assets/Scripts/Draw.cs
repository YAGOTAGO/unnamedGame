using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{


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

    #region Private
    //line layer needs to match layer number
    private List<GameObject> lines;
    private List<Vector2> currentLine;
    private LineRenderer currentLineRenderer;
    private EdgeCollider2D currentLineEdgeCollider;

    private bool drawing = false;
    private bool erasing = false;

    private Camera mainCamera;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!erasing)
                StartCoroutine("Drawing");
            Debug.Log("left click");
        }
        if (Input.GetMouseButtonUp(0))
        {
            drawing = false;
            Debug.Log("left click up");
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!drawing)
                StartCoroutine("Erasing");
        }
        if (Input.GetMouseButtonUp(1))
        {
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
        currentLineObject.name = "Line";
        currentLineObject.transform.parent = transform;
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

        //this is where we define layer of lines
        currentLineObject.layer = LayerMask.NameToLayer("Line");


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
}
