using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private float Xbound = 0.1f;
    [SerializeField]
    private float Ybound = 0.2f;
    [SerializeField]
    public float leftLimit, rightLimit, bottomLimit, topLimit;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Train")
        {
            leftLimit = -3.1f;
            rightLimit = 6.74f;
            topLimit = 5;
            bottomLimit = 1.18f;
        }
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //Create vector value that will be used to move the camera
        Vector2 moveDirection = Vector2.zero;

        //Calculate the offset of the player according to the bounds
        float deltaX = Player.position.x - transform.position.x;
        float deltaY = Player.position.y - transform.position.y;

        //X-Axis
        if (deltaX > Xbound || deltaX < -Xbound)
        {
            //The player is on the right side
            if (Player.position.x > transform.position.x)
            {
                moveDirection.x = deltaX - Xbound;
            }
            //The player is on the left side
            else
            {
                moveDirection.x = deltaX + Xbound;
            }
        }

        //Y-Axis
        if (deltaY > Ybound || deltaY < -Ybound)
        {
            //The player is on the upper side
            if (Player.position.y > transform.position.y)
            {
                moveDirection.y = deltaY - Ybound;
            }
            //The player is on the lower side
            else
            {
                moveDirection.y = deltaY + Ybound;
            }
        }

        //Apply the move vector to the camera position
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0);
        transform.position = new Vector3
           (
           Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
           Mathf.Clamp(transform.position.y, bottomLimit, topLimit), -10);
       
    
    }
}
