using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public GameObject NormalCursor;
    public GameObject MagnifyingGlass;
    private void OnMouseEnter()
    {
        NormalCursor.SetActive(false);
        MagnifyingGlass.SetActive(true);
    }

    private void OnMouseExit()
    {
        MagnifyingGlass.SetActive(false);
        NormalCursor.SetActive(true);
    }



}
