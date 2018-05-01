using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats : MonoBehaviour {

    [Header("User Informations")]
    public string username;

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
	
	// Update is called once per frame
	void Update () {
		
	}
}
