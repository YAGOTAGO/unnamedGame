using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEnable : MonoBehaviour
{

    //This should reference itself
    [SerializeField]
    private GameObject inventoryUI;

    
    public void ShowAndHide()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }else if (!inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(true);
        }

    }
}
