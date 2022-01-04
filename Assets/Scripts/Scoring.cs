using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    
    public static int computeScore(int time, int objects)
    {
        var timeScore = 0;
        var timeDiff = GameManager.maxPlayTime - time;
        if (timeDiff > 0)
        {
            timeScore = timeDiff * 30;
        }

        var objectScore = 180 * objects;
        
        var glowStickScore = PlayerScript.glowStickCount * 220;
        
        //if party not found (arrested or time's up)
        if (!PlayerScript.partyFound)
        {
            return timeScore/10;
        }
        else
        {
            return timeScore + objectScore + glowStickScore;
        }

    }
}
