using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class WorkspaceEnable : MonoBehaviour
{

    private bool buttonPressed = false;
    private bool workspaceUp = false;
    [SerializeField] private int speed;
    [SerializeField] private int topBound;
    [SerializeField] private int botBound;
    private BoxCollider2D boxCollider;
    private Image image;


    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        image = gameObject.GetComponent<Image>();
        image.enabled = false;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            if (!workspaceUp)
            {
                boxCollider.enabled = true;
                image.enabled = true;
                transform.Translate(speed * Time.deltaTime * Vector2.up);

                //yBound is hard coded
                if (transform.localPosition.y > topBound)
                {

                    workspaceUp = true;
                    buttonPressed = false;
                }

            }
            else
            {
                transform.Translate(speed * Time.deltaTime * Vector2.down);

                //xBound is hard coded
                if (transform.localPosition.y < botBound)
                {
                    buttonPressed = false;
                    workspaceUp = false;
                    boxCollider.enabled = false;
                    image.enabled = false;
                }

            }
        }


    }

    public void WorkspaceButton()
    {
        buttonPressed = true;
    }
}
