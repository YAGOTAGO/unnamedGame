using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    //Trigger Key
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    //Window
    public GameObject window;

    //Indicator
    //public GameObject indicator;

    //Dialogue list
    public List<string> dialogues;

    //Writing speed
    public float writingSpeed;
    
    //Index on dialogue
    private int index;

    //Character index
    private int charIndex;

    //Started boolean
    private bool started;

    //text component
    public TMP_Text DialogueText;

    //wait for next input
    private bool waitForNext;

    private void Awake()
    {
        //ToggleIndicator(false);
        ToggleWindow(false);
    }

    public void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator (bool show)
    {
        //indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if (started) 
            return;
        
        //Boolean to indicate we have started
        started = true;
        //show the window
        ToggleWindow(true);
        //hide the indicator
        //ToggleIndicator(false);

        GetDialogue(0);

    }

    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;
        charIndex = 0;
        //clear the dialogye component text
        DialogueText.text = string.Empty;
        //start writing
        StartCoroutine(Writing());
    }
    //End Dialogue
    public void EndDialogue()
    {
        started = false;
        waitForNext = false;
        StopAllCoroutines();
        //Hide the Window
        ToggleWindow(false);
        
    }
   
    //Writing logic

    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        //Write the character
        DialogueText.text += currentDialogue[charIndex];

        //increase the character index
        charIndex++;

        //Make sure you have reached end of sentence
        if(charIndex < currentDialogue.Length)
        {
            //Wait x seconds
            yield return new WaitForSeconds(writingSpeed);
            //Restart the process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for next one
            waitForNext = true;
           
        }

}
    private void Update()
    {
        if (!started)
            return;

        if(waitForNext && Input.GetKeyDown(interactKey))
        {

            waitForNext=false;
            index++;

            if(index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                //End dialogue
                EndDialogue();
            }
            
        }
    }
}
