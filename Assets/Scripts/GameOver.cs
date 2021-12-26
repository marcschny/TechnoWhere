using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //public Text roundsText;

    //void OnEnable(){ //gets called everytime a GO gets enabled (not like Start())
    //    roundsText.text = PlayerStats.Rounds.ToString();
    //}
    

    public void RetryGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart scene
        Debug.Log("Retry Game!");
    }

    public void QuitGame(){
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    
    
}
