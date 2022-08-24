
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LockScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI UIText;
    private string textContent = "";
    [SerializeField] private string code;
    private int codeLength = 4;

    private void Awake()
    {
        UIText = GetComponent<TextMeshProUGUI>();   
    }
   

    // Update is called once per frame
    void Update()
    {


        UIText.text = textContent;

        if(UIText.text == code)
        {
            Debug.Log("Code is correct, action would occur");

            this.enabled = false;
        }

        if(UIText.text.Length >= codeLength)
        {
            textContent = "";
        }

        
    }


    public void addDigit(string digit)
    {
        textContent += digit;
    }
}
