using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //game over 
    public static bool GameIsOver; //static to access it from outside.  = false, would equal to true, if the scenes gets started again
    public GameObject gameOverUI;

    void Start(){
        GameIsOver = false; //to make it false each scene
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //exit if game ended
        if(GameIsOver){
            return;
        }
        
        if(EnemyScript.playerCaught){
            EndGame();
        }
        

    }


    void EndGame(){
        GameIsOver = true;
        gameOverUI.SetActive(true);
        print("End Game!");
    }
    
    
    
    


}
