using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //game over ui
    public static bool GameIsOver; //static to access it from outside.  = false, would equal to true, if the scenes gets started again
    public GameObject gameOverUI;


    //items (OverlayCanvas)
    public GameObject booster;
    public Image progress;

    //display/hide boost progress
    private bool showProgress;
    private bool ShowProgress{
        get {return showProgress;}
        set{
            if(value == showProgress){
                return;
            }else{
                showProgress = value;
            }
            
            
            if (showProgress)
            {
                boosterTime = PlayerScript.defaultBoostTime;
            }
            
            progress.enabled = showProgress;
            booster.SetActive(showBooster);
        }
    }
    
    
    private float boosterTime;
    
    //show/hide booster (canIcon)
    private bool showBooster;
    private bool ShowBooster{
        get {return showBooster;}
        set{
            if(value == showBooster){
                return;
            }else{
                showBooster = value;
            }
            booster.SetActive(!booster.activeSelf);
            progress.enabled = showProgress;
        }
    }
    
    
    
    //glowsticks (OverlayCanvas)
    private GameObject[] glowSticks;

    private int noGlowSticks;
    public int NoGlowSticks{
        get{return noGlowSticks;}
        set{
            if(value == noGlowSticks){
                return;
            }else{
                noGlowSticks = value;
            }
            
            // show/hide glowsticks
            for(int i=0; i<glowSticks.Length-noGlowSticks; i++){
                glowSticks[i].SetActive(false);
                //Debug.Log("gs["+i+"]: "+glowSticks[i].activeSelf);
            }


        }

    }
    

    void Start(){
        GameIsOver = false; //to make it false each scene
        showBooster = false;
        boosterTime = PlayerScript.defaultBoostTime; //get booster time from playerscript
        showProgress = false;
        glowSticks = GameObject.FindGameObjectsWithTag("GlowStick_Overlay");    //get glowsticks by tag
        noGlowSticks = glowSticks.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //exit if game ended (one enemy reached the end)
        if(GameIsOver){
            return;
        }


        //uncomment: if(EnemyScript.playerCaught){
        if(PlayerScript.partyFound){ //TODO: remove this line
            EndGame();
        }

        //get hasItem and ligtStickCount from PlayerScript
        ShowBooster = PlayerScript.hasEnergyDrink;
        ShowProgress = PlayerScript.showProgress;
        NoGlowSticks = PlayerScript.glowStickCount;

        //reduce booster progress
        if (showProgress)
        {
            boosterTime -= Time.deltaTime;
            progress.fillAmount = boosterTime / PlayerScript.defaultBoostTime;
        }


    }


    void EndGame(){
        GameIsOver = true;
        gameOverUI.SetActive(true); //show GameOverUI-Canvas
        Time.timeScale = 0f; //freeze game time
    }


}
