using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Room Creation GameObjects")]
    public GameObject roomParent;
    public GameObject roomSinglePrefab;
    public GameObject roomTournamentPrefab;

    [Header("Circuit Images")]
    public Texture track1Texture;
    public Texture track2Texture;
    public Texture track3Texture;

    [Header("Room Creation")]
    public List<Room> rooms;


    void setRoomAttributes(GameObject room,string roomGameMode,int roomIndex)
    {
        if(roomGameMode== "SingleRace")
        {
            room.transform.Find("RaceMode").GetComponent<Text>().text = "COURSE SIMPLE";
            if (rooms[roomIndex].circuits[0] == Circuit.Track1)
                room.transform.Find("Circuit").GetComponent<RawImage>().texture = track1Texture;
            else if (rooms[roomIndex].circuits[0] == Circuit.Track2)
                room.transform.Find("Circuit").GetComponent<RawImage>().texture = track2Texture;
            else if (rooms[roomIndex].circuits[0] == Circuit.Track3)
                room.transform.Find("Circuit").GetComponent<RawImage>().texture = track3Texture;
        }
        else
            room.transform.Find("RaceMode").GetComponent<Text>().text = "TOURNOI";
        room.transform.Find("PlayerNB").GetComponent<Text>().text = rooms[roomIndex].currentPlayersNb + "/" + rooms[roomIndex].MaximumPlayersNb;
    }


    public void createRooms()
    {
        for (int i = GameObject.Find("Rooms").transform.childCount; i < rooms.Count; i++)
        {
            if (rooms[i].room == RoomType.SingleRace)
            {
                GameObject singleRaceRoom = Instantiate(roomSinglePrefab, roomParent.transform);
                setRoomAttributes(singleRaceRoom, "SingleRace", i);
                singleRaceRoom.transform.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2
                    (
                        singleRaceRoom.transform.GetComponent<RectTransform>().anchoredPosition.x,
                        -25 - 105 * i
                    );
            }
            else
            {
                GameObject tournamentRoom = Instantiate(roomTournamentPrefab, roomParent.transform);
                setRoomAttributes(tournamentRoom, "Tournament", i);
                tournamentRoom.transform.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2
                    (
                        tournamentRoom.transform.GetComponent<RectTransform>().anchoredPosition.x,
                        -25 - 105 * i
                    );
            }
        }
        if (rooms.Count > 4)
            GameObject.Find("Rooms").GetComponent<RectTransform>().offsetMin = new Vector2(0, - 105 * (rooms.Count - 4)+ GameObject.Find("Rooms").GetComponent<RectTransform>().offsetMax.y);
    }


    // Use this for initialization
    void Start () {
        createRooms();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
