using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    private AudioLowPassFilter lowPassFilter;

    private GameObject player;

    private float distance;

    private float invLerp;
    
    // Start is called before the first frame update
    void Start()
    {
        lowPassFilter = transform.GetComponent<AudioLowPassFilter>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);

        invLerp = Mathf.InverseLerp(50, 10, distance);
        lowPassFilter.cutoffFrequency = Mathf.Lerp(128, 5000, invLerp);

        lowPassFilter.enabled = !(distance <= 10);


    }
}
