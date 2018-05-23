using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_info_update : MonoBehaviour {

	public Text info;

	// Use this for initialization
	void Start () {
    }

    public void OnPointerClick()
    {
		info.text = 
		GameObject.Find ("UserStats").GetComponent<UserStats> ().username + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().raceNb + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().raceVictory + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().track1LapRecord + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().track2LapRecord + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().track3LapRecord + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().track1LapRecord + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().track2LapRecord + "\n" +
		GameObject.Find ("UserStats").GetComponent<UserStats> ().track3LapRecord + "\n";
    }

    // Update is called once per frame
    void Update () {
		
	}
}