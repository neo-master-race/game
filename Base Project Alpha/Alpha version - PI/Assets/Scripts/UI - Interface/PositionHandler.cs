using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Luc-Michel Zissler

public class PositionHandler : MonoBehaviour {

    public Text positionObject;
    private string ordinalIndicator; // example : st, nd, etc..
    
    public int playerPosition;
    public int totalPlayers;

	// Use this for initialization
	void Start () {
        initialPositionInfos(4, 4);

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
        positionObject.text = playerPosition.ToString() + ordinalIndicator;
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
                ordinalIndicator = "st";
                break;
            case 2:
                ordinalIndicator = "nd";
                break;
            case 3:
                ordinalIndicator = "rd";
                break;
            default:
                ordinalIndicator = "th";
                break;
        }
    }
}
