using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class room_creation_form : MonoBehaviour {

    public Room currentRoomCreated;

    public string toggleIsOn = "Single";
    public string toggle2IsOn = "Public";

    public int maxPlayers = 2;

    // Use this for initialization
    void Start () {
		
	}

    public void toggleSingleRace()
    {
        if(toggleIsOn=="Single")
            GameObject.Find("ToggleSingleRace").GetComponent<Toggle>().isOn = true;
        else
        {
            toggleIsOn = "Single";
            GameObject.Find("ToggleTournament").GetComponent<Toggle>().isOn = false;
        }
    }

    public void toggleTournament()
    {
        if (toggleIsOn == "Tournament")
            GameObject.Find("ToggleTournament").GetComponent<Toggle>().isOn = true;
        else
        {
            toggleIsOn = "Tournament";
            GameObject.Find("ToggleSingleRace").GetComponent<Toggle>().isOn = false;
        }
    }

    public void togglePublicRoom()
    {
        if (toggle2IsOn == "Public")
            GameObject.Find("TogglePublicLobby").GetComponent<Toggle>().isOn = true;
        else
        {
            toggle2IsOn = "Public";
            GameObject.Find("TogglePrivateLobby").GetComponent<Toggle>().isOn = false;
        }
    }

    public void togglePrivateRoom()
    {
        if (toggle2IsOn == "Private")
            GameObject.Find("TogglePrivateLobby").GetComponent<Toggle>().isOn = true;
        else
        {
            toggle2IsOn = "Private";
            GameObject.Find("TogglePublicLobby").GetComponent<Toggle>().isOn = false;
        }
    }

    public void changeMaxPlayersNb (Object hittedText)
    {
        if (hittedText.name == "2PlayersText")
            maxPlayers = 2;
        else if (hittedText.name == "3PlayersText")
            maxPlayers = 3;
        else if (hittedText.name == "4PlayersText")
            maxPlayers = 4;
    }

    public void OnPointer_Click()
    {
        switch (this.name)
        {
            case "RoomCreationSubmitText":
                currentRoomCreated.roomIndex = GameObject.Find("Rooms_Script").GetComponent<room_info_container>().rooms.Count;

                if(toggleIsOn=="Single")
                {
                    currentRoomCreated.room = RoomType.SingleRace;
                    currentRoomCreated.circuits = new Circuit[1];
                }
                else
                {
                    currentRoomCreated.room = RoomType.Tournament;
                    currentRoomCreated.circuits = new Circuit[3];
                }

                currentRoomCreated.circuits[0] = Circuit.Track1;
                currentRoomCreated.currentPlayersNb = 1;
                currentRoomCreated.MaximumPlayersNb = maxPlayers;
                currentRoomCreated.ActivePlayers = new string[maxPlayers];
                currentRoomCreated.ActivePlayers[0] = "localplayer";
                if (toggleIsOn == "Public")
                    currentRoomCreated.roomAccessibility = RoomAccesibility.Public;
                else
                    currentRoomCreated.roomAccessibility = RoomAccesibility.Private;

                GameObject.Find("Rooms_Script").GetComponent<room_info_container>().rooms.Add(currentRoomCreated);
                GameObject.Find("Rooms_Script").GetComponent<room_info_container>().createRooms();
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
