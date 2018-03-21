using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Luc-Michel Zissler
// TODO : add ordinal indicator

public class PositionHandler : MonoBehaviour {

    public Text positionObject;
    
    public int playerPosition;
    public int totalPlayers;

	// Use this for initialization
	void Start () {
        initialPositionInfos(6, 12);
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

        displayPositionInfos();
    }

    public void displayPositionInfos()
    {
        positionObject.text = playerPosition.ToString();
    }

    public void incrementPlayerPosition()
    {
        if (playerPosition < totalPlayers)
        {
            playerPosition++;
            displayPositionInfos();
        }
    }

    public void decrementPlayerPosition()
    {
        if( playerPosition > 1)
        {
            playerPosition--;
            displayPositionInfos();
        }
    }
}
