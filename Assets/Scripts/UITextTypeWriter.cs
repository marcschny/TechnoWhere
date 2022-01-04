using UnityEngine;
using System.Collections;
using TMPro;


public class UITextTypeWriter : MonoBehaviour 
{

    public TMP_Text txt;  //UI Text component (with the full text already there)
    string story;

    private bool startedTyping;
    
    private bool StartedTyping{
        get{return startedTyping;}
        set{
            if(value == startedTyping){
                return;
            }else{
                startedTyping = value;
            }
        }

    }

    void Awake ()
    {
        StartedTyping = false;
        story = txt.text;
        txt.text = "";

    }

    void Update()
    {
        if (txt.isActiveAndEnabled)
        {
            //TODO: add optional delay when to start
            StartCoroutine ("PlayText");
            StartedTyping = true;
        }
    }

    //text typing animation
    IEnumerator PlayText()
    {
        if (!startedTyping)
        {
            foreach (char c in story) 
            {
                txt.text += c;
                yield return new WaitForSeconds (0.045f);
            }
        }

    }

}