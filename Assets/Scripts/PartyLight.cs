using System.Collections;
using UnityEngine;

public class PartyLight : MonoBehaviour
{
    private GameObject [] lights;
    private bool lightOn;
 
    private void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("PartyLight");
        lightOn = true;
    }
 
    private void Update()
    {
        if (lightOn == true)
        {
            StartCoroutine(TurnLightsOff());
        }
        if (lightOn == false)
        {
            StartCoroutine(TurnLightsOn());
        }
    }
     
    IEnumerator TurnLightsOff()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject go in lights)
        {
            go.SetActive(false);
        }
        lightOn = false;
    }
 
    IEnumerator TurnLightsOn()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject go in lights)
        {
            go.SetActive(true);
        }
        lightOn = true;
    }
}