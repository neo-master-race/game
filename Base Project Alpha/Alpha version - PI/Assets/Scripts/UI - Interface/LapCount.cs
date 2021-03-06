﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Luc-Michel Zissler

public class LapCount : MonoBehaviour {

    public Text LapObject;
    public Text LapObjectTotal;
    public int raceLapNumber;
    public int currentLap;

	// Use this for initialization
	void Awake () {
        InitialLapInfos(1, GameObject.Find("UserStats").GetComponent<UserStats>().trackLapNumber);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitialLapInfos(int currentlapNumber, int lapNumber)
    {
        setCurrentLap(currentlapNumber);
        setRaceLapNumber(lapNumber);
    }

    public void setRaceLapNumber(int lapNumber)
    {
        raceLapNumber = lapNumber;
        GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceLapNumber=lapNumber;
        displayLapInfos();
    }

    public void setCurrentLap(int currentLapNumber)
    {
        currentLap = currentLapNumber;
        displayLapInfos();
    }

    public void displayLapInfos()
    {
        LapObject.text = currentLap.ToString();
        LapObjectTotal.text = raceLapNumber.ToString();
    }
}
