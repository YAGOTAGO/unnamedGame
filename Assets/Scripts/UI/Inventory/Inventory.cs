using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //gameObject will be the slot, itemObject will be the item

    //If addding more slots increase the number here
    private readonly int numOfSlots= 6;
    public GameObject[] inventoryArr;


    private void Start()
    {
        //initializes with right number of slots
        inventoryArr = new GameObject[numOfSlots];
        for(int i=0; i< numOfSlots; i++)
        {
            inventoryArr[i] = gameObject.transform.GetChild(i).gameObject;
        }

    }


    public void AddItem(GameObject g)
    {

        for (int i=0; i<inventoryArr.Length; i++)
        {
            //child of slot is not 0
            if (inventoryArr[i].transform.childCount == 0)
            {
                g.transform.SetParent(inventoryArr[i].transform);
                g.transform.position  = inventoryArr[i].transform.position;

                //Debug.Log("Object " + g + "added to " + inventoryArr[i]);
                return;
            }
            else if(i == numOfSlots-1)
            {
                Debug.LogError("Inventory full, no slots available");
            }
        }
    }

   
}


