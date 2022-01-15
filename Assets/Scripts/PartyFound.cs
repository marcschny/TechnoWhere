using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PartyFound : MonoBehaviour
{
    
    //game over texts
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text objectsText; 
    
    void OnEnable(){
        

        var totalTime = Mathf.RoundToInt((float)GameManager.timePlayed);
        var minutes = Mathf.Floor((float) totalTime / 60);
        var seconds = totalTime % 60;
        timeText.text = "Time: " + minutes.ToString()+"m " + seconds.ToString()+"s";

        var objectsCollected = PlayerScript.collectables;
        objectsText.text = "Objects: " + objectsCollected.ToString();
        
        var score = Scoring.computeScore(totalTime, objectsCollected);
        scoreText.text = "Score: " + score.ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2"); //start level 2
        Debug.Log("next level");
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