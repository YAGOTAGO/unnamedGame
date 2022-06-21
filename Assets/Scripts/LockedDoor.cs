using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public bool playerDetected;
    public GameObject exclamation;

    private void Awake()
    {
        exclamation.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            exclamation.SetActive(true);
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            exclamation.SetActive(false);
            playerDetected = false;
        }
    }

    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            //Do thing when interacting with door
            Debug.Log("Door was clicked");
        }
    }
}
