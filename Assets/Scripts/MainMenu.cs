using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject intro1;
    public GameObject intro2;
    public GameObject playButton;


    private void Start()
    {
        intro1.SetActive(false);
        intro2.SetActive(false);
        playButton.SetActive(false);

    }

    //play game
    public void PlayGame(){
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        intro1.SetActive(true);
        yield return new WaitForSeconds(15);
        intro1.SetActive(false);
        yield return new WaitForSeconds(1);
        intro2.SetActive(true);
        yield return new WaitForSeconds(8);
        playButton.SetActive(true);
    }

    public void LoadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame(){
        Debug.Log("Quit Game!");
        Application.Quit();
    }

}
