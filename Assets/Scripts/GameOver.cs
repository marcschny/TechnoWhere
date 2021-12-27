using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //time played
    public TMP_Text timeText;
    public TMP_Text objectsText; 
    
    void OnEnable(){ //gets called everytime a GO gets enabled (not like Start())
        var totalTime = Mathf.RoundToInt((float)GameManager.timePlayed);
        var minutes = Mathf.Floor((float) totalTime / 60);
        var seconds = totalTime % 60;
        timeText.text = "Time: " + minutes.ToString()+"m " + seconds.ToString()+"s";

        objectsText.text = "Objects: " + PlayerScript.collectables.ToString();
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
