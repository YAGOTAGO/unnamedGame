using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 mainPlayerPosition;
    public long lastUpdated;


    //This is the constructor that defines the data for a new game
    public GameData()
    {
        mainPlayerPosition = new Vector3(-22f, -10.3f, 0);
    }
}