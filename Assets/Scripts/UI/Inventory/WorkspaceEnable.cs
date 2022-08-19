using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class WorkspaceEnable : MonoBehaviour
{
    private bool workspaceUp = false;
    private BoxCollider2D boxCollider;
    private Image image;
    //time in seconds
    [SerializeField] private float time = 1f;
    [SerializeField] private iTween.EaseType easeType;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 startPosition;
    


    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        image = gameObject.GetComponent<Image>();
        image.enabled = false;
        boxCollider.enabled = false;
        
    }

    // Update is called once per frame
    

    public void WorkspaceButton()
    {
        
        if (!workspaceUp)
        {

            TweenUp();
            boxCollider.enabled = true;
            image.enabled = true;
            workspaceUp = true;
            return;

        }

        if (workspaceUp)
        {

            TweenDown();
            workspaceUp = false;

        }

        
    }

    public void TweenDown()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", startPosition, "time", time, "easetype", easeType, "oncomplete", "Disable", "islocal", true));
    }
    public void TweenUp()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", destination, "time", time, "easetype", easeType, "islocal", true));
    }
    private void Disable()
    {
        boxCollider.enabled = false;
        image.enabled = false;
    }
}
