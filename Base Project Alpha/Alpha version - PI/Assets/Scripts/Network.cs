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
 private
  int fps = 30;

  void Awake() {
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = fps;
  }

  // Use this for initialization
 private
  void Start() {

     carsContainer = GameObject.Find("Cars");

        players = new Hashtable();
    clientName = "Unity-" + new System.Random().Next(1, 65536);
    Debug.Log("network started");

    string host = "pi-2.ludovic-muller.fr";
    Int32 port = 4242;

    try {
      socket = new TcpClient(host, port);
      stream = socket.GetStream();
      socketReady = true;

      Debug.Log("socket is ready");
      UpdatePlayerStatus();
    } catch (Exception e) {
      Debug.Log("socket error: " + e.Message);
    }
  }

  // Update is called once per frame
 private
  void Update() {
    if (Application.targetFrameRate != fps)
      Application.targetFrameRate = fps;
    if (socketReady) {
      while (stream.DataAvailable) {
        onIncomingData();
      }
    }
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
  GameObject getPlayer(string clientName) {
    GameObject player;
    if (!players.ContainsKey(clientName)) {
      player = Instantiate(carPrefab, carsContainer.transform) as GameObject;
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

        player = getPlayer(user);

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
        user = ups.User;

        if (user == clientName)
          break;

        player = getPlayer(user);

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
        Destroy(getPlayer(user));
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
      default:
        Debug.LogWarning("unsupported message type for " + parsedData);
        break;
    }
  }

  // tell the network to send player's current position to all others
 public
  void updatePlayerPosition(Protocol.Vector vecPosition,
                            Protocol.Vector vecRotation,
                            Protocol.Vector vecScale,
                            Protocol.Vector vecVelocity) {
    // all together
    Protocol.UpdatePlayerPosition upp = new Protocol.UpdatePlayerPosition{
        Position = vecPosition, Direction = vecRotation, Scale = vecScale,
        User = clientName, Velocity = vecVelocity};

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
                          int supposedNextCheckpointNumber) {
    // all together
    Protocol.UpdatePlayerStatus ups = new Protocol.UpdatePlayerStatus{
        LapCount = lapCount,
        HasHitSFLineOnce = hasHitSFLineOnce,
        CpCount = cpCount,
        NextCheckpointNumber = nextCheckpointNumber,
        SupposedNextCheckpointNumber = supposedNextCheckpointNumber,
        User = clientName};

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
}
