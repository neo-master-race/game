using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Luc-Michel Zissler

public class Timer : MonoBehaviour {

    public Text TimeObject;

    // Time Values
    private int currentSec;
    private int currentMilliSec;
    private int currentMin;

    private string secString;
    private string milliSecString;
    private string minString;

    private float timeVal = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeVal += Time.deltaTime;

        currentMilliSec = (int)(timeVal * 1000) % 1000;
        currentSec = (int)(timeVal) % 60;
        currentMin = (int)(timeVal / 60) % 60;

        secString = zeroDisplay(currentSec,2);
        milliSecString = zeroDisplay(currentMilliSec, 3);
        minString = zeroDisplay(currentMin,2);

        TimeObject.text = minString + ":" + secString + "." + milliSecString;

    }

    string zeroDisplay(int toclock, int zeros)
    {
        string clockstring = toclock.ToString();
        for (int i = 0; i < zeros - clockstring.Length; i++)
            clockstring = "0" + clockstring;

        return clockstring;
    }

    public void resetTimer()
    {
        timeVal = 0;
        currentSec = 0;
        currentMilliSec = 0;
        currentMin = 0;
    }

}
