using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
    
    public TextMeshProUGUI timerText;
    protected float gametime = 46f;
    protected float timerTime;
    protected bool stopTimer;
   
    // Start is called before the first frame update
    void Start()
    {
        
        stopTimer = false;
        Time.timeScale = 1f;
        timerTime = gametime;
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        timerTime = gametime - Time.time;
        int minutes = Mathf.FloorToInt(timerTime / 60);
        int seconds = Mathf.FloorToInt(timerTime - minutes * 60);
        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        if (stopTimer == false)
        {
            timerText.text = textTime;
        }
        Check();
    }
    public bool Check()
    {
       
        if (timerTime <= 0)
        {
            stopTimer = true;
        }
        
        
        return stopTimer;
    }




}
