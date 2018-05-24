//SCHNEBERGER Maxime
//gère le système de checkpoint sur un circuit (modulable/adaptatif) selon le tracé et le nb de checkpoints voulu
//empêche le joueur de tricher sur la complétion d'un tour et l'oblige à suivre le tracé

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints_Check : MonoBehaviour
{

    [Header("Script Source requirement (0 for all on checkpoints object)")]
    public GameObject localPlayer;
    public GameObject checkpointsParentScript;
    public GameObject[] checkpoints_collider;
    public GameObject[] wayPoints;
    public bool initializedWaypointDistances;
    public bool initializedWaypointDistancesConfirmation=false;

    [Space(20)]

    [Header("Checkpoints variables (0 for all on script source)")]
    public bool isStartFinishLine;
    public int checkpointNumber;
    
    private int cpNumber;

    private GameObject[] players;



    void OnTriggerEnter(Collider car)
    {
        if (this.gameObject.GetComponent<Checkpoints_Check>().isStartFinishLine)
        {
            if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2" && !car.GetComponent<Player_Info_Ingame>().hasHitSFLineOnce)
            {
                car.GetComponent<Player_Info_Ingame>().hasHitSFLineOnce = true;
                car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber = 1;
                car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = 1;
            }
            else
            {
                if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2" && (car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber
                    && car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == car.GetComponent<Player_Info_Ingame>().wentThrough.Length))
                {
                    car.GetComponent<Player_Info_Ingame>().cp_count++;
                    car.GetComponent<Player_Info_Ingame>().wentThrough[car.GetComponent<Player_Info_Ingame>().wentThrough.Length - 1] = true;
                }

                /*for (int i = 0; i <= car.GetComponent<Player_Info_Ingame>().wentThrough.Length - 1; i++)
                {
                    if (car.GetComponent<Player_Info_Ingame>().wentThrough[i] == true)
                        car.GetComponent<Player_Info_Ingame>().cp_count++;
                }*/
                if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2" && (car.GetComponent<Player_Info_Ingame>().cp_count == car.GetComponent<Player_Info_Ingame>().wentThrough.Length))
                {

                    for (int i = 0; i < players.Length; i++)
                    {
                        if (car.gameObject == players[i])
                        {
                            GameObject.Find("TimeCounter").GetComponent<Timer>().resetTimer(i);
                            if (car.GetComponent<Player_Info_Ingame>().lap_count == GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceLapNumber)
                            {
                                GameObject.Find("TimeCounter").GetComponent<Timer>().stopGlobalTimer(i);
                                GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceMinimumLeaderboardPosition++;
                            }
                        }
                            
                    }
                    if(!(car.GetComponent<Player_Info_Ingame>().lap_count == GameObject.Find("RaceInformations").GetComponent<RaceInformations>().raceLapNumber))
                    {

                        car.GetComponent<Player_Info_Ingame>().lap_count++;
                        car.GetComponent<Player_Info_Ingame>().virtual_lap_count++;

                        if (car.GetComponent<Player_Info_Ingame>().isLocalPlayer)
                            GameObject.Find("LapCounter").GetComponent<LapCount>().setCurrentLap(car.GetComponent<Player_Info_Ingame>().lap_count);
                        car.GetComponent<Player_Info_Ingame>().cp_count = 0;
                        for (int j = 0; j <= car.GetComponent<Player_Info_Ingame>().wentThrough.Length - 1; j++)
                            car.GetComponent<Player_Info_Ingame>().wentThrough[j] = false;
                        car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber = 1;
                        //GameObject.Find("Race_Control").GetComponent<player_info_server>().players_laps[0] = 5;
                        if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2")
                            car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = 1;
                    }
                }
            }
        }
        else
        {
            if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2" && (car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber
                && car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber))
            {
                car.GetComponent<Player_Info_Ingame>().wentThrough[this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber - 1] = true;
                car.GetComponent<Player_Info_Ingame>().nextCheckpointNumber++;
                car.GetComponent<Player_Info_Ingame>().cp_count++;
                car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber + 1;
            }
            else if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2" && (car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber != this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber
                && car.GetComponent<Player_Info_Ingame>().secondLastHittedCP > car.GetComponent<Player_Info_Ingame>().lastHittedCP))
            {
                car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber;
                car.GetComponent<Player_Info_Ingame>().secondLastHittedCP = car.GetComponent<Player_Info_Ingame>().lastHittedCP;
                car.GetComponent<Player_Info_Ingame>().lastHittedCP= this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber;
            }
            else
            {
                if (car.name != "frontColliderR" && car.name != "frontColliderL" && car.name != "frontColliderRight" && car.name != "frontColliderLeft" && car.name != "Front_Collider1" && car.name != "Front_Collider2")
                {
                    car.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber + 1;
                    car.GetComponent<Player_Info_Ingame>().secondLastHittedCP = car.GetComponent<Player_Info_Ingame>().lastHittedCP;
                    car.GetComponent<Player_Info_Ingame>().lastHittedCP = this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber;
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        cpNumber = checkpoints_collider.Length;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
                localPlayer = player;
        }
    }
	
	// Update is called once per frame
	void Update () {
        players = GameObject.Find("RaceInformations").GetComponent<RaceInformations>().players;

        foreach (GameObject player in players)
        {
            for (int i = 0; i < cpNumber; i++)
            {
                if (i == (player.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber - 1))
                {
                    player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[2*i] = Vector3.Distance(wayPoints[2*i].transform.position, player.transform.position);
                    player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[2*i+1] = Vector3.Distance(wayPoints[2*i+1].transform.position, player.transform.position);
                }
                else
                {
                    player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[2 * i] = 0.0f;
                    player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[2 * i + 1] = 0.0f;
                }  
            }
        }
        if(!initializedWaypointDistances)
            initializedWaypointDistances = true;

    }
}
