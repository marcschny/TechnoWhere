using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)
//from: https://unitycoder.com/blog/2015/12/03/ui-text-typewriter-effect-script/
public class UITextTypeWriter : MonoBehaviour 
{

    public TMP_Text txt;
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
            // TODO: add optional delay when to start
            StartCoroutine ("PlayText");
            StartedTyping = true;
        }
    }

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