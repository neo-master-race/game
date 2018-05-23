using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_info_update : MonoBehaviour {

	public Text info;

	// Use this for initialization
	void Start () {
		GameObject.Find ("Network").GetComponent<Network> ().getGlobalRecord (1);
		GameObject.Find ("Network").GetComponent<Network> ().getGlobalRecord (2);
		GameObject.Find ("Network").GetComponent<Network> ().getGlobalRecord (3);
    }

    public void OnPointerClick()
	{
		string diff1 = "--:--:--";
		string diff2 = "--:--:--";
		string diff3 = "--:--:--";
		string extra1 = "";
		string extra2 = "";
		string extra3 = "";

		string t1time = GameObject.Find ("UserStats").GetComponent<UserStats> ().track1LapRecord;
		string r1time = GameObject.Find ("UserStats").GetComponent<UserStats> ().track1WorldRecord;
		if (string.Compare (t1time, "--:--:--") != 0) {
			int t1m = int.Parse (t1time.Split (':') [0]);
			int t1s = int.Parse (t1time.Split (':') [1]);
			int t1ms = int.Parse (t1time.Split (':') [2]);

			int r1m = int.Parse (r1time.Split (':') [0]);
			int r1s = int.Parse (r1time.Split (':') [1]);;
			int r1ms = int.Parse (r1time.Split (':') [2]);;

			int d1m = t1m-r1m;
			int d1s = ((t1s-r1s) % 60 + 60) %60;
			int d1ms = ((t1ms-r1ms) % 60 + 60) %60;

			if (d1m < 10)
				extra1 = "0";
			else
				extra1 = "";

			if (d1s < 10)
				extra2 = "0";
			else
				extra2 = "";

			if (d1ms < 10)
				extra3 = "0";
			else
				extra3 = "";
			
			diff1 = extra1 + d1m + ":" + extra2 + d1s + ":" + extra3 + d1ms;
		}

		string t2time = GameObject.Find ("UserStats").GetComponent<UserStats> ().track2LapRecord;
		string r2time = GameObject.Find ("UserStats").GetComponent<UserStats> ().track2WorldRecord;
		if (string.Compare (t2time, "--:--:--") != 0) {
			int t2m = int.Parse (t2time.Split (':') [0]);
			int t2s = int.Parse (t2time.Split (':') [1]);
			int t2ms = int.Parse (t2time.Split (':') [2]);

			int r2m = int.Parse (r2time.Split (':') [0]);
			int r2s = int.Parse (r2time.Split (':') [1]);;
			int r2ms = int.Parse (r2time.Split (':') [2]);;

			int d2m = t2m-r2m;
			int d2s = ((t2s-r2s) % 60 + 60) %60;
			int d2ms = ((t2ms-r2ms) % 60 + 60) %60;

			if (d2m < 10)
				extra1 = "0";
			else
				extra1 = "";

			if (d2s < 10)
				extra2 = "0";
			else
				extra2 = "";

			if (d2ms < 10)
				extra3 = "0";
			else
				extra3 = "";

			diff2 = extra1 + d2m + ":" + extra2 + d2s + ":" + extra3 + d2ms;
		}

		string t3time = GameObject.Find ("UserStats").GetComponent<UserStats> ().track3LapRecord;
		string r3time = GameObject.Find ("UserStats").GetComponent<UserStats> ().track3WorldRecord;
		if (string.Compare (t3time, "--:--:--") != 0) {
			int t3m = int.Parse (t3time.Split (':') [0]);
			int t3s = int.Parse (t3time.Split (':') [1]);
			int t3ms = int.Parse (t3time.Split (':') [2]);

			int r3m = int.Parse (r3time.Split (':') [0]);
			int r3s = int.Parse (r3time.Split (':') [1]);;
			int r3ms = int.Parse (r3time.Split (':') [2]);;

			int d3m = t3m-r3m;
			int d3s = ((t3s-r3s) % 60 + 60) %60;
			int d3ms = ((t3ms-r3ms) % 60 + 60) %60;

			if (d3m < 10)
				extra1 = "0";
			else
				extra1 = "";

			if (d3s < 10)
				extra2 = "0";
			else
				extra2 = "";

			if (d3ms < 10)
				extra3 = "0";
			else
				extra3 = "";

			diff3 = extra1 + d3m + ":" + extra2 + d3s + ":" + extra3 + d3ms;
		}

		info.text = 
			GameObject.Find ("UserStats").GetComponent<UserStats> ().username + "\n" +
			GameObject.Find ("UserStats").GetComponent<UserStats> ().raceNb + "\n" +
			GameObject.Find ("UserStats").GetComponent<UserStats> ().raceVictory + "\n" +
			GameObject.Find ("UserStats").GetComponent<UserStats> ().track1LapRecord + "\n" +
			GameObject.Find ("UserStats").GetComponent<UserStats> ().track2LapRecord + "\n" +
			GameObject.Find ("UserStats").GetComponent<UserStats> ().track3LapRecord + "\n" +
			diff1 + "\n" +
			diff2 + "\n" +
			diff3;
    }

    // Update is called once per frame
    void Update () {
		
	}
}