using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_info_update : MonoBehaviour {

	public Text info;

	// Use this for initialization
	void Start () {
    }

    public void OnPointerClick()
    {
		info.text = "5";
		//GameObject.Find("UserStats").GetComponent<UserStats>().gobacktomenu();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
