using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setCarContainer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Network").GetComponent<Network>().carsContainer = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
