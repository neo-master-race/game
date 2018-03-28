using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int leaderboardPosition;
    public int currentItemIndex;
    [Space(5)]
    public float[] lapTimes;
    public float bestLapTime;

    // Use this for initialization
    void Start () {
        wentThrough = new bool[GameObject.Find("Checkpoints").GetComponent<Checkpoints_Check>().checkpoints_collider.Length];
        distanceToWaypoint = new float[GameObject.Find("Checkpoints").GetComponent<Checkpoints_Check>().wayPoints.Length];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
