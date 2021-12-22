using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //start game
    public void PlayGame(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }


    public void QuitGame(){
        Debug.Log("Quit Game!");
        Application.Quit();
    }

}
