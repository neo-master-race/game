using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum RoomType { SingleRace, Tournament };
public enum Circuit { Track1, Track2, Track3 };
public enum RoomAccesibility { Public, Private };

[System.Serializable]
public class Room
{
    public int roomIndex;
    public RoomType room;
    public Circuit[] circuits;
    public int currentPlayersNb;
    public int MaximumPlayersNb;
    public string[] ActivePlayers;
    public RoomAccesibility roomAccessibility;
}

public class room_info_container : MonoBehaviour {

    public Room[] rooms;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
