using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_info_server : MonoBehaviour {

    public int[] players_laps=new int[4];

    public GameObject[] players;
    public GameObject[] playersLeaderboard;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < players.Length ; i++)
        {
            players_laps[i] = players[i].GetComponent<Player_Info_Ingame>().lap_count;
            Debug.Log(players[i].name);
        }*/
    }
}
