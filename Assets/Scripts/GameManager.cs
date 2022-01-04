using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //game over 
    public static bool GameIsOver; //static to access it from outside.  = false, would equal to true, if the scenes gets started again
    public GameObject gameOverUI;
    
    //party found
    public static bool partyFound;
    public GameObject partyFoundUI;
    
    //play time
    public static int maxPlayTime = 120; //sec
    [System.NonSerialized]
    public static double timePlayed;
    
    //game time ui
    public TMP_Text timeText;
    private int showMessageTime = 5;
    
    
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
        Time.timeScale = 1f; //start game time
        GameIsOver = false; //to make it false each scene
        partyFound = false;
        gameOverUI.SetActive(false);
        partyFoundUI.SetActive(false);
        ShowBooster = false;
        timePlayed = 0.0;
        boosterTime = PlayerScript.defaultBoostTime; //get booster time from playerscript
        showProgress = false;
        glowSticks = GameObject.FindGameObjectsWithTag("GlowStick Overlay");    //get glowsticks by tag
        noGlowSticks = glowSticks.Length;
        
        //start coroutine to show time related messages
        StartCoroutine(TimeMessages());
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        //exit if game ended
        if(GameIsOver || partyFound){
            return;
        }
        
        //increase time played
        timePlayed += Time.deltaTime;
        
        //if player caught
        if(EnemyScript.playerCaught){
            EndGame();
        }

        //if time's up
        if (timePlayed >= maxPlayTime + 12)
        {
            EndGame();
        }

        //if party found
        if (PlayerScript.partyFound)
        {
            PartyFound();
        }

        //get hasEnergyDrink, showProgess and glowStickCount from PlayerScript
        ShowBooster = PlayerScript.hasEnergyDrink;
        ShowProgress = PlayerScript.showProgress;
        NoGlowSticks = PlayerScript.glowStickCount;

        //reduce booster progress over time (if activated)
        if (showProgress)
        {
            boosterTime -= Time.deltaTime;
            progress.fillAmount = boosterTime / PlayerScript.defaultBoostTime;
        }
        
    }

    //routine for time related messages
    private IEnumerator TimeMessages()
    {
        var firstMoment = maxPlayTime / 2;
        var secondMoment = maxPlayTime / 4 - showMessageTime;
        var thirdMoment = maxPlayTime / 8 - 2 * showMessageTime;
        
        timeText.enabled = false;
        
        //first message (at around half time)
        yield return new WaitForSeconds(firstMoment);
        timeText.text = "Der Rave wartet nicht die ganze Nacht auf dich...";
        timeText.enabled = true;
        yield return new WaitForSeconds(showMessageTime);
        timeText.enabled = false;
        
        //second message (at around three-quarter)
        yield return new WaitForSeconds(secondMoment);
        timeText.text = "Beeil' dich, die Nacht ist bald vorbei...";
        timeText.enabled = true;
        yield return new WaitForSeconds(showMessageTime);
        timeText.enabled = false;

        //third message (at last 20 seconds)
        yield return new WaitForSeconds(thirdMoment);
        timeText.text = "Einige haben den Rave bereits verlassen...";
        timeText.enabled = true;
        yield return new WaitForSeconds(showMessageTime);
        timeText.enabled = false;
    }

    void PartyFound()
    {
        partyFound = true;
        partyFoundUI.SetActive(true);
        Time.timeScale = 0f; //freeze game time
    }


    void EndGame(){
        GameIsOver = true;
        Time.timeScale = 0f; //freeze game time
        gameOverUI.SetActive(true);
        print("End Game!");
    }
    
    
    
    


}
