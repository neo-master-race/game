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
    public string roomIndex;
    public RoomType room;
    public Circuit[] circuits;
    public int currentPlayersNb;
    public int MaximumPlayersNb;
    public string[] ActivePlayers;
    public RoomAccesibility roomAccessibility;
    public int[] playersRaceNb;
    public int[] playersRaceWin;
    public string[] playersRaceRecord;
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

    public void roomConstructor(string id, int room_type, int id_circuit,int max_players, int nb_players,
        string[] players_username,int[] players_nb_races,int[] players_nb_wins, string[] players_record)
    {

        Room currRoom=new Room();

        
        currRoom.roomIndex = id;

        // which room type
        switch (room_type) {
            case 2:
                currRoom.room = RoomType.Tournament;
                break;
            default:
                currRoom.room = RoomType.SingleRace;
                break;
        }

        currRoom.circuits = new Circuit[1];
        // which circuit
        switch (id_circuit) {
            case 2:
                currRoom.circuits[0] = Circuit.Track2;
                break;
            case 3:
                currRoom.circuits[0] = Circuit.Track3;
                break;
            default:
                currRoom.circuits[0] = Circuit.Track1;
                break;
        }


        currRoom.MaximumPlayersNb = max_players;
        currRoom.currentPlayersNb = nb_players;
        currRoom.ActivePlayers = players_username;
        currRoom.roomAccessibility = RoomAccesibility.Public;
        currRoom.playersRaceNb = players_nb_races;
        currRoom.playersRaceWin = players_nb_wins;
        currRoom.playersRaceRecord = players_record;

        
        rooms.Add(currRoom);

        
        createRooms();

        
    }

    public void notOnRoomList()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().isOnRoomList = false;
    }

    public void network_call()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().isOnRoomList = true;
        GameObject.Find("Network").GetComponent<Network>().roomListRequest();
    }


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
        {
            room.transform.Find("RaceMode").GetComponent<Text>().text = "TOURNOI";
            if (rooms[roomIndex].circuits[0] == Circuit.Track1)
                room.transform.Find("Circuit1").GetComponent<RawImage>().texture = track1Texture;
            else if (rooms[roomIndex].circuits[0] == Circuit.Track2)
                room.transform.Find("Circuit1").GetComponent<RawImage>().texture = track2Texture;
            else if (rooms[roomIndex].circuits[0] == Circuit.Track3)
                room.transform.Find("Circuit1").GetComponent<RawImage>().texture = track3Texture;

            if (rooms[roomIndex].circuits[1] == Circuit.Track1)
                room.transform.Find("Circuit2").GetComponent<RawImage>().texture = track1Texture;
            else if (rooms[roomIndex].circuits[1] == Circuit.Track2)
                room.transform.Find("Circuit2").GetComponent<RawImage>().texture = track2Texture;
            else if (rooms[roomIndex].circuits[1] == Circuit.Track3)
                room.transform.Find("Circuit2").GetComponent<RawImage>().texture = track3Texture;

            if (rooms[roomIndex].circuits[2] == Circuit.Track1)
                room.transform.Find("Circuit3").GetComponent<RawImage>().texture = track1Texture;
            else if (rooms[roomIndex].circuits[2] == Circuit.Track2)
                room.transform.Find("Circuit3").GetComponent<RawImage>().texture = track2Texture;
            else if (rooms[roomIndex].circuits[2] == Circuit.Track3)
                room.transform.Find("Circuit3").GetComponent<RawImage>().texture = track3Texture;
        }
        room.transform.Find("PlayerNB").GetComponent<Text>().text = rooms[roomIndex].currentPlayersNb + "/" + rooms[roomIndex].MaximumPlayersNb;
    }


    public void createRooms()
    {
        for (int i = roomParent.transform.childCount; i < rooms.Count; i++)
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
            roomParent.GetComponent<RectTransform>().offsetMin = new Vector2(0, - 105 * (rooms.Count - 4)+ roomParent.GetComponent<RectTransform>().offsetMax.y);
    }


    // Use this for initialization
    void Start () {
        createRooms();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
