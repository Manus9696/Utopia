﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PCInterfaceScript : MonoBehaviour
{
    public int hours = 0;
    public int minutes = 0;
    public int seconds = 0;
    public Boolean clockRunning = false;
    public DateTime roomTime = new DateTime(1997, 5, 12,3,3,3);
    public Text clockText;
    // Start is called before the first frame update
    void Start()
    {
        StartClock();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void ToogleInterfaceActive()
    {
        this.gameObject.SetActive(!this.gameObject.active);
    }

    public void StartClock()
    {
        clockRunning = true;
        roomTime = roomTime.AddHours(UnityEngine.Random.Range(0, 23));
        roomTime = roomTime.AddMinutes(UnityEngine.Random.Range(0, 59));
        roomTime = roomTime.AddSeconds(UnityEngine.Random.Range(0, 59));
        
        clockText.text = roomTime.Hour + ":" + roomTime.Minute + ":" + roomTime.Second;
        StartCoroutine(clockTimeIncrease());
    }
    public IEnumerator clockTimeIncrease()
    {
        while (true)
        {
            roomTime = roomTime.AddSeconds(1);
            updateClockTime();
            yield return new WaitForSeconds(1f);
        }
    }
    public void updateClockTime()
    {
        clockText.text = "";
        if (roomTime.Hour < 10) { clockText.text += "0" + roomTime.Hour; } else { clockText.text += roomTime.Hour; }
        clockText.text += ":";
        if (roomTime.Minute < 10) { clockText.text += "0" + roomTime.Minute; } else { clockText.text += roomTime.Minute; }
        clockText.text += ":";
        if (roomTime.Second < 10) { clockText.text += "0" + roomTime.Second; } else { clockText.text += roomTime.Second; }
    }
}
