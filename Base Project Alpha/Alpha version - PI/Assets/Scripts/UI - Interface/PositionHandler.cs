using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Luc-Michel Zissler

public class PositionHandler : MonoBehaviour {

    public Text positionObject;
    public Text positionObjectend;
    private string ordinalIndicator; // example : st, nd, etc..
    
    public int playerPosition;
    public int totalPlayers;

    public GameObject positionparent;

	// Use this for initialization
	void Start () {
        initialPositionInfos(4, 4);
        setpos(4);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initialPositionInfos(int pos, int total)
    {
        setTotalPlayers(total);
        setPlayerPosition(pos);
    }

    public void setTotalPlayers(int total)
    {
        if(total > 0)
        {
            totalPlayers = total; 
        }

        displayPositionInfos();
    }

    public void setPlayerPosition(int pos)
    {
        if(pos > totalPlayers)
        {
            playerPosition = totalPlayers;
        }
        else if (pos < 1)
        {
            playerPosition = 1;
        }
        else
        {
            playerPosition = pos;
        }

        getOrdinalIndicator(playerPosition);
        displayPositionInfos();
    }

    public void displayPositionInfos()
    {
        positionObject.text = playerPosition.ToString();
        positionObjectend.text = ordinalIndicator;
    }

    public void incrementPlayerPosition()
    {
        if (playerPosition < totalPlayers)
        {
            playerPosition++;
            getOrdinalIndicator(playerPosition);
            displayPositionInfos();
        }
    }

    public void decrementPlayerPosition()
    {
        if( playerPosition > 1)
        {
            playerPosition--;
            getOrdinalIndicator(playerPosition);
            displayPositionInfos();
        }
    }

    public void getOrdinalIndicator(int pos)
    {
        switch(pos)
        {
            case 1:
                ordinalIndicator = "er";
                break;
            default:
                ordinalIndicator = "ème";
                break;
        }
    }

    public void setpos(int pos)
    {
        switch (pos)
        {
            case 1:
                GameObject.Find("PositionText").GetComponent<Text>().text = "1";
                GameObject.Find("PositionText2").GetComponent<Text>().text = "er";
                positionparent.GetComponent<Image>().color = new Color32(235,205,0, 200);
                GameObject.Find("PositionText").GetComponent<Text>().color = new Color32(255, 225, 0, 255);
                GameObject.Find("PositionText2").GetComponent<Text>().color = new Color32(255, 225, 0, 255);
                break;
            case 2:
                GameObject.Find("PositionText").GetComponent<Text>().text = "2";
                GameObject.Find("PositionText2").GetComponent<Text>().text = "ème";
                positionparent.GetComponent<Image>().color = new Color32(120,120,120, 200);
                GameObject.Find("PositionText").GetComponent<Text>().color = new Color32(154, 154, 154, 255);
                GameObject.Find("PositionText2").GetComponent<Text>().color = new Color32(154, 154, 154, 255);
                break;
            case 3:
                GameObject.Find("PositionText").GetComponent<Text>().text = "3";
                GameObject.Find("PositionText2").GetComponent<Text>().text = "ème";
                positionparent.GetComponent<Image>().color = new Color32(170, 80, 50, 200);
                GameObject.Find("PositionText").GetComponent<Text>().color = new Color32(207, 120, 50, 255);
                GameObject.Find("PositionText2").GetComponent<Text>().color = new Color32(207, 120, 50, 255);
                break;
            case 4:
                GameObject.Find("PositionText").GetComponent<Text>().text = "4";
                GameObject.Find("PositionText2").GetComponent<Text>().text = "ème";
                positionparent.GetComponent<Image>().color = new Color32(60, 16, 104, 200);
                GameObject.Find("PositionText").GetComponent<Text>().color = new Color32(120, 60, 150, 255);
                GameObject.Find("PositionText2").GetComponent<Text>().color = new Color32(120, 60, 150, 255);
                break;
        }
    }
}
