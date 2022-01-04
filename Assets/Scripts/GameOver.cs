using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    
    //game over texts
    public TMP_Text gameOverText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text objectsText; 
    
    void OnEnable(){ //gets called everytime a GO gets enabled (not like Start())
        if (EnemyScript.playerCaught)
        {
            gameOverText.text = "You were arrested...";
        }
        else if (GameManager.timePlayed > GameManager.maxPlayTime + 12)
        {
            gameOverText.text = "All ravers are already gone...";
        }
        else
        {
            gameOverText.text = "";
        }

        var totalTime = Mathf.RoundToInt((float)GameManager.timePlayed);
        var minutes = Mathf.Floor((float) totalTime / 60);
        var seconds = totalTime % 60;
        timeText.text = "Time: " + minutes.ToString()+"m " + seconds.ToString()+"s";

        var objectsCollected = PlayerScript.collectables;
        objectsText.text = "Objects: " + objectsCollected.ToString();
        
        var score = Scoring.computeScore(totalTime, objectsCollected);
        scoreText.text = "Score: " + score.ToString();
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
