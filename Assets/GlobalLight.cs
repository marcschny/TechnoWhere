using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    
    private UnityEngine.Rendering.Universal.Light2D globalLight;

    private double startIntensity = 0.16;
    private double maxIntensity = 0.40;
    private double intensityDiff;

    private Color startColor;
    private Color endColor;



    void Start()
    {
        intensityDiff = maxIntensity - startIntensity;
        globalLight = gameObject.GetComponent<Light2D>();
        startColor = new Color(0.6f,0.6f,0.6f,1.0f);
        endColor = new Color(1,0.6f,0.6f,1);
        //Debug.Log(globalLight.color);
        globalLight.color = startColor;
    }

    void FixedUpdate()
    {
        if (GameManager.timePlayed > GameManager.maxPlayTime*0.66 && GameManager.timePlayed <= GameManager.maxPlayTime && globalLight.intensity <= maxIntensity)
        {
            globalLight.intensity = (float)(startIntensity+((intensityDiff/GameManager.maxPlayTime*0.33)*GameManager.timePlayed));
            //Debug.Log("intensity: "+globalLight.intensity.ToString());
            globalLight.color = Color.Lerp(startColor, endColor, (float)(GameManager.timePlayed/GameManager.maxPlayTime*0.33));
            //Debug.Log(globalLight.color.ToString());
        }

    }
}
