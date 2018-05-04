using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Luc-Michel Zissler

public class Timer : MonoBehaviour {

    public Text TimeObject;

    // Time Values
    private int[] currentSecLap=new int[4];
    private int[] currentMilliSecLap = new int[4];
    private int[] currentMinLap = new int[4];

    private string[] secStringLap = new string[4];
    private string[] milliSecStringLap=new string[4];
    private string[] minStringLap=new string[4];

    private float[] timeValLap = { 0f, 0f, 0f, 0f };


    private int[] currentSecGlobal = new int[4];
    private int[] currentMilliSecGlobal = new int[4];
    private int[] currentMinGlobal = new int[4];

    private string[] secStringGlobal = new string[4];
    private string[] milliSecStringGlobal = new string[4];
    private string[] minStringGlobal = new string[4];

    private float[] timeValGlobal = { 0f, 0f, 0f, 0f };

    public GameObject[] players;


   // public string[] globalTimes;
    //public string[] lapTimes;

    public bool[] timerOn = { false, false, false, false };

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        players = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().players;
        for (int i = 0; i < players.Length; i++)
        {
            if (timerOn[i])
            {
                timeValLap[i] += Time.deltaTime;
                timeValGlobal[i] += Time.deltaTime;

                currentMilliSecLap[i] = (int)(timeValLap[i] * 1000) % 1000;
                currentSecLap[i] = (int)(timeValLap[i]) % 60;
                currentMinLap[i] = (int)(timeValLap[i] / 60) % 60;

                secStringLap[i] = zeroDisplay(currentSecLap[i], 2);
                milliSecStringLap[i] = zeroDisplay(currentMilliSecLap[i], 3);
                minStringLap[i] = zeroDisplay(currentMinLap[i], 2);

                currentMilliSecGlobal[i] = (int)(timeValGlobal[i] * 1000) % 1000;
                currentSecGlobal[i] = (int)(timeValGlobal[i]) % 60;
                currentMinGlobal[i] = (int)(timeValGlobal[i] / 60) % 60;

                secStringGlobal[i] = zeroDisplay(currentSecGlobal[i], 2);
                milliSecStringGlobal[i] = zeroDisplay(currentMilliSecGlobal[i], 3);
                minStringGlobal[i] = zeroDisplay(currentMinGlobal[i], 2);

                if(players[i].GetComponent<Player_Info_Ingame>().isLocalPlayer)
                    TimeObject.text = minStringLap[i] + ":" + secStringLap[i] + "." + milliSecStringLap[i];
            }
        }
    }

    string zeroDisplay(int toclock, int zeros)
    {
        string clockstring = toclock.ToString();
        for (int i = 0; i < zeros - clockstring.Length; i++)
            clockstring = "0" + clockstring;

        return clockstring;
    }

    public void resetTimer(int playerId)
    {
        int racelap = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceLapNumber;
        GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerLapTimes[playerId* racelap + GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerLapCount[playerId]- 1] = zeroDisplay(currentMinLap[playerId], 2) + ":"+ zeroDisplay(currentSecLap[playerId], 2) + ":"+ zeroDisplay(currentMilliSecLap[playerId], 3);
        if (GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerBestLapTimes[playerId] != "-")
        {
            int bestLapMin = int.Parse(GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerBestLapTimes[playerId].Substring(0, 2));
            int bestlapSec = int.Parse(GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerBestLapTimes[playerId].Substring(3, 2));
            int bestLapMSec = int.Parse(GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerBestLapTimes[playerId].Substring(6, 3));
            if(currentMinLap[playerId] < bestLapMin || ((currentMinLap[playerId] == bestLapMin) && (currentSecLap[playerId]< bestlapSec))
            || ((currentMinLap[playerId] == bestLapMin) && (bestlapSec == currentSecLap[playerId]) && (currentMilliSecLap[playerId]<bestLapMSec)))
            {
                GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerBestLapTimes[playerId]= zeroDisplay(currentMinLap[playerId], 2) + ":" + zeroDisplay(currentSecLap[playerId], 2) + ":" + zeroDisplay(currentMilliSecLap[playerId], 3);
            }
            Debug.Log(bestLapMin + ":" + bestlapSec + ":" + bestLapMSec);
            Debug.Log(currentMinLap[playerId] + ":" + currentSecLap[playerId] + ":" + currentMilliSecLap[playerId]);
        }
        else
            GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerBestLapTimes[playerId]= zeroDisplay(currentMinLap[playerId], 2) + ":" + zeroDisplay(currentSecLap[playerId], 2) + ":" + zeroDisplay(currentMilliSecLap[playerId], 3);
        timeValLap[playerId] = 0;
        currentSecLap[playerId] = 0;
        currentMilliSecLap[playerId] = 0;
        currentMinLap[playerId] = 0;
    }

    public void stopGlobalTimer(int playerId)
    {
        timerOn[playerId] = false;
        GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerGlobalTimes[playerId] = zeroDisplay(currentMinGlobal[playerId], 2) + ":" + zeroDisplay(currentSecGlobal[playerId], 2) + ":" + zeroDisplay(currentMilliSecGlobal[playerId], 3);
    }

}
