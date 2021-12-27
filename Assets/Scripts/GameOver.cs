using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //set maximum time in seconds (every played time below this gets scored)
    private int maxTime = 180;
    
    //game over texts
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text objectsText; 
    
    void OnEnable(){ //gets called everytime a GO gets enabled (not like Start())
        var totalTime = Mathf.RoundToInt((float)GameManager.timePlayed);
        var minutes = Mathf.Floor((float) totalTime / 60);
        var seconds = totalTime % 60;
        timeText.text = "Time: " + minutes.ToString()+"m " + seconds.ToString()+"s";

        var objectsCollected = PlayerScript.collectables;
        objectsText.text = "Objects: " + objectsCollected.ToString();
        
        var score = computeScore(totalTime, objectsCollected);
        scoreText.text = "Score: " + score.ToString();
    }

    private int computeScore(int time, int objects)
    {
        var timeScore = 0;
        var timeDiff = maxTime - time;
        if (timeDiff > 0)
        {
            timeScore = timeDiff * 30;
        }

        var objectScore = 180 * objects;
        
        //party found big bonus
        var partyFoundBonus = 0;
        if (PlayerScript.partyFound)
        {
            partyFoundBonus = 5000;
        }

        return timeScore + objectScore + partyFoundBonus;
    }


    public void RetryGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart scene
        Debug.Log("Retry Game!");
    }

    public void QuitGame(){
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    
    
}
