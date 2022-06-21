using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMagnify : mouseCursor
{
    [SerializeField]
    private GameObject cursor;
    private void Start()
    {
        cursor.SetActive(false);
    }
}
