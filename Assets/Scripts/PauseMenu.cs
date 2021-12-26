using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public GameObject ui;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            Toggle();
        }
    }

    public void Toggle(){
        ui.SetActive(!ui.activeSelf); //true if GO is enabled 

        if(ui.activeSelf){
            Time.timeScale = 0f; //freeze game time
        }else{
            Time.timeScale = 1f; //back to game time
        }
    }

    public void Retry(){    //TODO: not working properly
        Toggle(); //to restart time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart scene

    }

    public void QuitGame(){
        Debug.Log("Quit Game!");
        Application.Quit();
    }


}
