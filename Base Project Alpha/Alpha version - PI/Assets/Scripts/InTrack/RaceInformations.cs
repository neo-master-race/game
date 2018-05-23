﻿using System.Collections;
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
    private bool onlyOnce = true;

    public GameObject players2;
    public GameObject players3;
    public GameObject players4;

    public string trackwr;


    private int i = 0;



    // Use this for initialization
    void Start() {
        playerLapTimes = new string[GameObject.Find("LapCounter").GetComponent<LapCount>().raceLapNumber * 4];
        //playerGlobalTimes = 
        playerLapCount = new int[4];
        GameObject.Find("Network").GetComponent<Network>().getGlobalRecord(GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb);
        if (GameObject.Find("UserStats").GetComponent<UserStats>().playingSolo)
        {
            leaderboardUI.SetActive(false);
            GameObject.Find("PositionText").GetComponent<PositionHandler>().setpos(1);
        }
        /*else if (GameObject.Find("UserStats").GetComponent<UserStats>().playingMulti)
        {

        }*/
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
                if ((bestlapSec - lapSec) <= 0)
                    return "- " + (bestLapMin - (1 + LapMin)) + ":" + zeroDisplay(((59 + bestlapSec) - lapSec), 2) + "." + zeroDisplay(((1000 + bestLapMSec) - LapMSec), 3);
                else
                    return "- " + (bestLapMin - LapMin) + ":" + zeroDisplay(((-1 + bestlapSec) - lapSec), 2) + "." + zeroDisplay(((1000 + bestLapMSec) - LapMSec), 3);
            }
            else
            {
                if ((bestlapSec - lapSec) < 0)
                    return "- " + (bestLapMin - (1 + LapMin)) + ":" + zeroDisplay(((60 + bestlapSec) - lapSec), 2) + "." + zeroDisplay((bestLapMSec - LapMSec), 3);
                else
                    return "- " + (bestLapMin - LapMin) + ":" + zeroDisplay((bestlapSec - lapSec), 2) + "." + zeroDisplay((bestLapMSec - LapMSec), 3);
            }
        }
        else
        {
            if ((LapMSec - bestLapMSec) < 0)
            {
                if ((lapSec - bestlapSec) <= 0)
                    return "+ " + (LapMin - (1 + bestLapMin)) + ":" + zeroDisplay(((59 + lapSec) - bestlapSec), 2) + "." + zeroDisplay(((1000 + LapMSec) - bestLapMSec), 3);
                else
                    return "+ " + (LapMin - bestLapMin) + ":" + zeroDisplay(((-1 + lapSec) - bestlapSec), 2) + "." + zeroDisplay(((1000 + LapMSec) - bestLapMSec), 3);
            }
            else
            {
                if ((lapSec - bestlapSec) < 0)
                    return "+ " + (LapMin - (1 + bestLapMin)) + ":" + zeroDisplay(((60 + lapSec) - bestlapSec), 2) + "." + zeroDisplay((LapMSec - bestLapMSec), 3);
                else
                    return "+ " + (LapMin - bestLapMin) + ":" + zeroDisplay((lapSec - bestlapSec), 2) + "." + zeroDisplay((LapMSec - bestLapMSec), 3);
            }
        }
    }

    public bool isnewRecord(string lap, string best)
    {
        if (best == "--:--:--")
            return true;
        int bestLapMin = int.Parse(best.Substring(0, 2));
        int bestlapSec = int.Parse(best.Substring(3, 2));
        int bestLapMSec = int.Parse(best.Substring(6, 3));

        int LapMin = int.Parse(lap.Substring(0, 2));
        int lapSec = int.Parse(lap.Substring(3, 2));
        int LapMSec = int.Parse(lap.Substring(6, 3));

        if (LapMin < bestLapMin || ((LapMin == bestLapMin) && (lapSec < bestlapSec))
            || ((LapMin == bestLapMin) && (bestlapSec == lapSec) && (LapMSec < bestLapMSec)))
        { return true; }
        return false;
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
        if (raceLapNumber == 1)
        {
            lap1.SetActive(true);
            lap1.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap1.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
        }
        else if (raceLapNumber == 2)
        {
            lap2.SetActive(true);
            lap2.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap2.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
            lap2.transform.Find("Lap2Time").GetComponent<Text>().text = playerLapTimes[1];
            lap2.transform.Find("Lap2Delta").GetComponent<Text>().text = getDelta(playerLapTimes[1], record);
        }
        else if (raceLapNumber == 3)
        {
            lap3.SetActive(true);
            lap3.transform.Find("Lap1Time").GetComponent<Text>().text = playerLapTimes[0];
            lap3.transform.Find("Lap1Delta").GetComponent<Text>().text = getDelta(playerLapTimes[0], record);
            lap3.transform.Find("Lap2Time").GetComponent<Text>().text = playerLapTimes[1];
            lap3.transform.Find("Lap2Delta").GetComponent<Text>().text = getDelta(playerLapTimes[1], record);
            lap3.transform.Find("Lap3Time").GetComponent<Text>().text = playerLapTimes[2];
            lap3.transform.Find("Lap3Delta").GetComponent<Text>().text = getDelta(playerLapTimes[2], record);
        }
        else if (raceLapNumber == 4)
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
        else if (raceLapNumber == 5)
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
        endScreenSolo.transform.Find("RecordNowStringBackground/RecordNowStringtext").GetComponent<Text>().text = playerBestLapTimes[0];
        bool gotNewRecord = false;
        string newrecord = "";
        gotNewRecord = isnewRecord(playerBestLapTimes[0], record);
        if(gotNewRecord)
        {
            newrecord = playerBestLapTimes[0];
            if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
                GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord = newrecord;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
                GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord = newrecord;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
                GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord = newrecord;

            GameObject.Find("UserStats").GetComponent<UserStats>().sendStats();
        }
            

        if (gotNewRecord)
            endScreenSolo.transform.Find("RecordNewStringBackground/RecordNewStringtext").GetComponent<Text>().text = newrecord;
        else
            endScreenSolo.transform.Find("RecordNewStringBackground/RecordNewStringtext").GetComponent<Text>().text = record;

        endScreenSolo.transform.Find("wrStringtext").GetComponent<Text>().text = trackwr;

        gotNewRecord = false;
        gotNewRecord = isnewRecord(newrecord, trackwr);
        if (gotNewRecord)
            GameObject.Find("Network").GetComponent<Network>().setGlobalRecord(GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb, newrecord);
    }

    public void setMultiScreen()
    {
        endScreenMulti.SetActive(true);
        string record = "";
        if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord;
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord;
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord;
        if (playerLeaderboard.Length == 1)
        {
            players2.SetActive(true);
            players2.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players2.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players2.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players2.transform.Find("player2Username").GetComponent<Text>().text = "-";
            players2.transform.Find("player2TotalTimes").GetComponent<Text>().text = "--:--.--";
            players2.transform.Find("player2bestTimes").GetComponent<Text>().text = "--:--.--";
        }
        if (playerLeaderboard.Length == 2)
        {
            players2.SetActive(true);
            players2.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players2.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players2.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players2.transform.Find("player2Username").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players2.transform.Find("player2TotalTimes").GetComponent<Text>().text = playerGlobalTimes[1].ToString();
            players2.transform.Find("player2bestTimes").GetComponent<Text>().text = playerBestLapTimes[1].ToString();
        }
        else if (playerLeaderboard.Length == 3)
        {
            players3.SetActive(true);
            players3.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players3.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players3.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players3.transform.Find("player2Username").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players3.transform.Find("player2TotalTimes").GetComponent<Text>().text = playerGlobalTimes[1].ToString();
            players3.transform.Find("player2bestTimes").GetComponent<Text>().text = playerBestLapTimes[1].ToString();
            players3.transform.Find("player3Username").GetComponent<Text>().text = playerLeaderboard[2].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players3.transform.Find("player3TotalTimes").GetComponent<Text>().text = playerGlobalTimes[2].ToString();
            players3.transform.Find("player3bestTimes").GetComponent<Text>().text = playerBestLapTimes[2].ToString();
        }
        else if (playerLeaderboard.Length == 4)
        {
            players4.SetActive(true);
            players4.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players4.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players4.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players4.transform.Find("player2Username").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players4.transform.Find("player2TotalTimes").GetComponent<Text>().text = playerGlobalTimes[1].ToString();
            players4.transform.Find("player2bestTimes").GetComponent<Text>().text = playerBestLapTimes[1].ToString();
            players4.transform.Find("player3Username").GetComponent<Text>().text = playerLeaderboard[2].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players4.transform.Find("player3TotalTimes").GetComponent<Text>().text = playerGlobalTimes[2].ToString();
            players4.transform.Find("player3bestTimes").GetComponent<Text>().text = playerBestLapTimes[2].ToString();
            players4.transform.Find("player4Username").GetComponent<Text>().text = playerLeaderboard[3].transform.Find("userName").GetComponent<UserHoverTag>().username.ToString();
            players4.transform.Find("player4TotalTimes").GetComponent<Text>().text = playerGlobalTimes[3].ToString();
            players4.transform.Find("player4bestTimes").GetComponent<Text>().text = playerBestLapTimes[3].ToString();
        }

        string newrecord = "";
        foreach (GameObject player in playerLeaderboard)
        {
            if (player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                newrecord = playerBestLapTimes[player.GetComponent<Player_Info_Ingame>().leaderboardPosition - 1];
            }
        }
        bool gotNewRecord = false;

        gotNewRecord = isnewRecord(playerBestLapTimes[0], record);
        if (gotNewRecord)
        {
            newrecord = playerBestLapTimes[0];
            if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
                GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord = newrecord;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
                GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord = newrecord;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
                GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord = newrecord;

            GameObject.Find("UserStats").GetComponent<UserStats>().sendStats();
        }

        
        gotNewRecord = false;
        gotNewRecord = isnewRecord(newrecord, trackwr);
        if (gotNewRecord)
            GameObject.Find("Network").GetComponent<Network>().setGlobalRecord(GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb, newrecord);
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

        bool everybodyHasFinished = false;
        if(players.Length>0 && GameObject.Find("StartLights").GetComponent<startLightsSequence>().lightSequenceStep==8)
        {
            int l = 0;
            for(int j=0;j<players.Length;j++)
            {
                if (playerGlobalTimes[j] != "--:--.---")
                    l++;
                    
            }
            if (l == players.Length)
                everybodyHasFinished = true;
        }
        if (everybodyHasFinished && onlyOnce)
        {
            onlyOnce = false;
            StartCoroutine(showEndScreen());
        }
            
    }
}
