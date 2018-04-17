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
    public GameObject[] playersLeaderboard;

    // Use this for initialization
    void Start()
    {
        leaderboardPosition = 1;
        wentThrough = new bool[GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().checkpoints_collider.Length];
        distanceToWaypoint = new float[GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().wayPoints.Length];
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if(this.gameObject.GetComponent<Player_Info_Ingame>().isLocalPlayer)
        {
            int nb_players_before = players.Length;
            players = GameObject.FindGameObjectsWithTag("Player");
            playersLeaderboard = GameObject.FindGameObjectsWithTag("Player");
            int nb_players_after = players.Length;
            if (nb_players_before != nb_players_after)
            {
                if (nb_players_after - nb_players_before > 0)
                {
                    for (int i = 0; i < nb_players_after - nb_players_before; i++)
                    {
                        for (int j = 0; j < playersLeaderboard.Length - 1; j++)
                        {
                            GameObject tmp = playersLeaderboard[j + 1];
                            playersLeaderboard[j + 1] = playersLeaderboard[j];
                            playersLeaderboard[j] = tmp;
                        }
                    }
                }
            }
            
            if (GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().initializedWaypointDistances
                && !GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().initializedWaypointDistancesConfirmation)
            {
                for (int i=0;i< playersLeaderboard.Length-1;i++)
                {
                    for (int j = 0; j < playersLeaderboard.Length-1; j++)
                    {
                        if ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[0]+ playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[1])
                             >
                            (playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().distanceToWaypoint[0] + playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[1]))
                        {
                            GameObject tmp = playersLeaderboard[j + 1];
                            playersLeaderboard[j + 1] = playersLeaderboard[j];
                            playersLeaderboard[j] = tmp;
                        }
                    }
                }
                GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().initializedWaypointDistancesConfirmation = true;
            }
            
            for (int i = 0; i < playersLeaderboard.Length - 1; i++)
            {
                for (int j = 0; j < playersLeaderboard.Length - 1; j++)
                {
                    if (playersLeaderboard[j].GetComponent<Player_Info_Ingame>().lap_count < playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().lap_count)
                    {
                        GameObject tmp = playersLeaderboard[j + 1];
                        playersLeaderboard[j + 1] = playersLeaderboard[j];
                        playersLeaderboard[j] = tmp;
                    }
                    else if ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().lap_count == playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().lap_count)
                        && (playersLeaderboard[j].GetComponent<Player_Info_Ingame>().nextCheckpointNumber < playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().nextCheckpointNumber))
                    {
                        GameObject tmp = playersLeaderboard[j + 1];
                        playersLeaderboard[j + 1] = playersLeaderboard[j];
                        playersLeaderboard[j] = tmp;
                    }
                    else if ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().lap_count == playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().lap_count)
                        && (playersLeaderboard[j].GetComponent<Player_Info_Ingame>().nextCheckpointNumber == playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().nextCheckpointNumber)
                        && ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[(nextCheckpointNumber-1)*2]+ playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[((nextCheckpointNumber - 1) * 2)+1])
                            >
                            (playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().distanceToWaypoint[(nextCheckpointNumber-1)*2])+ playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().distanceToWaypoint[((nextCheckpointNumber - 1) * 2)+1]))
                    {
                        GameObject tmp = playersLeaderboard[j + 1];
                        playersLeaderboard[j + 1] = playersLeaderboard[j];
                        playersLeaderboard[j] = tmp;
                    }
                }
            }
            for (int i = 0; i < playersLeaderboard.Length; i++)
            {
                if (playersLeaderboard[i].GetComponent<Player_Info_Ingame>().isLocalPlayer)
                    playersLeaderboard[i].GetComponent<Player_Info_Ingame>().leaderboardPosition = i + 1;
            }
            GameObject.Find("PositionText").GetComponent<Text>().text = leaderboardPosition.ToString();
            //Debug.Log(leaderboardPosition);
        }
    }
}
