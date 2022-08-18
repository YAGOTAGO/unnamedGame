using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEnable : MonoBehaviour
{

    
    [SerializeField]
    private int speed = 150;
    private bool inventoryUp = false;
    private bool buttonClicked= false;


    //NOTE: values of how high or low inventory goes are hard coded!!! this will give potential errors in future


    private void Update()
    {
        if (buttonClicked)
        {
            if (!inventoryUp)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.up);

                //4 is hard coded
                if (transform.position.y > 4)
                {
                    inventoryUp = true;
                    buttonClicked = false;
                }

            }
            else
            {
                transform.Translate(speed * Time.deltaTime * Vector2.down);

                //-70 is hard coded
                if (transform.position.y < -95)
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
