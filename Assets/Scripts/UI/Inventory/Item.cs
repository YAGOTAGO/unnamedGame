using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemPickup))]
public class Item : MonoBehaviour
{ 
    public Sprite icon;
    public Sprite fullItem;
    public int widthOfFull;
    public int heightOfFull;


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
