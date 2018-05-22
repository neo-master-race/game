using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceInformations : MonoBehaviour {

    [Header("PlayerInfo")]
    public GameObject[] players;
    public GameObject[] playerLeaderboard;

    [Header("InRace Info")]
    public int raceLapNumber;
    [Space(10)]
    public int[] playerLapCount;
    public string[] playerGlobalTimes= { "--:--.---", "--:--.---", "--:--.---", "--:--.---" };
    public string[] playerLapTimes;
    public string[] playerBestLapTimes= { "--:--.---", "--:--.---", "--:--.---", "--:--.---" };

    [Header("final leaderboard")]
    public GameObject endScreenSolo;
    public GameObject endScreenMulti;
    public GameObject timeUI;
    public GameObject lapUI;
    public GameObject positionUI;
    public GameObject controlUI;
    public GameObject leaderboardUI;
    public GameObject lap1;
    public GameObject lap2;
    public GameObject lap3;
    public GameObject lap4;
    public GameObject lap5;
    public bool hasFinished = false;


    private int i = 0;



    // Use this for initialization
    void Start() {
        playerLapTimes = new string[GameObject.Find("LapCounter").GetComponent<LapCount>().raceLapNumber * 4];
        //playerGlobalTimes = 
        playerLapCount = new int[4];
    }

    string zeroDisplay(int toclock, int zeros)
    {
        string clockstring = toclock.ToString();
        for (int i = 0; i < zeros - clockstring.Length; i++)
            clockstring = "0" + clockstring;

        return clockstring;
    }

    string getDelta(string lap, string bestlap)
    {
        if (bestlap == "--:--:--")
            return "- "+lap;
        int bestLapMin = int.Parse(bestlap.Substring(0, 2));
        int bestlapSec = int.Parse(bestlap.Substring(3, 2));
        int bestLapMSec = int.Parse(bestlap.Substring(6, 3));

        int LapMin = int.Parse(lap.Substring(0, 2));
        int lapSec = int.Parse(lap.Substring(3, 2));
        int LapMSec = int.Parse(lap.Substring(6, 3));

        if (LapMin < bestLapMin || ((LapMin == bestLapMin) && (lapSec < bestlapSec))
        || ((LapMin == bestLapMin) && (bestlapSec == lapSec) && (LapMSec < bestLapMSec)))
        {
            if ((bestLapMSec - LapMSec) < 0)
            {
                if ((bestlapSec - lapSec) < 0)
                    return "- " + (bestLapMin - (1 + LapMin)) + ":" + zeroDisplay(((59 + bestlapSec) - lapSec), 2) + "." + zeroDisplay(((1000 + bestLapMSec) - bestLapMSec), 3);
                else
                    return "- " + (bestLapMin - LapMin) + ":" + zeroDisplay(((-1 + bestlapSec) - lapSec), 2) + "." + zeroDisplay(((1000 + bestLapMSec) - bestLapMSec), 3);
            }
            else
            {
                if ((bestlapSec - lapSec) < 0)
                    return "- " + (bestLapMin - (1 + LapMin)) + ":" + zeroDisplay(((60 + bestlapSec) - lapSec), 2) + "." + zeroDisplay((bestLapMSec - bestLapMSec), 3);
                else
                    return "- " + (bestLapMin - LapMin) + ":" + zeroDisplay((bestlapSec - lapSec), 2) + "." + zeroDisplay((bestLapMSec - bestLapMSec), 3);
            }
        }
        else
        {
            if ((bestLapMSec - bestLapMSec) < 0)
            {
                if ((lapSec - bestlapSec) < 0)
                    return "+ " + (LapMin - (1 + bestLapMin)) + ":" + zeroDisplay(((59 + lapSec) - bestlapSec), 2) + "." + zeroDisplay(((1000 + bestLapMSec) - bestLapMSec), 3);
                else
                    return "+ " + (LapMin - bestLapMin) + ":" + zeroDisplay(((-1 + lapSec) - bestlapSec), 2) + "." + zeroDisplay(((1000 + bestLapMSec) - bestLapMSec), 3);
            }
            else
            {
                if ((lapSec - bestlapSec) < 0)
                    return "+ " + (LapMin - (1 + bestLapMin)) + ":" + zeroDisplay(((60 + lapSec) - bestlapSec), 2) + "." + zeroDisplay((bestLapMSec - bestLapMSec), 3);
                else
                    return "+ " + (LapMin - bestLapMin) + ":" + zeroDisplay((lapSec - bestlapSec), 2) + "." + zeroDisplay((bestLapMSec - bestLapMSec), 3);
            }
        }
    }

    public void setSoloScreen ()
    {
        endScreenSolo.SetActive(true);
        string record = "";
        if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord;
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord;
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord;
        if (raceLapNumber==1)
        {
            lap1.SetActive(true);
            lap1.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap1.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0],record);
        }
        else if(raceLapNumber == 2)
        {
            lap2.SetActive(true);
            lap2.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap2.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
            lap2.transform.Find("Lap2Time").GetComponent<Text>().text = playerLapTimes[1];
            lap2.transform.Find("Lap2Delta").GetComponent<Text>().text = getDelta(playerLapTimes[1], record);
        }
        else if(raceLapNumber == 3)
        {
            lap3.SetActive(true);
            lap3.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap3.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
            lap3.transform.Find("Lap2Time").GetComponent<Text>().text = playerLapTimes[1];
            lap3.transform.Find("Lap2Delta").GetComponent<Text>().text = getDelta(playerLapTimes[1], record);
            lap3.transform.Find("Lap3Time").GetComponent<Text>().text = playerLapTimes[2];
            lap3.transform.Find("Lap3Delta").GetComponent<Text>().text = getDelta(playerLapTimes[2], record);
        }
        else if(raceLapNumber == 4)
        {
            lap4.SetActive(true);
            lap4.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap4.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
            lap4.transform.Find("Lap2Time").GetComponent<Text>().text = playerLapTimes[1];
            lap4.transform.Find("Lap2Delta").GetComponent<Text>().text = getDelta(playerLapTimes[1], record);
            lap4.transform.Find("Lap3Time").GetComponent<Text>().text = playerLapTimes[2];
            lap4.transform.Find("Lap3Delta").GetComponent<Text>().text = getDelta(playerLapTimes[2], record);
            lap4.transform.Find("Lap4Time").GetComponent<Text>().text = playerLapTimes[3];
            lap4.transform.Find("Lap4Delta").GetComponent<Text>().text = getDelta(playerLapTimes[3], record);
        }
        else if(raceLapNumber == 5)
        {
            lap5.SetActive(true);
            lap5.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap5.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
            lap5.transform.Find("Lap2Time").GetComponent<Text>().text = playerLapTimes[1];
            lap5.transform.Find("Lap2Delta").GetComponent<Text>().text = getDelta(playerLapTimes[1], record);
            lap5.transform.Find("Lap3Time").GetComponent<Text>().text = playerLapTimes[2];
            lap5.transform.Find("Lap3Delta").GetComponent<Text>().text = getDelta(playerLapTimes[2], record);
            lap5.transform.Find("Lap4Time").GetComponent<Text>().text = playerLapTimes[3];
            lap5.transform.Find("Lap4Delta").GetComponent<Text>().text = getDelta(playerLapTimes[3], record);
            lap5.transform.Find("Lap5Time").GetComponent<Text>().text = playerLapTimes[4];
            lap5.transform.Find("Lap5Delta").GetComponent<Text>().text = getDelta(playerLapTimes[4], record);
        }
    }

    public void setMultiScreen()
    {
        endScreenMulti.SetActive(true);
    }

    public IEnumerator showEndScreen()
    {
        hasFinished = true;
        yield return new WaitForSeconds(5f);
        timeUI.SetActive(false);
        lapUI.SetActive(false);
        positionUI.SetActive(false);
        controlUI.SetActive(false);
        leaderboardUI.SetActive(false);
        GameObject.Find("EndScreen").GetComponent<RawImage>().enabled = true;
        if (GameObject.Find("UserStats").GetComponent<UserStats>().playingSolo)
            setSoloScreen();
        else
            setMultiScreen();
    }

    // Update is called once per frame
    void Update () {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                players =player.GetComponent<Player_Info_Ingame>().players;
                playerLeaderboard = player.GetComponent<Player_Info_Ingame>().playersLeaderboard;
            }
        }
        
        i = 0;
        foreach(GameObject playr in players)
        {
            playerLapCount[i] = players[i].GetComponent<Player_Info_Ingame>().lap_count;
            i++;
        }

        bool everybodyHasFinished = true;
        for(int j=0;j<players.Length;j++)
        {
            if (playerGlobalTimes[j] == "--:--.---")
                everybodyHasFinished = false;
        }
        if (everybodyHasFinished)
            StartCoroutine(showEndScreen());
    }
}
