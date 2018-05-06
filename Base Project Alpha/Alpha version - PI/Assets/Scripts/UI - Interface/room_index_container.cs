﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class room_index_container : MonoBehaviour, IPointerClickHandler {

    public string roomIndex;

    

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject.Find("Network").GetComponent<Network>().joinGameRequest(roomIndex);
        GameObject.Find("Rooms_Script").GetComponent<room_info_container>().goToLobby();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
	}
}
