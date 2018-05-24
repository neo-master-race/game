using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Info_Ingame : MonoBehaviour {

    [Space(20)]

    public bool isLocalPlayer = false;
    public string userName;

    [Space(30)]

    [Header("current race completion infos")]
    public bool[] wentThrough;
    public int lap_count = 1;
    public int virtual_lap_count = 1;
    public bool hasHitSFLineOnce;
    public int nextCheckpointNumber = 1;
    public int supposedNextCheckpointNumber = 1;
    public int cp_count=0;
    public int lastHittedCP = 0;
    public int secondLastHittedCP = 0;

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
    public string[] playersGlobalTime;
    public string[] playersLapTimes;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Destroy(transform.Find("Particle System").gameObject);
            Destroy(transform.Find("Particle System (1)").gameObject);
        }

        if (GameObject.Find("UserStats").GetComponent<UserStats>().carIndex == 1)
        {
            Destroy(GameObject.Find("Porsche"));
            Destroy(GameObject.Find("Lamborghini"));
            Destroy(GameObject.Find("Ford"));
        }
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().carIndex == 2)
        {
            Destroy(GameObject.Find("Stratos"));
            Destroy(GameObject.Find("Lamborghini"));
            Destroy(GameObject.Find("Ford"));
        }
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().carIndex == 3)
        {
            Destroy(GameObject.Find("Stratos"));
            Destroy(GameObject.Find("Porsche"));
            Destroy(GameObject.Find("Ford"));
        }
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().carIndex == 4)
        {
            Destroy(GameObject.Find("Stratos"));
            Destroy(GameObject.Find("Porsche"));
            Destroy(GameObject.Find("Lamborghini"));
        }
       
        
    }

    // Use this for initialization
    void Start()
    {
        leaderboardPosition = 1;
        wentThrough = new bool[GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().checkpoints_collider.Length];
        distanceToWaypoint = new float[GameObject.FindWithTag("Checkpoints").GetComponent<Checkpoints_Check>().wayPoints.Length];
        players = GameObject.FindGameObjectsWithTag("Player");
        if(GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb==1)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("track1"));
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("track2"));
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("track3"));

        
    }

    public void setpos()
    {
        if (GameObject.Find("UserStats").GetComponent<UserStats>().playingSolo)
            this.transform.position = GameObject.Find("Slot1").transform.position;
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().playingMulti)
        {
            if(GameObject.Find("UserStats").GetComponent<UserStats>().startingPosition==1)
                this.transform.position = GameObject.Find("Slot1").transform.position;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().startingPosition == 2)
                this.transform.position = GameObject.Find("Slot2").transform.position;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().startingPosition == 3)
                this.transform.position = GameObject.Find("Slot3").transform.position;
            else if (GameObject.Find("UserStats").GetComponent<UserStats>().startingPosition == 4)
                this.transform.position = GameObject.Find("Slot4").transform.position;

            
        }
        if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 1)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("track1"));
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 2)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("track2"));
        else if (GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb == 3)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("track3"));

        foreach (GameObject player in GameObject.Find("RaceInformations").GetComponent<RaceInformations>().playerLeaderboard)
        {
            string user = "";
            if (!player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                if (player.GetComponent<Player_Info_Ingame>().leaderboardPosition == GameObject.Find("UserStats").GetComponent<UserStats>().player2sp)
                    user = GameObject.Find("UserStats").GetComponent<UserStats>().player2name;
                if (player.GetComponent<Player_Info_Ingame>().leaderboardPosition == GameObject.Find("UserStats").GetComponent<UserStats>().player3sp)
                    user = GameObject.Find("UserStats").GetComponent<UserStats>().player3name;
                if (player.GetComponent<Player_Info_Ingame>().leaderboardPosition == GameObject.Find("UserStats").GetComponent<UserStats>().player4sp)
                    user = GameObject.Find("UserStats").GetComponent<UserStats>().player4name;
                player.transform.Find("userName/userNameTexture/userNameText").GetComponent<Text>().text = user;
            }

        }

    }

    // Update is called once per frame
    void Update () {
        if (this.gameObject.GetComponent<Player_Info_Ingame>().isLocalPlayer)
        {
            int nb_players_before = players.Length;
            players = GameObject.FindGameObjectsWithTag("Player");
            playersLeaderboard = GameObject.FindGameObjectsWithTag("Player");
            int nb_players_after = players.Length;
            if (nb_players_before != nb_players_after)
            {
                if (nb_players_after - nb_players_before > 0)
                {
                    for (int i = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; i < nb_players_after - nb_players_before; i++)
                    {
                        for (int j = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; j < playersLeaderboard.Length - 1; j++)
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
                for (int i = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; i < playersLeaderboard.Length - 1; i++)
                {
                    for (int j = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; j < playersLeaderboard.Length - 1; j++)
                    {
                        if ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[0] + playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[1])
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
            for (int i = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; i < playersLeaderboard.Length - 1; i++)
            {
                for (int j = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; j < playersLeaderboard.Length - 1; j++)
                {
                    if (playersLeaderboard[j].GetComponent<Player_Info_Ingame>().virtual_lap_count < playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().virtual_lap_count)
                    {
                        GameObject tmp = playersLeaderboard[j + 1];
                        playersLeaderboard[j + 1] = playersLeaderboard[j];
                        playersLeaderboard[j] = tmp;
                    }
                    else if ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().virtual_lap_count == playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().virtual_lap_count)
                        && (playersLeaderboard[j].GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber < playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber))
                    {
                        GameObject tmp = playersLeaderboard[j + 1];
                        playersLeaderboard[j + 1] = playersLeaderboard[j];
                        playersLeaderboard[j] = tmp;
                    }
                    else if ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().virtual_lap_count == playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().virtual_lap_count)
                        && (playersLeaderboard[j].GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber == playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber)
                        && ((playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[(supposedNextCheckpointNumber - 1) * 2] + playersLeaderboard[j].GetComponent<Player_Info_Ingame>().distanceToWaypoint[((supposedNextCheckpointNumber - 1) * 2) + 1])
                            >
                            (playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().distanceToWaypoint[(supposedNextCheckpointNumber - 1) * 2]) + playersLeaderboard[j + 1].GetComponent<Player_Info_Ingame>().distanceToWaypoint[((supposedNextCheckpointNumber - 1) * 2) + 1]))
                    {
                        GameObject tmp = playersLeaderboard[j + 1];
                        playersLeaderboard[j + 1] = playersLeaderboard[j];
                        playersLeaderboard[j] = tmp;
                    }
                }
            }
            for (int i = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition - 1; i < playersLeaderboard.Length; i++)
            {
                playersLeaderboard[i].GetComponent<Player_Info_Ingame>().leaderboardPosition = i + 1;

                if (playersLeaderboard[i].GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber > playersLeaderboard[i].GetComponent<Player_Info_Ingame>().nextCheckpointNumber)
                    playersLeaderboard[i].GetComponent<Player_Info_Ingame>().virtual_lap_count = playersLeaderboard[i].GetComponent<Player_Info_Ingame>().lap_count - 1;
                else
                    playersLeaderboard[i].GetComponent<Player_Info_Ingame>().virtual_lap_count = playersLeaderboard[i].GetComponent<Player_Info_Ingame>().lap_count;
            }
            if (!GameObject.Find("RaceInformations").GetComponent<RaceInformations>().hasFinished && leaderboardPosition >= GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition)
            {
                GameObject.Find("PositionText").GetComponent<Text>().text = leaderboardPosition.ToString();
                GameObject.Find("PositionText").GetComponent<PositionHandler>().setpos(leaderboardPosition);
            }
        }
    }
}
