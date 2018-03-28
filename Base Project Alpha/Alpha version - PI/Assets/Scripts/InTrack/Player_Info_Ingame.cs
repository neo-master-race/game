using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Info_Ingame : MonoBehaviour {

    [Space(20)]

    public bool isLocalPlayer = false;

    [Space(30)]

    [Header("current race completion infos")]
    public bool[] wentThrough;
    public int lap_count = 1;
    public bool hasHitSFLineOnce;
    public int nextCheckpointNumber = 1;
    public int supposedNextCheckpointNumber = 1;
    public int cp_count=0;

    [Header("Waypoints (relative position indicator)")]
    public float[] distanceToWaypoint;

    [Header("UI Stats")]
    public int leaderboardPosition=1;
    public int currentItemIndex;
    [Space(5)]
    public float[] lapTimes;
    public float bestLapTime;

    [Header("Other Players")]
    public GameObject[] players;

    // Use this for initialization
    void Start()
    {
        leaderboardPosition = 1;
        wentThrough = new bool[GameObject.Find("Checkpoints").GetComponent<Checkpoints_Check>().checkpoints_collider.Length];
        distanceToWaypoint = new float[GameObject.Find("Checkpoints").GetComponent<Checkpoints_Check>().wayPoints.Length];
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if(GameObject.Find("Checkpoints").GetComponent<Checkpoints_Check>().initializedWaypointDistances)
        {
            foreach (GameObject player in players)
            {
                if (GetComponent<Player_Info_Ingame>().distanceToWaypoint[0] > player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[0])
                    leaderboardPosition = player.GetComponent<Player_Info_Ingame>().leaderboardPosition + 1;
            }
        }
        /*foreach(GameObject player in players)
        {
            if(!player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                if (GetComponent<Player_Info_Ingame>().lap_count < player.GetComponent<Player_Info_Ingame>().lap_count)
                    leaderboardPosition= player.GetComponent<Player_Info_Ingame>().leaderboardPosition + 1;
                else if(GetComponent<Player_Info_Ingame>().lap_count > player.GetComponent<Player_Info_Ingame>().lap_count)
                {

                }
            }
        }*/
        GameObject.Find("PositionText").GetComponent<Text>().text = leaderboardPosition.ToString();

    }
}
