using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is for creating the scriptable object

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class ItemObject : ScriptableObject
{
    
    public Sprite icon;
    public Sprite fullItem;
    
   

}
