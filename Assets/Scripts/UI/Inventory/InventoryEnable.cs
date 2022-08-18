using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEnable : MonoBehaviour
{

    
    [SerializeField]
    private int speed = 150;
    private bool inventoryUp = false;
    private bool buttonClicked= false;
    [SerializeField] private int topBound;
    [SerializeField] private int botBound;


    //NOTE: values of how high or low inventory goes are hard coded!!! this will give potential errors in future


    private void Update()
    {
        if (buttonClicked)
        {
            if (!inventoryUp)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.up);

                //yBound is hard coded
                if (transform.localPosition.y > topBound)
                {
                    
                    inventoryUp = true;
                    buttonClicked = false;
                }

            }
            else
            {
                transform.Translate(speed * Time.deltaTime * Vector2.down);

                //xBound is hard coded
                if (transform.localPosition.y < botBound)
                {
                    buttonClicked = false;
                    inventoryUp = false;
                }

            }
        }
        
       
    }


    public void SlideUp()
    {
        buttonClicked = true;
      
    }



}
