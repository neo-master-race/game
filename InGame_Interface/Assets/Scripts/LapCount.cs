using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Luc-Michel Zissler

public class LapCount : MonoBehaviour {

    public Text LapObject;
    private int raceLapNumber;
    private int currentLap;

	// Use this for initialization
	void Start () {
        InitialLapInfos(1, 3);
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
        displayLapInfos();
    }

    public void setCurrentLap(int currentLapNumber)
    {
        currentLap = currentLapNumber;
        displayLapInfos();
    }

    public void displayLapInfos()
    {
        LapObject.text = currentLap + "/" + raceLapNumber;
    }
}
