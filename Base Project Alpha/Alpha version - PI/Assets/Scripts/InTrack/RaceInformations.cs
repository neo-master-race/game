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
    public int raceMinimumLeaderboardPosition = 1;

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

    public GameObject leaderboard2players;
    public GameObject leaderboard3players;
    public GameObject leaderboard4players;

    private int i = 0;



    // Use this for initialization
    void Start() {
        playerLapTimes = new string[GameObject.Find("LapCounter").GetComponent<LapCount>().raceLapNumber * 4];
        //playerGlobalTimes = 
        playerLapCount = new int[4];
		int nbTrack = GameObject.Find ("UserStats").GetComponent<UserStats> ().onTrackNb;
		GameObject.Find("Network").GetComponent<Network>().getGlobalRecord(nbTrack);
		if(nbTrack==1)
			trackwr = GameObject.Find("UserStats").GetComponent<UserStats>().track1WorldRecord;
		if(nbTrack==2)
			trackwr = GameObject.Find("UserStats").GetComponent<UserStats>().track2WorldRecord;
		if(nbTrack==3)
			trackwr = GameObject.Find("UserStats").GetComponent<UserStats>().track3WorldRecord;
		
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
        if (bestlap == "--:--:--" || bestlap == "--:--.--" || bestlap == "--:--.---" || bestlap == "--:--:---")
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
        if (best == "--:--:--" || best== "--:--.--" || best == "--:--.---" || best == "--:--:---")
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
        {
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord;
            GameObject.Find("CircuitText").GetComponent<Text>().text = "Euromir";
        }
            
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
        {
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord;
            GameObject.Find("CircuitText").GetComponent<Text>().text = "Monaco";
        }
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
        {
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord;
            GameObject.Find("CircuitText").GetComponent<Text>().text = "Super-8";
        }

        GameObject.Find("Pseudotext").GetComponent<Text>().text = GameObject.Find("UserStats").GetComponent<UserStats>().username.ToString();
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

        newrecord = playerBestLapTimes[0];
        gotNewRecord = isnewRecord(newrecord, trackwr);
        if (gotNewRecord)
        {
            GameObject.Find("Network").GetComponent<Network>().setGlobalRecord(GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb, newrecord);
            endScreenSolo.transform.Find("wrStringBackground/wrStringtext").GetComponent<Text>().text = newrecord;
        }
        else
            endScreenSolo.transform.Find("wrStringBackground/wrStringtext").GetComponent<Text>().text = trackwr;


    }

    public void setMultiScreen()
    {
        endScreenMulti.SetActive(true);
        string record = "";
        if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
        {
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord;
            GameObject.Find("CircuitText").GetComponent<Text>().text = "Euromir";
        }
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
        {
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord;
            GameObject.Find("CircuitText").GetComponent<Text>().text = "Monaco";
        }
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
        {
            record = GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord;
            GameObject.Find("CircuitText").GetComponent<Text>().text = "Super-8";
        }

        GameObject.Find("Pseudotext").GetComponent<Text>().text = GameObject.Find("UserStats").GetComponent<UserStats>().username.ToString();
        if (playerLeaderboard.Length == 1)
        {
            players2.SetActive(true);
            players2.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players2.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players2.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players2.transform.Find("player2Username").GetComponent<Text>().text = "-";
            players2.transform.Find("player2TotalTimes").GetComponent<Text>().text = "--:--.--";
            players2.transform.Find("player2bestTimes").GetComponent<Text>().text = "--:--.--";
        }
        if (playerLeaderboard.Length == 2)
        {
            players2.SetActive(true);
            players2.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players2.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players2.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players2.transform.Find("player2Username").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players2.transform.Find("player2TotalTimes").GetComponent<Text>().text = playerGlobalTimes[1].ToString();
            players2.transform.Find("player2bestTimes").GetComponent<Text>().text = playerBestLapTimes[1].ToString();
        }
        else if (playerLeaderboard.Length == 3)
        {
            players3.SetActive(true);
            players3.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players3.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players3.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players3.transform.Find("player2Username").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players3.transform.Find("player2TotalTimes").GetComponent<Text>().text = playerGlobalTimes[1].ToString();
            players3.transform.Find("player2bestTimes").GetComponent<Text>().text = playerBestLapTimes[1].ToString();
            players3.transform.Find("player3Username").GetComponent<Text>().text = playerLeaderboard[2].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players3.transform.Find("player3TotalTimes").GetComponent<Text>().text = playerGlobalTimes[2].ToString();
            players3.transform.Find("player3bestTimes").GetComponent<Text>().text = playerBestLapTimes[2].ToString();
        }
        else if (playerLeaderboard.Length == 4)
        {
            players4.SetActive(true);
            players4.transform.Find("player1Username").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players4.transform.Find("player1TotalTimes").GetComponent<Text>().text = playerGlobalTimes[0].ToString();
            players4.transform.Find("player1bestTimes").GetComponent<Text>().text = playerBestLapTimes[0].ToString();
            players4.transform.Find("player2Username").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players4.transform.Find("player2TotalTimes").GetComponent<Text>().text = playerGlobalTimes[1].ToString();
            players4.transform.Find("player2bestTimes").GetComponent<Text>().text = playerBestLapTimes[1].ToString();
            players4.transform.Find("player3Username").GetComponent<Text>().text = playerLeaderboard[2].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players4.transform.Find("player3TotalTimes").GetComponent<Text>().text = playerGlobalTimes[2].ToString();
            players4.transform.Find("player3bestTimes").GetComponent<Text>().text = playerBestLapTimes[2].ToString();
            players4.transform.Find("player4Username").GetComponent<Text>().text = playerLeaderboard[3].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            players4.transform.Find("player4TotalTimes").GetComponent<Text>().text = playerGlobalTimes[3].ToString();
            players4.transform.Find("player4bestTimes").GetComponent<Text>().text = playerBestLapTimes[3].ToString();
        }

        string newrecord = "";
        foreach (GameObject player in playerLeaderboard)
        {
            if (player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                newrecord = playerBestLapTimes[player.GetComponent<Player_Info_Ingame>().leaderboardPosition - 1];
                int finalpos = player.GetComponent<Player_Info_Ingame>().leaderboardPosition;
                if(finalpos==1)
                {
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().text = "1";
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().text = "er";
                    endScreenMulti.transform.Find("PositionPanel").GetComponent<Image>().color = new Color32(235, 205, 0, 200);
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().color = new Color32(255, 225, 0, 255);
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().color = new Color32(255, 225, 0, 255);
                }
                else if (finalpos == 2)
                {
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().text = "2";
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().text = "ème";
                    endScreenMulti.transform.Find("PositionPanel").GetComponent<Image>().color = new Color32(120, 120, 120, 200);
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().color = new Color32(154, 154, 154, 255);
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().color = new Color32(154, 154, 154, 255);
                }
                else if (finalpos == 3)
                {
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().text = "3";
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().text = "ème";
                    endScreenMulti.transform.Find("PositionPanel").GetComponent<Image>().color = new Color32(170, 80, 50, 200);
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().color = new Color32(207, 120, 50, 255);
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().color = new Color32(207, 120, 50, 255);
                }
                else if (finalpos == 4)
                {
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().text = "4";
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().text = "ème";
                    endScreenMulti.transform.Find("PositionPanel").GetComponent<Image>().color = new Color32(60, 16, 104, 200);
                    endScreenMulti.transform.Find("PositionPanel/PositionText").GetComponent<Text>().color = new Color32(120, 60, 150, 255);
                    endScreenMulti.transform.Find("PositionPanel/PositionText2").GetComponent<Text>().color = new Color32(120, 60, 150, 255);
                }
            }
        }
        bool gotNewRecord = false;

        gotNewRecord = isnewRecord(newrecord, record);
        if (gotNewRecord)
        {
            if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
                GameObject.Find("UserStats").GetComponent<UserStats>().track1LapRecord = newrecord;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
                GameObject.Find("UserStats").GetComponent<UserStats>().track2LapRecord = newrecord;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
                GameObject.Find("UserStats").GetComponent<UserStats>().track3LapRecord = newrecord;

            GameObject.Find("UserStats").GetComponent<UserStats>().sendStats();
        }


        if (gotNewRecord)
        {
            gotNewRecord = isnewRecord(newrecord, trackwr);
            if (gotNewRecord)
                GameObject.Find("Network").GetComponent<Network>().setGlobalRecord(GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb, newrecord);
        }
    }

    public IEnumerator showEndScreen()
    {
        hasFinished = true;
        yield return new WaitForSeconds(2f);
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

        int nbTrack2 = GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb;
        if (nbTrack2 == 1)
            trackwr = GameObject.Find("UserStats").GetComponent<UserStats>().track1WorldRecord;
        if (nbTrack2 == 2)
            trackwr = GameObject.Find("UserStats").GetComponent<UserStats>().track2WorldRecord;
        if (nbTrack2 == 3)
            trackwr = GameObject.Find("UserStats").GetComponent<UserStats>().track3WorldRecord;



        if (playerLeaderboard.Length == 2)
        {
            leaderboard2players.SetActive(true);
            leaderboard3players.SetActive(false);
            leaderboard4players.SetActive(false);
            leaderboard2players.transform.Find("LeaderboardPlayer1Back/1stPlayer").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            leaderboard2players.transform.Find("LeaderboardPlayer2Back/2ndPlayer").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
        }
        else if (playerLeaderboard.Length == 3)
        {
            leaderboard2players.SetActive(false);
            leaderboard3players.SetActive(true);
            leaderboard4players.SetActive(false);
            leaderboard3players.transform.Find("LeaderboardPlayer1Back/1stPlayer").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            leaderboard3players.transform.Find("LeaderboardPlayer2Back/2ndPlayer").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            leaderboard3players.transform.Find("LeaderboardPlayer3Back/3rdPlayer").GetComponent<Text>().text = playerLeaderboard[2].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
        }
        else if (playerLeaderboard.Length == 4)
        {
            leaderboard2players.SetActive(false);
            leaderboard3players.SetActive(false);
            leaderboard4players.SetActive(true);
            leaderboard4players.transform.Find("LeaderboardPlayer1Back/1stPlayer").GetComponent<Text>().text = playerLeaderboard[0].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            leaderboard4players.transform.Find("LeaderboardPlayer2Back/2ndPlayer").GetComponent<Text>().text = playerLeaderboard[1].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            leaderboard4players.transform.Find("LeaderboardPlayer3Back/3rdPlayer").GetComponent<Text>().text = playerLeaderboard[2].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
            leaderboard4players.transform.Find("LeaderboardPlayer4Back/4thPlayer").GetComponent<Text>().text = playerLeaderboard[3].transform.Find("userName").GetComponent<UserHoverTag>().username.text.ToString();
        }
        else
        {
            leaderboard2players.SetActive(false);
            leaderboard3players.SetActive(false);
            leaderboard4players.SetActive(false);
        }


    }
}
