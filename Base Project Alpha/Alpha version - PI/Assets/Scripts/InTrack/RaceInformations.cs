using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject endScreen;
    public GameObject timeUI;
    public GameObject lapUI;
    public GameObject positionUI;
    public GameObject controlUI;
    public GameObject leaderboardUI;


    private int i = 0;



    // Use this for initialization
    void Start() {
        playerLapTimes = new string[GameObject.Find("LapCounter").GetComponent<LapCount>().raceLapNumber * 4];
        //playerGlobalTimes = 
        playerLapCount = new int[4];
    }

    public IEnumerator showEndScreen()
    {
        yield return new WaitForSeconds(5f);
        timeUI.SetActive(false);
        lapUI.SetActive(false);
        positionUI.SetActive(false);
        controlUI.SetActive(false);
        leaderboardUI.SetActive(false);
        endScreen.SetActive(true);
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
        for(int j=0;j<=players.Length;j++)
        {
            if (playerGlobalTimes[j] == "--:--.---")
                everybodyHasFinished = false;
        }
        if (everybodyHasFinished)
            StartCoroutine(showEndScreen());
    }
}
