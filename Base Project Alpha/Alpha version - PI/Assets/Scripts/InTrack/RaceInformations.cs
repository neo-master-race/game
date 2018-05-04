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
    public string[] playerGlobalTimes;
    public string[] playerLapTimes;
    public string[] playerBestLapTimes= { "-", "-", "-", "-" };


    private int i = 0;



    // Use this for initialization
    void Start() {
        playerLapTimes = new string[GameObject.Find("LapCounter").GetComponent<LapCount>().raceLapNumber * 4];
        playerGlobalTimes = new string[4];
        playerLapCount = new int[4];
    }
	
	// Update is called once per frame
	void Update () {
        players = GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().players;
        playerLeaderboard = GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().playersLeaderboard;

        foreach(GameObject playr in players)
        {
            playerLapCount[i] = players[i].GetComponent<Player_Info_Ingame>().lap_count;
        }
    }
}
