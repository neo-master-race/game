using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserHoverTag : MonoBehaviour {

    public Text username;
    public GameObject localPlayer;


	// Use this for initialization
	void Start () {
        username.text =
            GameObject.Find("UserStats").GetComponent<UserStats>().username;

        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                player.transform.Find("userName/userNameTexture/userNamePosition").GetComponent<Text>().enabled = false;
                player.transform.Find("userName/userNameTexture/userNameText").GetComponent<Text>().enabled = false;
                player.transform.Find("userName/userNameTexture").GetComponent<RawImage>().enabled = false;
                localPlayer = player;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.localEulerAngles = new Vector3(0, 180+(localPlayer.transform.localEulerAngles.y-this.transform.parent.localEulerAngles.y), 0);
        /*for (int i = 0; i < GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().playersLeaderboard.Length; i++)
        {
            if (!GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().playersLeaderboard[i].GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().playersLeaderboard[i].gameObject.transform.Find("userName/userNameTexture/userNamePosition").GetComponent<Text>().text = (i + 1).ToString();
                GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().playersLeaderboard[i].gameObject.transform.Find("userName/userNameTexture/userNameText").GetComponent<Text>().text = GameObject.Find("Stratos").GetComponent<Player_Info_Ingame>().playersLeaderboard[i].GetComponent<Player_Info_Ingame>().userName;
            }
        }*/
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player_Info_Ingame>().isLocalPlayer)
            {
                player.transform.Find("userName/userNameTexture/userNamePosition").GetComponent<Text>().enabled = false;
                player.transform.Find("userName/userNameTexture/userNameText").GetComponent<Text>().enabled = false;
                player.transform.Find("userName/userNameTexture").GetComponent<RawImage>().enabled = false;
            }
        }
    }
}
