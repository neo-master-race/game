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
    public bool hasFinished = false;


    private int i = 0;



    // Use this for initialization
    void Start() {
        playerLapTimes = new string[GameObject.Find("LapCounter").GetComponent<LapCount>().raceLapNumber * 4];
        //playerGlobalTimes = 
        playerLapCount = new int[4];
    }

    public void setSoloScreen ()
    {
        endScreenSolo.SetActive(true);
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
