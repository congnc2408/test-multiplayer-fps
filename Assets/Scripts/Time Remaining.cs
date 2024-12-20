using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeRemaining : MonoBehaviour
{
    public static TimeRemaining timeRemaining { get; set; }

    public TMP_Text timerText;
    public float remainingTime;
    public int healthNew;
    int timeSkip;

    public void setRemainingTime(float _remainingTime)
    {
        _remainingTime = remainingTime;
        TimeCountDown();
    }
    public float getRemainingTime()

    {
        return this.remainingTime;
    }
    void Awake()
    {
        timeRemaining = this;
        //Debug.Log("time down: " + remainingTime);
        // healthNew = gameObject.GetComponent<Health>().health;
        // if (healthNew )
        // {

        //}
    }

    void Update()
    {
        TimeCountDown();
    }

    void TimeCountDown()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0)
        {
            timerText.text = "00:00";
            Application.Quit();
            Debug.Log("End");
        }

    }


}
