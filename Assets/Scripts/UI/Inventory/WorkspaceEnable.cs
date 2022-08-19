using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class WorkspaceEnable : MonoBehaviour
{
    public bool workspaceUp = false;
    private BoxCollider2D boxCollider;
    private Image image;
    //time in seconds
    [SerializeField] private float time = 1f;
    [SerializeField] private iTween.EaseType easeType;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private string lineName = "Line";
    private Draw draw;

    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        image = gameObject.GetComponent<Image>();
        image.enabled = false;
        boxCollider.enabled = false;
        draw = GetComponent<Draw>();

    }

    // Update is called once per frame
    

    public void WorkspaceButton()
    {
        
        if (!workspaceUp)
        {
            TweenUp();
            Enable();
            return;
        }

        if (workspaceUp)
        {

            TweenDown();
            
        }
 
    }

    public void TweenDown()
    {

        //disables any drawing ability
        draw.enabled = false;

        //updates workspace state
        workspaceUp = false;


        //Disables the lines 
        foreach (Transform child in transform)
        {
            if (child.name == lineName)
                child.gameObject.SetActive(false);
        }

        boxCollider.enabled = false;
        iTween.MoveTo(gameObject, iTween.Hash("position", startPosition, "time", time, "easetype", easeType, "oncomplete", "Disable", "islocal", true));
    }
    public void TweenUp()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", destination, "time", time, "easetype", easeType, "islocal", true));
    }

    private void Enable()
    {
        boxCollider.enabled = true;
        image.enabled = true;
        workspaceUp = true;
        foreach (Transform child in transform)
                child.gameObject.SetActive(true);
        
    }

    //called at the end of TweenDown
    private void Disable()
    {
        //Disables the workspace collider and image
        image.enabled = false;

    }
}
