using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class track_selection_form : MonoBehaviour {

	public Texture circuit1;
	public Texture circuit2;
	public Texture circuit3;
	public Text TrackName;
	public GameObject previous_Track;
	public GameObject TrackVisible;
	public GameObject next_Track;
	public GameObject previous_Lap;
	public Text Lap;
	public GameObject next_Lap;
	public int Difficulty;
	public Text TrackInfo;
	public int nbTrack;
	private int nbLaps;
	private int MaxTrack=3;

	// Use this for initialization
	void Start () {
		nbTrack = 1;
		nbLaps = 3;
	}

	public void previousTrack ()
	{
		nbTrack = (nbTrack - 1) % MaxTrack;
		if (nbTrack <= 0)
			nbTrack += MaxTrack;
		trackUpdate ();
	}

	public void nextTrack ()
	{
		nbTrack = (nbTrack % MaxTrack) + 1;
		trackUpdate ();
	}

	public void trackUpdate ()
	{
		if (nbTrack == 1) {
			TrackName.text = "Euromir";
			TrackVisible.transform.GetComponent<RawImage>().texture = circuit1;
			Difficulty = 1;
			//TrackInfo.text = "Euromir";
		}
		if (nbTrack == 2) {
			TrackName.text = "Monaco";
			TrackVisible.transform.GetComponent<RawImage>().texture = circuit2;
			Difficulty = 2;
			//TrackInfo.text = "Monaco";
		}
		if (nbTrack == 3) {
			TrackName.text = "Track 3";
			TrackVisible.transform.GetComponent<RawImage>().texture = circuit3;
			Difficulty = 3;
			//TrackInfo.text = "Track 3";
		}
	}

	public void previousLap ()
	{
		if (nbLaps != 1) {
			nbLaps--;
			Lap.text = nbLaps.ToString();
		}
	}

	public void nextLap()
	{
		if (nbLaps != 5) {
			nbLaps++;
			Lap.text = nbLaps.ToString();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
