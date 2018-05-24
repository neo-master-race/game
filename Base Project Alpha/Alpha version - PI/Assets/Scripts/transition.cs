using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class transition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().gobacktomenu();
        //GameObject.Find("Network").GetComponent<Network>().leaveRoom();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
