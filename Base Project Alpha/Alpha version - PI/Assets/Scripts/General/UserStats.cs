using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats : MonoBehaviour {

    [Header("User Informations")]
    public string username;
    public bool isOnRoomList = false;
    public string inLobby;

    [Header("User Global Stats")]
    public int raceNb;
    public int raceVictory;

    [Header("User Records")]
    public string track1LapRecord;
    public string track2LapRecord;
    public string track3LapRecord;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }

    public void isGuest()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().username = GameObject.Find("Network").GetComponent<Network>().getClientName();
    }

    public void setUserStats(string usernam,int numberOfRaces,int numberOfWins,string recordTrack1, string recordTrack2, string recordTrack3)
    {
        username = usernam;
        raceNb = numberOfRaces;
        raceVictory = numberOfWins;
        track1LapRecord = recordTrack1;
        track2LapRecord = recordTrack2;
        track3LapRecord = recordTrack3;
    }

    // Update is called once per frame
    void Update () {
            
    }
}
