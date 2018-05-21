// Muller Ludovic
// 07-03-2018

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using UnityEngine.SceneManagement;

public
class Network : MonoBehaviour {
 private
  bool socketReady;
 private
  TcpClient socket;
 private
  NetworkStream stream;
 private
  Hashtable players;
 private
  string clientName;
 public
  GameObject carsContainer;
 public
  GameObject carPrefab;
 public
  GameObject car2Prefab;
 public
  GameObject car3Prefab;
 public
  GameObject car4Prefab;
 public
  Material otherPlayerMat;

  [Header("User Log Informations")] public String username;
 public
  String password;

  // Use this for initialization
 private
  void Start() {
    DontDestroyOnLoad(this.gameObject);
    carsContainer = GameObject.Find("Cars");

    players = new Hashtable();
    clientName = "Invité-" + new System.Random().Next(1, 65536);

    string host = "pi-2.ludovic-muller.fr";
    Int32 port = 4242;

    try {
      socket = new TcpClient(host, port);
      stream = socket.GetStream();
      socketReady = true;

      sendMessage(new Protocol.Message{
          Type = "change_username",
          ChangeUsername = new Protocol.ChangeUsername{Username = clientName}});

      UpdatePlayerStatus();
    } catch (Exception e) {
      socketReady = false;
      Debug.Log("socket error: " + e.Message);
    }
  }

  // Update is called once per frame
 private
  void Update() {
    if (socketReady) {
      while (stream.DataAvailable) {
        onIncomingData();
      }
    }

    if (SceneManager.GetActiveScene().name == "entryScene" &&
        ((Input.GetKeyDown(KeyCode.Return) ||
          GameObject.Find("Script_Source")
                  .GetComponent<menu_selection>()
                  .confirm_start == 1) &&
         (GameObject.Find("Script_Source")
              .GetComponent<menu_selection>()
              .start_action == "Login")) &&
        (username != "" && password != "")) {
      login();
    }

    if (SceneManager.GetActiveScene().name == "entryScene" &&
        ((Input.GetKeyDown(KeyCode.Return) ||
          GameObject.Find("Script_Source")
                  .GetComponent<menu_selection>()
                  .confirm_start == 1) &&
         (GameObject.Find("Script_Source")
              .GetComponent<menu_selection>()
              .start_action == "Register")) &&
        (username != "" && password != "")) {
      register();
    }
  }

 public
  void GetInputUser(GameObject userfield) {
    this.username = userfield.GetComponent<InputField>().text;
  }

 public
  void GetInputPass(GameObject pwdfield) {
    this.password = pwdfield.GetComponent<InputField>().text;
  }

 public
  string getClientName() { return clientName; }

 public
  void login() {
    Debug.Log("call to login function");
    if (username == "" || password == "") {
      GameObject.Find("LogRegForm")
          .GetComponent<log_reg_form>()
          .RegisterError("Veuillez saisir vos identifiants.");
      return;
    }

    Protocol.LoginRequest lr =
        new Protocol.LoginRequest{Username = username, Password = password};

    Protocol.Message msg =
        new Protocol.Message{Type = "login_request", LoginRequest = lr};

    sendMessage(msg);
  }

 public
  void register() {
    Debug.Log("call to register function");
    if (username == "" || password == "") {
      GameObject.Find("LogRegForm")
          .GetComponent<log_reg_form>()
          .LogInError("Veuillez saisir vos identifiants.");
      return;
    }

    Protocol.RegisterRequest rr =
        new Protocol.RegisterRequest{Username = username, Password = password};

    Protocol.Message msg =
        new Protocol.Message{Type = "register_request", RegisterRequest = rr};

    sendMessage(msg);
  }

 public
  void logout() {
    // just generate a new random clientName
    clientName = "Invité-" + new System.Random().Next(1, 65536);
    sendMessage(new Protocol.Message{
        Type = "change_username",
        ChangeUsername = new Protocol.ChangeUsername{Username = clientName}});
  }

  // send a message (UpdatePlayerPosition, ChatMessage, ...) to the socket
 public
  void sendMessage(Protocol.Message msg) {
    byte[] msgBytes = msg.ToByteArray();
    byte[] msgLength = BitConverter.GetBytes(msgBytes.Length);
    if (!BitConverter.IsLittleEndian) {
      Array.Reverse(msgLength);
    }

    byte[] finalMessage = new byte[4 + msgBytes.Length];
    Array.Copy(msgLength, finalMessage, 4);
    Array.Copy(msgBytes, 0, finalMessage, 4, msgBytes.Length);

    stream.Write(finalMessage, 0, finalMessage.Length);
  }

  // when user clicks the send button to send a chat message
 public
  void onChatSendAction() {
    GameObject go = GameObject.Find("InputField");
    string message = go.GetComponent<InputField>().text;
    sendChatMessage(message);
    go.GetComponent<InputField>().text = "";
  }

  // send a chat message
 private
  void sendChatMessage(string data) {
    Protocol.ChatMessage chatMsg =
        new Protocol.ChatMessage{User = clientName, Content = data};

    Protocol.Message msg =
        new Protocol.Message{Type = "chat_message", ChatMessage = chatMsg};

    sendMessage(msg);
  }

  // when there are some incoming datas, do something with them
 private
  void onIncomingData() {
    Byte[] data = new Byte[4];

    stream.Read(data, 0, 4);  // read an int

    if (!BitConverter.IsLittleEndian) {
      Array.Reverse(data);
    }

    int dataLength = BitConverter.ToInt32(data, 0);

    data = new Byte[dataLength];
    stream.Read(data, 0, data.Length);

    try {
      Protocol.Message parsedData;
      parsedData = Protocol.Message.Parser.ParseFrom(data);
      filterIncomingMessages(parsedData);
    } catch {
      // do nothing
    }
  }

  // get the player (or create if needed)
 private
  GameObject getPlayer(string clientName, int cType, int cR, int cG, int cB) {
    GameObject player;
    if (!players.ContainsKey(clientName)) {
      Material currPlayerMat = new Material(otherPlayerMat);
      currPlayerMat.color = new Color32((byte)cR, (byte)cG, (byte)cB, 255);
      switch (cType) {
        case 1:
          player =
              Instantiate(carPrefab, carsContainer.transform) as GameObject;
          player.transform.Find("stratos")
              .transform.GetChild(0)
              .GetComponent<MeshRenderer>()
              .material = currPlayerMat;
          break;
        case 2:
          player =
              Instantiate(car2Prefab, carsContainer.transform) as GameObject;
          player.transform.Find("porsche")
              .transform.GetChild(0)
              .GetComponent<MeshRenderer>()
              .material = currPlayerMat;
          break;
        case 3:
          player =
              Instantiate(car3Prefab, carsContainer.transform) as GameObject;
          player.transform.Find("lamborghini")
              .transform.GetChild(0)
              .GetComponent<MeshRenderer>()
              .material = currPlayerMat;
          break;
        case 4:
          player =
              Instantiate(car4Prefab, carsContainer.transform) as GameObject;
          player.transform.Find("ford")
              .transform.GetChild(0)
              .GetComponent<MeshRenderer>()
              .material = currPlayerMat;
          break;
        default:
          player =
              Instantiate(carPrefab, carsContainer.transform) as GameObject;
          break;
      }

      player.GetComponent<Player_Info_Ingame>().userName = clientName;
      player.GetComponent<Player_Info_Ingame>().isLocalPlayer = false;
      player.GetComponent<CarController>().isLocalPlayer = false;

      players.Add(clientName, player);
    } else {
      player = players[clientName] as GameObject;
    }
    return player;
  }

  // filter incoming message using his type
 private
  void filterIncomingMessages(Protocol.Message parsedData) {
    GameObject player;
    string user;

    switch (parsedData.Type) {
      case "update_player_position":
        Protocol.UpdatePlayerPosition upp = parsedData.UpdatePlayerPosition;
        Protocol.Vector vecPos = upp.Position;
        Protocol.Vector vecRot = upp.Direction;
        Protocol.Vector vecScale = upp.Scale;
        Protocol.Vector vecVelocity = upp.Velocity;
        user = upp.User;

        if (user == clientName)
          break;

        player = getPlayer(user, upp.CarType, upp.CarR, upp.CarG, upp.CarB);

        player.transform.localPosition =
            new Vector3(vecPos.X, vecPos.Y, vecPos.Z);
        player.transform.localEulerAngles =
            new Vector3(vecRot.X, vecRot.Y, vecRot.Z);
        player.transform.localScale =
            new Vector3(vecScale.X, vecScale.Y, vecScale.Z);
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(vecVelocity.X, vecVelocity.Y, vecVelocity.Z);
        break;
      case "update_player_status":
        Protocol.UpdatePlayerStatus ups = parsedData.UpdatePlayerStatus;

        IEnumerator<bool> num = ups.WentThrough.GetEnumerator();
        List<bool> list = new List<bool>();
        while (num.MoveNext()) {
          list.Add(num.Current);
        }
        bool[] wentThrough = list.ToArray();

        int lapCount = ups.LapCount;
        bool hasHitSFLineOnce = ups.HasHitSFLineOnce;
        int cpCount = ups.CpCount;
        int nextCheckpointNumber = ups.NextCheckpointNumber;
        int supposedNextCheckpointNumber = ups.SupposedNextCheckpointNumber;
        int virtual_lap_count = ups.VirtualLapCount;
        int lastHittedCP = ups.LastHittedCp;
        int secondLastHittedCP = ups.SecondLastHittedCp;
        user = ups.User;

        if (user == clientName)
          break;

        player = getPlayer(user, 0, 0, 0, 0);

        Debug.Log("Got status response from " + user);
        player.GetComponent<Player_Info_Ingame>().lap_count = lapCount;
        player.GetComponent<Player_Info_Ingame>().hasHitSFLineOnce =
            hasHitSFLineOnce;
        player.GetComponent<Player_Info_Ingame>().cp_count = cpCount;
        player.GetComponent<Player_Info_Ingame>().nextCheckpointNumber =
            nextCheckpointNumber;
        player.GetComponent<Player_Info_Ingame>().supposedNextCheckpointNumber =
            supposedNextCheckpointNumber;
        player.GetComponent<Player_Info_Ingame>().wentThrough = wentThrough;
        player.GetComponent<Player_Info_Ingame>().virtual_lap_count =
            virtual_lap_count;
        player.GetComponent<Player_Info_Ingame>().lastHittedCP = lastHittedCP;
        player.GetComponent<Player_Info_Ingame>().secondLastHittedCP =
            secondLastHittedCP;

        break;
      case "update_player_status_request":
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("Got request");

        foreach (GameObject car in cars) {
          if (car.GetComponent<CarController>().isLocalPlayer) {
            car.GetComponent<CarController>().updatePlayerStatus();
          }
        }

        break;
      case "chat_message":
        Protocol.ChatMessage chatMsg = parsedData.ChatMessage;
        Debug.Log("Received from " + chatMsg.User +
                  " the following message: " + chatMsg.Content);
        break;
      case "disconnect":
        user = parsedData.Disconnect.User;
        Destroy(getPlayer(user, 0, 0, 0, 0));
        players.Remove(user);
        break;
      case "starting_position":
        Protocol.StartingPosition sp = parsedData.StartingPosition;
        IEnumerator<int> numInt = sp.Position.GetEnumerator();
        List<int> listInt = new List<int>();
        while (numInt.MoveNext()) {
          listInt.Add(numInt.Current);
        }
        int[] positions = listInt.ToArray();

        // @TODO: do something with positions

        break;
      case "register_response":
        Protocol.RegisterResponse registerResponse =
            parsedData.RegisterResponse;
        bool registredSuccess = registerResponse.Success;
        string registredUsername = registerResponse.Username;

        if (registredSuccess) {
          updateUserStats(registerResponse.UserStats);
          GameObject.Find("LogRegForm")
              .GetComponent<log_reg_form>()
              .RegisterSuccess();
          clientName = registredUsername;
          Debug.Log("Sucessfully registred as " + clientName);
        } else {
          Debug.Log("Error while trying to register as " + registredUsername);
          GameObject.Find("LogRegForm")
              .GetComponent<log_reg_form>()
              .RegisterError("Nom d'utilisateur déjà existant.");
        }

        break;
      case "login_response":
        Protocol.LoginResponse loginResponse = parsedData.LoginResponse;
        bool loggedSuccess = loginResponse.Success;
        string loggedUsername = loginResponse.Username;

        if (loggedSuccess) {
          updateUserStats(loginResponse.UserStats);
          GameObject.Find("LogRegForm")
              .GetComponent<log_reg_form>()
              .LogInSuccess();
          clientName = loggedUsername;
          Debug.Log("Sucessfully logged in as " + clientName);
        } else {
          Debug.Log("Error while trying to log in as " + loggedUsername);
          GameObject.Find("LogRegForm")
              .GetComponent<log_reg_form>()
              .LogInError("Nom d'utilisateur ou mot de passe incorrect.");
        }

        break;
      case "room_list_response":
        Protocol.RoomListResponse rlr = parsedData.RoomListResponse;

        foreach (Protocol.RoomListItem rli in rlr.RoomList) {
          List<string> playersUsernameList = new List<string>();
          List<int> playersNbRacesList = new List<int>();
          List<int> playersNbWinsList = new List<int>();
          List<string> playersRecordList = new List<string>();

          IEnumerator<Protocol.Player> numPlayers = rli.Players.GetEnumerator();
          while (numPlayers.MoveNext()) {
            playersUsernameList.Add(numPlayers.Current.Username);
            playersNbRacesList.Add(numPlayers.Current.NbRaces);
            playersNbWinsList.Add(numPlayers.Current.NbWins);
            playersRecordList.Add(numPlayers.Current.Record);
          }

          string[] playersUsername = playersUsernameList.ToArray();
          int[] playersNbRaces = playersNbRacesList.ToArray();
          int[] playersNbWins = playersNbWinsList.ToArray();
          string[] playersRecord = playersRecordList.ToArray();

          List<int> rliStartingPositionsList = new List<int>();
          IEnumerator<int> numSP = rli.StartingPositions.GetEnumerator();
          while (numSP.MoveNext()) {
            rliStartingPositionsList.Add(numSP.Current);
          }
          int[] rliStartingPositions = rliStartingPositionsList.ToArray();

          GameObject.Find("Rooms_Script")
              .GetComponent<room_info_container>()
              .addOrUpdateRoom(rli.Id, rli.RoomType, rli.IdCircuit,
                               rli.MaxPlayers, rli.NbPlayers, playersUsername,
                               playersNbRaces, playersNbWins, playersRecord,
                               rliStartingPositions);
        }

        Debug.Log("Got response and created all rooms");
        break;
      case "join_room_response":
        Protocol.JoinRoomResponse jrr = parsedData.JoinRoomResponse;
        Protocol.RoomListItem rlitem = jrr.Room;

        if (jrr.Success) {
          List<string> playersUsernameList = new List<string>();
          List<int> playersNbRacesList = new List<int>();
          List<int> playersNbWinsList = new List<int>();
          List<string> playersRecordList = new List<string>();

          IEnumerator<Protocol.Player> numPlayers =
              rlitem.Players.GetEnumerator();
          while (numPlayers.MoveNext()) {
            playersUsernameList.Add(numPlayers.Current.Username);
            playersNbRacesList.Add(numPlayers.Current.NbRaces);
            playersNbWinsList.Add(numPlayers.Current.NbWins);
            playersRecordList.Add(numPlayers.Current.Record);
          }

          string[] playersUsername = playersUsernameList.ToArray();
          int[] playersNbRaces = playersNbRacesList.ToArray();
          int[] playersNbWins = playersNbWinsList.ToArray();
          string[] playersRecord = playersRecordList.ToArray();

          List<int> rlitemStartingPositionsList = new List<int>();
          IEnumerator<int> nSP = rlitem.StartingPositions.GetEnumerator();
          while (nSP.MoveNext()) {
            rlitemStartingPositionsList.Add(nSP.Current);
          }
          int[] rlitemStartingPositions = rlitemStartingPositionsList.ToArray();

          GameObject.Find("Rooms_Script")
              .GetComponent<room_info_container>()
              .goToLobby(rlitem.Id, rlitem.RoomType, rlitem.IdCircuit,
                         rlitem.MaxPlayers, rlitem.NbPlayers, playersUsername,
                         playersNbRaces, playersNbWins, playersRecord);
        }
        break;
      case "start_room":
        Protocol.StartRoom sr = parsedData.StartRoom;

        if (sr.Success) {
          IEnumerator coroutine;
          coroutine = GameObject.Find("Rooms_Script")
                          .GetComponent<room_info_container>()
                          .roomStartCountdown(10.0f);
          StartCoroutine(coroutine);
        }

        break;
      default:
        Debug.LogWarning("unsupported message type for " + parsedData);
        break;
    }
  }

 private
  void updateUserStats(Protocol.UserStats us) {
    GameObject.Find("UserStats")
        .GetComponent<UserStats>()
        .setUserStats(
            us.Username, us.Race, us.Victory, us.Recordt1, us.Recordt2,
            us.Recordt3, us.Car1Red, us.Car1Green, us.Car1Blue, us.Car2Red,
            us.Car2Green, us.Car2Blue, us.Car3Red, us.Car3Green, us.Car3Blue,
            us.Car4Red, us.Car4Green, us.Car4Blue, us.Car1Slider, us.Car1RedTR,
            us.Car1GreenTR, us.Car1BlueTR, us.Car1CursorX, us.Car1CursorY,
            us.Car2Slider, us.Car2RedTR, us.Car2GreenTR, us.Car2BlueTR,
            us.Car2CursorX, us.Car2CursorY, us.Car3Slider, us.Car3RedTR,
            us.Car3GreenTR, us.Car3BlueTR, us.Car3CursorX, us.Car3CursorY,
            us.Car4Slider, us.Car4RedTR, us.Car4GreenTR, us.Car4BlueTR,
            us.Car4CursorX, us.Car4CursorY);
  }

  // tell the network to send player's current position to all others
 public
  void updatePlayerPosition(Protocol.Vector vecPosition,
                            Protocol.Vector vecRotation,
                            Protocol.Vector vecScale,
                            Protocol.Vector vecVelocity) {
    int carType =
        GameObject.Find("UserStats").GetComponent<UserStats>().carIndex;
    int carR =
        GameObject.Find("UserStats").GetComponent<UserStats>().currentCarR;
    int carG =
        GameObject.Find("UserStats").GetComponent<UserStats>().currentCarG;
    int carB =
        GameObject.Find("UserStats").GetComponent<UserStats>().currentCarB;

    // all together
    Protocol.UpdatePlayerPosition upp =
        new Protocol.UpdatePlayerPosition{Position = vecPosition,
                                          Direction = vecRotation,
                                          Scale = vecScale,
                                          User = clientName,
                                          Velocity = vecVelocity,
                                          CarType = carType,
                                          CarR = carR,
                                          CarG = carG,
                                          CarB = carB};

    // final message that we can send
    Protocol.Message msg = new Protocol.Message{Type = "update_player_position",
                                                UpdatePlayerPosition = upp};

    sendMessage(msg);
  }

  // tell the network to send player's current status to all others
 public
  void UpdatePlayerStatus(bool[] wentThrough,
                          int lapCount,
                          bool hasHitSFLineOnce,
                          int cpCount,
                          int nextCheckpointNumber,
                          int supposedNextCheckpointNumber,
                          int virtual_lap_count,
                          int lastHittedCP,
                          int secondLastHittedCP) {
    // all together
    Protocol.UpdatePlayerStatus ups = new Protocol.UpdatePlayerStatus{
        LapCount = lapCount,
        HasHitSFLineOnce = hasHitSFLineOnce,
        CpCount = cpCount,
        NextCheckpointNumber = nextCheckpointNumber,
        SupposedNextCheckpointNumber = supposedNextCheckpointNumber,
        User = clientName,
        VirtualLapCount = virtual_lap_count,
        LastHittedCp = lastHittedCP,
        SecondLastHittedCp = secondLastHittedCP};

    ups.WentThrough.Add(wentThrough);

    // final message that we can send
    Protocol.Message msg = new Protocol.Message{Type = "update_player_status",
                                                UpdatePlayerStatus = ups};

    sendMessage(msg);
  }

  // tell the network to send an int array containing all starting positions
 public
  void StartingPosition(int[] positions) {
    Protocol.StartingPosition sp = new Protocol.StartingPosition{};
    sp.Position.Add(positions);

    // final message that we can send
    Protocol.Message msg =
        new Protocol.Message{Type = "starting_position", StartingPosition = sp};

    sendMessage(msg);
  }

 public
  void UpdatePlayerStatus() {
    sendMessage(new Protocol.Message{
        Type = "update_player_status_request",
        UpdatePlayerStatusRequest = new Protocol.UpdatePlayerStatusRequest()});
  }

  // request for creating a room
 public
  void createRoom(int type, int circuit, int maxPlayers) {
    sendMessage(new Protocol.Message{
        Type = "create_room",
        CreateRoom = new Protocol.CreateRoom{
            RoomType = type, IdCircuit = circuit, MaxPlayers = maxPlayers}});
  }

 public
  void roomListRequest() {
    sendMessage(
        new Protocol.Message{Type = "room_list_request",
                             RoomListRequest = new Protocol.RoomListRequest()});
  }

 public
  void joinGameRequest(string gameId) {
    sendMessage(new Protocol.Message{
        Type = "join_room_request",
        JoinRoomRequest = new Protocol.JoinRoomRequest{Id = gameId}});
  }

 public
  void sendUserStatsToDB(int race,
                         int victory,
                         string recordt1,
                         string recordt2,
                         string recordt3,
                         int car1red,
                         int car1green,
                         int car1blue,
                         int car2red,
                         int car2green,
                         int car2blue,
                         int car3red,
                         int car3green,
                         int car3blue,
                         int car4red,
                         int car4green,
                         int car4blue,
                         float car1slider,
                         int car1redTR,
                         int car1greenTR,
                         int car1blueTR,
                         float car1cursorX,
                         float car1cursorY,
                         float car2slider,
                         int car2redTR,
                         int car2greenTR,
                         int car2blueTR,
                         float car2cursorX,
                         float car2cursorY,
                         float car3slider,
                         int car3redTR,
                         int car3greenTR,
                         int car3blueTR,
                         float car3cursorX,
                         float car3cursorY,
                         float car4slider,
                         int car4redTR,
                         int car4greenTR,
                         int car4blueTR,
                         float car4cursorX,
                         float car4cursorY) {
    sendMessage(new Protocol.Message{
        Type = "set_user_stats",
        SetUserStats = new Protocol.SetUserStats{
            UserStats =
                new Protocol.UserStats{
                    Username = clientName,     Race = race,
                    Victory = victory,         Recordt1 = recordt1,
                    Recordt2 = recordt2,       Recordt3 = recordt3,
                    Car1Red = car1red,         Car1Green = car1green,
                    Car1Blue = car1blue,       Car2Red = car2red,
                    Car2Green = car2green,     Car2Blue = car2blue,
                    Car3Red = car3red,         Car3Green = car3green,
                    Car3Blue = car3blue,       Car4Red = car4red,
                    Car4Green = car4green,     Car4Blue = car4blue,
                    Car1Slider = car1slider,   Car1RedTR = car1redTR,
                    Car1GreenTR = car1greenTR, Car1BlueTR = car1blueTR,
                    Car1CursorX = car1cursorX, Car1CursorY = car1cursorY,
                    Car2Slider = car2slider,   Car2RedTR = car2redTR,
                    Car2GreenTR = car2greenTR, Car2BlueTR = car2blueTR,
                    Car2CursorX = car2cursorX, Car2CursorY = car2cursorY,
                    Car3Slider = car3slider,   Car3RedTR = car3redTR,
                    Car3GreenTR = car3greenTR, Car3BlueTR = car3blueTR,
                    Car3CursorX = car3cursorX, Car3CursorY = car3cursorY,
                    Car4Slider = car4slider,   Car4RedTR = car4redTR,
                    Car4GreenTR = car4greenTR, Car4BlueTR = car4blueTR,
                    Car4CursorX = car4cursorX, Car4CursorY = car4cursorY}

        }});
  }
}
