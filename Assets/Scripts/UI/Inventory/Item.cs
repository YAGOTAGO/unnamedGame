using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemPickup))]
public class Item : MonoBehaviour
{
    //Holds all information relevant to an item
    [SerializeField]private Sprite icon;
    [SerializeField] private Sprite fullItem;
    [SerializeField] private int widthOfFull;
    [SerializeField] private int heightOfFull;


    public int GetWidth()
    {
        return widthOfFull;
    }
    public int GetHeight()
    {
       return heightOfFull;
    }
    public Sprite GetFullItem()
    {
        return fullItem;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
}
