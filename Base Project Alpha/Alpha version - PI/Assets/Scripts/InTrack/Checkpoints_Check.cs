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

    [Space(20)]

    [Header("Checkpoints variables (0 for all on script source)")]
    public bool isStartFinishLine;
    public int checkpointNumber;
    
    private int cpNumber;

    private GameObject[] players;



    void OnTriggerEnter()
    {
        if (this.gameObject.GetComponent<Checkpoints_Check>().isStartFinishLine)
        {
            if (!localPlayer.GetComponent<Player_Info_Ingame>().hasHitSFLineOnce)
                localPlayer.GetComponent<Player_Info_Ingame>().hasHitSFLineOnce = true;
            else
            {
                if (localPlayer.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == localPlayer.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber
                    && localPlayer.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == localPlayer.GetComponent<Player_Info_Ingame>().wentThrough.Length)
                    localPlayer.GetComponent<Player_Info_Ingame>().wentThrough[localPlayer.GetComponent<Player_Info_Ingame>().wentThrough.Length - 1] = true;
                for (int i = 0; i <= localPlayer.GetComponent<Player_Info_Ingame>().wentThrough.Length - 1; i++)
                {
                    if (localPlayer.GetComponent<Player_Info_Ingame>().wentThrough[i] == true)
                        localPlayer.GetComponent<Player_Info_Ingame>().cp_count++;
                }
                if (localPlayer.GetComponent<Player_Info_Ingame>().cp_count == localPlayer.GetComponent<Player_Info_Ingame>().wentThrough.Length)
                {
                    localPlayer.GetComponent<Player_Info_Ingame>().lap_count++;
                    for (int j = 0; j <= localPlayer.GetComponent<Player_Info_Ingame>().wentThrough.Length - 1; j++)
                        localPlayer.GetComponent<Player_Info_Ingame>().wentThrough[j] = false;
                    localPlayer.GetComponent<Player_Info_Ingame>().nextCheckpointNumber = 1;
                }
                localPlayer.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = 1;
            }
        }
        else
        {
            if (localPlayer.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber
                && localPlayer.GetComponent<Player_Info_Ingame>().nextCheckpointNumber == localPlayer.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber)
            {
                localPlayer.GetComponent<Player_Info_Ingame>().wentThrough[this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber - 1] = true;
                localPlayer.GetComponent<Player_Info_Ingame>().nextCheckpointNumber++;
            }
            localPlayer.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber = this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber + 1;
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
		for(int i=0;i< cpNumber;i++)
        {
            foreach (GameObject player in players)
            {
                if (i == (player.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber - 1))
                    player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[i] = Vector3.Distance(wayPoints[i].transform.position, player.transform.position);
                else
                    player.GetComponent<Player_Info_Ingame>().distanceToWaypoint[i] = 0.0f;
            }
        }
	}
}
