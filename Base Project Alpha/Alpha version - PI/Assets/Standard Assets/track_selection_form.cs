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
	public int nbLaps;
	private int MaxTrack=3;
	public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;

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
			Difficulty = 2;
			Star1.SetActive (true);
			Star2.SetActive (true);
			Star3.SetActive (false);
			TrackInfo.text = "Euromir propose une balance entre phases d’accélérations et parties techniques, permettant aux débutants d’apprendre à maitriser la voiture, avec un peu de défis.";
		}
		if (nbTrack == 2) {
			TrackName.text = "Monaco";
			TrackVisible.transform.GetComponent<RawImage>().texture = circuit2;
			Difficulty = 3;
			Star1.SetActive (true);
			Star2.SetActive (true);
			Star3.SetActive (true);
			TrackInfo.text = "Monaco est quasiment identiques à son homologue réel sans les virages incurvés. Il comporte de nombreux passage très punitif si mal gérés.";
		}
		if (nbTrack == 3) {
			TrackName.text = "Super 8";
			TrackVisible.transform.GetComponent<RawImage>().texture = circuit3;
			Difficulty = 1;
			Star1.SetActive (true);
			Star2.SetActive (false);
			Star3.SetActive (false);
			TrackInfo.text = "Super8 est composé de larges virages et de lignes droites permettant aux débutants de bien se familiariser avec les contrôles du véhicule en virage. Il possède aussi un saut.";
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
