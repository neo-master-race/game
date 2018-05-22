using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startLightsSequence : MonoBehaviour {

    public float timerInBetweenLights = 1.0f;

    public Texture blackLight;
    public Texture redLight;
    public Texture greenLight;

    private int lightSequenceStep = 0;
    public GameObject localPlayer;

	// Use this for initialization
	void Start () {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                player.GetComponent<CarController>().canDrive = false;
                localPlayer = player;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        timerInBetweenLights -= Time.deltaTime;
        if(timerInBetweenLights<0 && lightSequenceStep<=7)
        {
            lightSequenceStep++;

            if (lightSequenceStep <= 5)
                GameObject.Find("Light" + lightSequenceStep).GetComponent<RawImage>().texture = redLight;

            timerInBetweenLights = 1.0f;

            if(lightSequenceStep == 5)
                timerInBetweenLights = 1.0f+Random.Range(1f,3f);

            if (lightSequenceStep==6)
            {
                for(int i=1;i<=5;i++)
                {
                    GameObject.Find("Light" + i).GetComponent<RawImage>().texture = greenLight;
                }
                localPlayer.GetComponent<CarController>().enabled = true;
                timerInBetweenLights = 2.0f;
                GameObject.Find("TimeCounter").GetComponent<Timer>().timerOn[0] = true;
                GameObject.Find("TimeCounter").GetComponent<Timer>().timerOn[1] = true;
                GameObject.Find("TimeCounter").GetComponent<Timer>().timerOn[2] = true;
                GameObject.Find("TimeCounter").GetComponent<Timer>().timerOn[3] = true;
                localPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                localPlayer.GetComponent<CarController>().canDrive = true;
            }
            if (lightSequenceStep == 7)
            {
                for (int i = 1; i <= 5; i++)
                {
                    GameObject.Find("Light" + i).SetActive(false);
                }
            }
        }
	}
}
