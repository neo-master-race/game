using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class room_creation_form : MonoBehaviour {

    public int roomIndex;
    public RoomType roomType;
    public Circuit[] circuits;
    public int currentPlayersNb;
    public int MaximumPlayersNb;
    public string[] ActivePlayers;
    public RoomAccesibility roomAccessibility;

    public Room currentRoomCreated;

    public string toggleIsOn = "Single";
    public string toggle2IsOn = "Public";

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

    public void OnPointer_Click()
    {
        switch (this.name)
        {
            case "RoomCreationSubmitText":
                //currentRoomCreated.roomType=
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
