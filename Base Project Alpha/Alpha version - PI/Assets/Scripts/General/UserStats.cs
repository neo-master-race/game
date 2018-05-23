using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserStats : MonoBehaviour {

    [Header("User Informations")]
    public string username;
    public bool isOnRoomList = false;
    public bool isOnLobby = false;
    public string inLobby;
    public int onTrackNb = 0;
    public bool playingSolo = false;
    public bool playingMulti = false;
    public int trackLapNumber = 0;

    [Header("User Global Stats")]
    public int raceNb;
    public int raceVictory;

    [Header("User Records")]
    public string track1LapRecord;
    public string track2LapRecord;
    public string track3LapRecord;

    [Header("Session Infos")]
    public int carIndex = 1;
    public int currentCarR;
    public int currentCarG;
    public int currentCarB;
    public bool isBack = false;
    public bool isguest = false;
    public int startingPosition;

    [Header("User Cars")]
    public int Car1R;
    public int Car1G;
    public int Car1B;
    public int Car2R;
    public int Car2G;
    public int Car2B;
    public int Car3R;
    public int Car3G;
    public int Car3B;
    public int Car4R;
    public int Car4G;
    public int Car4B;

    public float stratosValueSlider;
    public float porscheValueSlider;
    public float lamborghiniValueSlider;
    public float fordValueSlider;

    public int stratosTopRR;
    public int stratosTopRG;
    public int stratosTopRB;
    public int porscheTopRR;
    public int porscheTopRG;
    public int porscheTopRB;
    public int lamborghiniTopRR;
    public int lamborghiniTopRG;
    public int lamborghiniTopRB;
    public int fordTopRR;
    public int fordTopRG;
    public int fordTopRB;

    public float stratosCursorX;
    public float stratosCursorY;
    public float porscheCursorX;
    public float porscheCursorY;
    public float lamborghiniCursorX;
    public float lamborghiniCursorY;
    public float fordCursorX;
    public float fordCursorY;



    private static UserStats playerInstance;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

    public void gobacktomenu ()
    {
		isBack = true;
		SceneManager.LoadScene("entryScene", LoadSceneMode.Single);
        isOnRoomList = false;
        isOnLobby = false;
        inLobby=null;
        onTrackNb = 0;
        playingSolo = false;
		playingMulti = false;
        trackLapNumber = 0;
		isBack = false;
    }

    public void isGuest()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().username = GameObject.Find("Network").GetComponent<Network>().getClientName();
        isguest = true;
        track1LapRecord = "--:--:--";
        track2LapRecord = "--:--:--";
        track3LapRecord = "--:--:--";
    }

    public void setUserStats(string usernam,int numberOfRaces,int numberOfWins,string recordTrack1, string recordTrack2, string recordTrack3,
         int car1red, int car1green, int car1blue,
         int car2red, int car2green, int car2blue,
         int car3red, int car3green, int car3blue,
         int car4red, int car4green, int car4blue,
         float car1slider, int car1redTR, int car1greenTR, int car1blueTR, float car1cursorX, float car1cursorY,
         float car2slider, int car2redTR, int car2greenTR, int car2blueTR, float car2cursorX, float car2cursorY,
         float car3slider, int car3redTR, int car3greenTR, int car3blueTR, float car3cursorX, float car3cursorY,
         float car4slider, int car4redTR, int car4greenTR, int car4blueTR, float car4cursorX, float car4cursorY)
    {
        username = usernam;
        raceNb = numberOfRaces;
        raceVictory = numberOfWins;
        track1LapRecord = recordTrack1;
        track2LapRecord = recordTrack2;
        track3LapRecord = recordTrack3;

        Car1R = car1red;
        Car1G = car1green;
        Car1B = car1blue;
        Car2R = car2red;
        Car2G = car2green;
        Car2B = car2blue;
        Car3R = car3red;
        Car3G = car3green;
        Car3B = car3blue;
        Car4R = car4red;
        Car4G = car4green;
        Car4B = car4blue;

        stratosValueSlider = car1slider;
        porscheValueSlider = car2slider;
        lamborghiniValueSlider = car3slider;
        fordValueSlider = car4slider;


        stratosTopRR = car1redTR;
        porscheTopRR = car2redTR;
        lamborghiniTopRR = car3redTR;
        fordTopRR = car4redTR;
        stratosTopRG = car1greenTR;
        porscheTopRG = car2greenTR;
        lamborghiniTopRG = car3greenTR;
        fordTopRG = car4greenTR;
        stratosTopRB = car1blueTR;
        porscheTopRB = car2blueTR;
        lamborghiniTopRB = car3blueTR;
        fordTopRB = car4blueTR;

        stratosCursorX = car1cursorX;
        stratosCursorY = car1cursorY;
        porscheCursorX = car2cursorX;
        porscheCursorY = car2cursorY;
        lamborghiniCursorX = car3cursorX;
        lamborghiniCursorY = car3cursorY;
        fordCursorX = car4cursorX;
        fordCursorY = car4cursorY;
}

    public void sendStats()
    {
        GameObject.Find("Network").GetComponent<Network>().sendUserStatsToDB(
            raceNb,
            raceVictory,
            track1LapRecord,
            track2LapRecord,
            track3LapRecord,
            Car1R,
            Car1G,
            Car1B,
            Car2R,
            Car2G,
            Car2B,
            Car3R,
            Car3G,
            Car3B,
            Car4R,
            Car4G,
            Car4B,
            stratosValueSlider,
            stratosTopRR,
            stratosTopRG,
            stratosTopRB,
            stratosCursorX,
            stratosCursorY,
            porscheValueSlider,
            porscheTopRR,
            porscheTopRG,
            porscheTopRB,
            porscheCursorX,
            porscheCursorY,
            lamborghiniValueSlider,
            lamborghiniTopRR,
            lamborghiniTopRG,
            lamborghiniTopRB,
            lamborghiniCursorX,
            lamborghiniCursorY,
            fordValueSlider,
            fordTopRR,
            fordTopRG,
            fordTopRB,
            fordCursorX,
            fordCursorY
            );
    }

    // Update is called once per frame
    void Update () {
            
    }
}
