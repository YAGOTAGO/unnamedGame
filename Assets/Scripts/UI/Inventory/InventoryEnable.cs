using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static iTween;

public class InventoryEnable : MonoBehaviour
{

    private bool inventoryUp = false;
    private WorkspaceEnable workspace;

    #region Itween
    [SerializeField] private float time = .6f;
    [SerializeField] private iTween.EaseType easeType;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 startPosition;
    #endregion

    private void Awake()
    {
        //MAKE SURE there is a parent with the workspace enable script
        workspace = gameObject.GetComponentInChildren<WorkspaceEnable>();

    }

    public void SlideUp()
    {
        if(inventoryUp && workspace)
        {
            workspace.TweenDown();
            TweenDown();
            return;

        }

        if (!inventoryUp)
        {
            MoveTo(gameObject, Hash("position", destination, "time", time, "easetype", easeType, "islocal", true));
            inventoryUp = true;
            return;
        }
        if(inventoryUp)
        {
            TweenDown();
        }
        


    }

    private void TweenDown()
    {

        MoveTo(gameObject, Hash("position", startPosition, "time", time, "easetype", easeType, "islocal", true));
        inventoryUp = false;
    }



}
