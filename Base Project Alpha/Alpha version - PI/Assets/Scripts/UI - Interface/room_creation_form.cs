using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class room_creation_form : MonoBehaviour {

    [Header("Room")]
    public Room currentRoomCreated;

    [Header("Room form variables")]
    public GameObject roomCreationForm;
    public string toggleIsOn = "Single";
    public string toggle2IsOn = "Public";
    public int maxPlayers = 2;
    [Space(10)]
    public GameObject players2;
    public GameObject players3;
    public GameObject players4;

    [Header("Room circuit choices")]
    public GameObject CircuitChoice;
    public GameObject Track1Visible;
    public GameObject Track2Visible;
    public GameObject Track3Visible;
    public Texture circuit1;
    public Texture circuit2;
    public Texture circuit3;
    public int actualCircuitSelection = 0;

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
        {
            maxPlayers = 2;
            players2.GetComponent<RawImage>().color = new Color32(0, 255, 44,255);
            players3.GetComponent<RawImage>().color = new Color32(88, 88, 88,255);
            players4.GetComponent<RawImage>().color = new Color32(88, 88, 88,255);
        }
        else if (hittedText.name == "3PlayersText")
        {
            maxPlayers = 3;
            players2.GetComponent<RawImage>().color = new Color32(88, 88, 88, 255);
            players3.GetComponent<RawImage>().color = new Color32(0, 255, 44, 255);
            players4.GetComponent<RawImage>().color = new Color32(88, 88, 88, 255);
        }
        else if (hittedText.name == "4PlayersText")
        {
            maxPlayers = 4;
            players2.GetComponent<RawImage>().color = new Color32(88, 88, 88, 255);
            players3.GetComponent<RawImage>().color = new Color32(88, 88, 88, 255);
            players4.GetComponent<RawImage>().color = new Color32(0, 255, 44, 255);
        }
    }

    public void activateCircuitChoice (GameObject trackSelection)
    {
        CircuitChoice.SetActive(true);
        if (trackSelection.name == "Circuit1ChoiceBackground")
            actualCircuitSelection = 1;
        else if (trackSelection.name == "Circuit2ChoiceBackground")
            actualCircuitSelection = 2;
        else if (trackSelection.name == "Circuit3ChoiceBackground")
            actualCircuitSelection = 3;
    }

    public void CircuitConfirm(GameObject trackTexture)
    {
        if(toggleIsOn=="Single")
        {
            currentRoomCreated.circuits = new Circuit[1];
            if (trackTexture.name == "Track1Image" || trackTexture.name == "Track1Text")
            {
                Track1Visible.transform.GetComponent<RawImage>().texture= circuit1;
                currentRoomCreated.circuits[0] = Circuit.Track1;
            }
            else if (trackTexture.name == "Track2Image" || trackTexture.name == "Track2Text")
            {
                Track1Visible.transform.GetComponent<RawImage>().texture = circuit2;
                currentRoomCreated.circuits[0] = Circuit.Track2;
            }
            else if (trackTexture.name == "Track3Image" || trackTexture.name == "Track3Text")
            {
                Track1Visible.transform.GetComponent<RawImage>().texture = circuit3;
                currentRoomCreated.circuits[0] = Circuit.Track3;
            }
        }
        else
        {
            currentRoomCreated.circuits = new Circuit[3];
            if (actualCircuitSelection == 1)
            {
                if (trackTexture.name == "Track1Image" || trackTexture.name == "Track1Text")
                {
                    Track1Visible.transform.GetComponent<RawImage>().texture = circuit1;
                    currentRoomCreated.circuits[0] = Circuit.Track1;
                }
                else if (trackTexture.name == "Track2Image" || trackTexture.name == "Track2Text")
                {
                    Track1Visible.transform.GetComponent<RawImage>().texture = circuit2;
                    currentRoomCreated.circuits[0] = Circuit.Track2;
                }
                else if (trackTexture.name == "Track3Image" || trackTexture.name == "Track3Text")
                {
                    Track1Visible.transform.GetComponent<RawImage>().texture = circuit3;
                    currentRoomCreated.circuits[0] = Circuit.Track3;
                }
            }
            else if (actualCircuitSelection == 2)
            {
                if (trackTexture.name == "Track1Image" || trackTexture.name == "Track1Text")
                { 
                    Track2Visible.transform.GetComponent<RawImage>().texture = circuit1;
                    currentRoomCreated.circuits[1] = Circuit.Track1;
                }
                else if (trackTexture.name == "Track2Image" || trackTexture.name == "Track2Text")
                {
                    Track2Visible.transform.GetComponent<RawImage>().texture = circuit2;
                    currentRoomCreated.circuits[1] = Circuit.Track2;
                }
                else if (trackTexture.name == "Track3Image" || trackTexture.name == "Track3Text")
                {
                    Track2Visible.transform.GetComponent<RawImage>().texture = circuit3;
                    currentRoomCreated.circuits[1] = Circuit.Track3;
                }
            }
            else if (actualCircuitSelection == 3)
            {
                if (trackTexture.name == "Track1Image" || trackTexture.name == "Track1Text")
                {
                    Track3Visible.transform.GetComponent<RawImage>().texture = circuit1;
                    currentRoomCreated.circuits[2] = Circuit.Track1;
                }
                else if (trackTexture.name == "Track2Image" || trackTexture.name == "Track2Text")
                {
                    Track3Visible.transform.GetComponent<RawImage>().texture = circuit2;
                    currentRoomCreated.circuits[2] = Circuit.Track2;
                }
                else if (trackTexture.name == "Track3Image" || trackTexture.name == "Track3Text")
                {
                    Track3Visible.transform.GetComponent<RawImage>().texture = circuit3;
                    currentRoomCreated.circuits[2] = Circuit.Track3;
                }
            }
        }
        CircuitChoice.SetActive(false);
    }

    public void OnPointer_Click()
    {
        switch (this.name)
        {
            case "RoomCreationSubmitText":
                currentRoomCreated.roomIndex = GameObject.Find("Rooms_Script").GetComponent<room_info_container>().rooms.Count;

                if(toggleIsOn=="Single")
                    currentRoomCreated.room = RoomType.SingleRace;
                else
                    currentRoomCreated.room = RoomType.Tournament;

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
                roomCreationForm.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
