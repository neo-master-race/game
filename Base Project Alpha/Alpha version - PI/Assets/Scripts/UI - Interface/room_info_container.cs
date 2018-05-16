using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject roomstatetext;

    [Header("Circuit Images")]
    public Texture track1Texture;
    public Texture track2Texture;
    public Texture track3Texture;

    [Header("Room Creation")]
    public List<Room> rooms;

    [Header("GameObjects")]
    public GameObject lobby;
    public GameObject room;
    public GameObject scrollbar;
    public GameObject player1OnLobby;
    public GameObject player2OnLobby;
    public GameObject player3OnLobby;
    public GameObject player4OnLobby;
    public GameObject player1InWait;
    public GameObject player2InWait;
    public GameObject player3InWait;
    public GameObject player4InWait;

    [Header("Star Textures")]
    public Texture filledStar;
    public Texture unfilledStar;

    public void roomConstructor(string id, int room_type, int id_circuit, int max_players, int nb_players,
        string[] players_username, int[] players_nb_races, int[] players_nb_wins, string[] players_record)
    {

        Room currRoom = new Room();


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

    public void addOrUpdateRoom(string id, int room_type, int id_circuit, int max_players, int nb_players,
        string[] players_username, int[] players_nb_races, int[] players_nb_wins, string[] players_record)
    {
        bool found = false;

        for (int i=0; i<rooms.Count; i++)
        {
            if (rooms[i].roomIndex == id)
            {
                switch (room_type)
                {
                    case 2:
                        rooms[i].room = RoomType.Tournament;
                        break;
                    default:
                        rooms[i].room = RoomType.SingleRace;
                        break;
                }

                // which circuit
                switch (id_circuit)
                {
                    case 2:
                        rooms[i].circuits[0] = Circuit.Track2;
                        break;
                    case 3:
                        rooms[i].circuits[0] = Circuit.Track3;
                        break;
                    default:
                        rooms[i].circuits[0] = Circuit.Track1;
                        break;
                }


                rooms[i].MaximumPlayersNb = max_players;
                rooms[i].currentPlayersNb = nb_players;
                rooms[i].ActivePlayers = players_username;
                rooms[i].roomAccessibility = RoomAccesibility.Public;
                rooms[i].playersRaceNb = players_nb_races;
                rooms[i].playersRaceWin = players_nb_wins;
                rooms[i].playersRaceRecord = players_record;
                createRooms();

                found = true;
                break;
            }
        }

        if (!found)
        {
            roomConstructor(id, room_type, id_circuit, max_players, nb_players, players_username, players_nb_races, players_nb_wins, players_record);
        }

        if(GameObject.Find("UserStats").GetComponent<UserStats>().isOnLobby)
        {
            for (int j = 0; j < rooms.Count; j++)
            {
                if (rooms[j].roomIndex == GameObject.Find("UserStats").GetComponent<UserStats>().inLobby)
                {
                    for (int i = 0; i < rooms[j].MaximumPlayersNb; i++)
                    {
                        if (i < rooms[j].ActivePlayers.Length)
                        {
                            GameObject currentPlayer= player1OnLobby;
                            if(i==0)
                            {
                                currentPlayer = player1OnLobby;
                                player1OnLobby.SetActive(true);
                            }
                                
                            if (i == 1)
                            {
                                currentPlayer = player2OnLobby;
                                player2OnLobby.SetActive(true);
                            }
                            if (i == 2)
                            {
                                currentPlayer = player3OnLobby;
                                player3OnLobby.SetActive(true);
                            }
                            if (i == 3)
                            {
                                currentPlayer = player4OnLobby;
                                player4OnLobby.SetActive(true);
                            }
                            currentPlayer.GetComponent<Text>().text = rooms[j].ActivePlayers[i];
                            currentPlayer.transform.Find("Player1RacesImage/Player1RacesText").GetComponent<Text>().text = rooms[j].playersRaceNb[i].ToString();
                            currentPlayer.transform.Find("Player1RaceWinImage/Player1RaceWinText").GetComponent<Text>().text = rooms[j].playersRaceWin[i].ToString();
                            currentPlayer.transform.Find("Player1RaceWinImage/Player1RaceWinTextPourcent").GetComponent<Text>().text = "(" + (100f * ((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i])).ToString("F0") + "%)";
                            currentPlayer.transform.Find("TrackOneLapRecord_Time").GetComponent<Text>().text = rooms[j].playersRaceRecord[i];

                            if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.8f && rooms[j].playersRaceNb[i] >= 50)
                                currentPlayer.transform.Find("Stars/Star5").GetComponent<RawImage>().texture = filledStar;
                            else
                                currentPlayer.transform.Find("Stars/Star5").GetComponent<RawImage>().texture = unfilledStar;

                            if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.6f && rooms[j].playersRaceNb[i] >= 30)
                                currentPlayer.transform.Find("Stars/Star4").GetComponent<RawImage>().texture = filledStar;
                            else
                                currentPlayer.transform.Find("Stars/Star4").GetComponent<RawImage>().texture = unfilledStar;

                            if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.4f && rooms[j].playersRaceNb[i] >= 15)
                                currentPlayer.transform.Find("Stars/Star3").GetComponent<RawImage>().texture = filledStar;
                            else
                                currentPlayer.transform.Find("Stars/Star3").GetComponent<RawImage>().texture = unfilledStar;

                            if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.2f && rooms[j].playersRaceNb[i] >= 5)
                                currentPlayer.transform.Find("Stars/Star2").GetComponent<RawImage>().texture = filledStar;
                            else
                                currentPlayer.transform.Find("Stars/Star2").GetComponent<RawImage>().texture = unfilledStar;

                            if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.1f)
                                currentPlayer.transform.Find("Stars/Star1").GetComponent<RawImage>().texture = filledStar;
                            else
                                currentPlayer.transform.Find("Stars/Star1").GetComponent<RawImage>().texture = unfilledStar;

                            currentPlayer.transform.parent.transform.Find("PlayerNB").GetComponent<Text>().text = rooms[j].ActivePlayers.Length + "/" + rooms[j].MaximumPlayersNb + "\nJoueurs";
                        }
                        else
                        {
                            GameObject currentPlayer = player1InWait;
                            if (i == 0)
                            {
                                currentPlayer = player1InWait;
                                player1InWait.SetActive(true);
                            }

                            if (i == 1)
                            {
                                currentPlayer = player2InWait;
                                player2InWait.SetActive(true);
                            }
                            if (i == 2)
                            {
                                currentPlayer = player3InWait;
                                player3InWait.SetActive(true);
                            }
                            if (i == 3)
                            {
                                currentPlayer = player4InWait;
                                player4InWait.SetActive(true);
                            }
                            currentPlayer.transform.GetChild(0).GetComponent<Text>().text = "Joueur " + (i + 1) + "\n\nEn attente";

                        }
                    }
                }
            }
        }
    }

    public void goToLobby(string id, int room_type, int id_circuit, int max_players, int nb_players,
        string[] players_username, int[] players_nb_races, int[] players_nb_wins, string[] players_record)
    {
        lobby.SetActive(true);
        room.SetActive(false);
        scrollbar.SetActive(false);
        GameObject.Find("UserStats").GetComponent<UserStats>().inLobby = id;
        GameObject.Find("UserStats").GetComponent<UserStats>().isOnLobby = true;
        GameObject.Find("UserStats").GetComponent<UserStats>().isOnRoomList = false;



        for (int j=0;j<rooms.Count;j++)
        {
            if (rooms[j].roomIndex==GameObject.Find("UserStats").GetComponent<UserStats>().inLobby)
            {
                for (int i = 0; i < rooms[j].MaximumPlayersNb; i++)
                {
                    if (i < rooms[j].ActivePlayers.Length)
                    {
                        GameObject currentPlayer = player1OnLobby;
                        if (i == 0)
                        {
                            currentPlayer = player1OnLobby;
                            player1OnLobby.SetActive(true);
                        }

                        if (i == 1)
                        {
                            currentPlayer = player2OnLobby;
                            player2OnLobby.SetActive(true);
                        }
                        if (i == 2)
                        {
                            currentPlayer = player3OnLobby;
                            player3OnLobby.SetActive(true);
                        }
                        if (i == 3)
                        {
                            currentPlayer = player4OnLobby;
                            player4OnLobby.SetActive(true);
                        }
                        currentPlayer.GetComponent<Text>().text = rooms[j].ActivePlayers[i];
                        currentPlayer.transform.Find("Player1RacesImage/Player1RacesText").GetComponent<Text>().text = rooms[j].playersRaceNb[i].ToString();
                        currentPlayer.transform.Find("Player1RaceWinImage/Player1RaceWinText").GetComponent<Text>().text = rooms[j].playersRaceWin[i].ToString();
                        currentPlayer.transform.Find("Player1RaceWinImage/Player1RaceWinTextPourcent").GetComponent<Text>().text = "(" + (100f * ((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i])).ToString("F0") + "%)";
                        currentPlayer.transform.Find("TrackOneLapRecord_Time").GetComponent<Text>().text = rooms[j].playersRaceRecord[i];

                        if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.8f && rooms[j].playersRaceNb[i] >= 50)
                            currentPlayer.transform.Find("Stars/Star5").GetComponent<RawImage>().texture = filledStar;
                        else
                            currentPlayer.transform.Find("Stars/Star5").GetComponent<RawImage>().texture = unfilledStar;

                        if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.6f && rooms[j].playersRaceNb[i] >= 30)
                            currentPlayer.transform.Find("Stars/Star4").GetComponent<RawImage>().texture = filledStar;
                        else
                            currentPlayer.transform.Find("Stars/Star4").GetComponent<RawImage>().texture = unfilledStar;

                        if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.4f && rooms[j].playersRaceNb[i] >= 15)
                            currentPlayer.transform.Find("Stars/Star3").GetComponent<RawImage>().texture = filledStar;
                        else
                            currentPlayer.transform.Find("Stars/Star3").GetComponent<RawImage>().texture = unfilledStar;

                        if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.2f && rooms[j].playersRaceNb[i] >= 5)
                            currentPlayer.transform.Find("Stars/Star2").GetComponent<RawImage>().texture = filledStar;
                        else
                            currentPlayer.transform.Find("Stars/Star2").GetComponent<RawImage>().texture = unfilledStar;

                        if (((float)rooms[j].playersRaceWin[i] / (float)rooms[j].playersRaceNb[i]) > 0.1f)
                            currentPlayer.transform.Find("Stars/Star1").GetComponent<RawImage>().texture = filledStar;
                        else
                            currentPlayer.transform.Find("Stars/Star1").GetComponent<RawImage>().texture = unfilledStar;

                        currentPlayer.transform.parent.transform.Find("PlayerNB").GetComponent<Text>().text = rooms[j].ActivePlayers.Length + "/" + rooms[j].MaximumPlayersNb + "\nJoueurs";
                        Debug.Log("heu" + i);
                    }
                    else if(i >= rooms[j].ActivePlayers.Length)
                    {
                        Debug.Log(i);
                        Debug.Log(rooms[j].MaximumPlayersNb);
                        GameObject currentPlayer = player1InWait;
                        if (i == 0)
                        {
                            currentPlayer = player1InWait;
                            player1InWait.SetActive(true);
                        }

                        if (i == 1)
                        {
                            currentPlayer = player2InWait;
                            player2InWait.SetActive(true);
                        }
                        if (i == 2)
                        {
                            currentPlayer = player3InWait;
                            player3InWait.SetActive(true);
                        }
                        if (i == 3)
                        {
                            currentPlayer = player4InWait;
                            player4InWait.SetActive(true);
                        }
                        currentPlayer.transform.GetChild(0).GetComponent<Text>().text = "Joueur " + (i + 1) + "\n\nEn attente";
                    }
                }
            }
        }     
    }

    public void startGame()
    {
        SceneManager.LoadScene("trackCommon", LoadSceneMode.Single);
        SceneManager.LoadScene("Track1", LoadSceneMode.Additive);
    }

    public IEnumerator roomStartCountdown(float waitTime)
    {
        while (waitTime>=0.0f)
        {
            roomstatetext.GetComponent<Text>().text = "La partie démarre dans " + (int)waitTime + " secondes.";
            yield return new WaitForSeconds(1.0f);
            waitTime -= 1.0f;
        }
        startGame();
        yield break;
    }

    public void reset_list()
    {
        this.rooms = new List<Room>(new Room[0]);
    }

    public void notOnRoomList()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().isOnRoomList = false;
        reset_list();
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
        for(int i =0;i< rooms[roomIndex].ActivePlayers.Length;i++)
        {
            room.transform.Find("Player" + (i + 1)).GetComponent<Text>().text = "- "+rooms[roomIndex].ActivePlayers[i];
        }
        room.transform.GetChild(0).GetComponent<room_index_container>().roomIndex= rooms[roomIndex].roomIndex;
    }


    public void reset_list_visual()
    {
        roomParent.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        roomParent.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        for (int i = 0; i < roomParent.transform.childCount; i++)
        {
            Destroy(roomParent.transform.GetChild(0).gameObject);
        }
    }


    public void createRooms()
    {
        reset_list_visual();
        for (int i = 0; i < rooms.Count; i++)
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
