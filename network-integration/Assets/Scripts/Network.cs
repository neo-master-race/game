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

  // Use this for initialization
 private
  void Start() {
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
    } catch (Exception e) {
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
  }

  // send a message (UpdatePlayerPosition, ChatMessage, ...) to the socket
 private
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

  // filter incoming message using his type
 private
  void filterIncomingMessages(Protocol.Message parsedData) {
    switch (parsedData.Type) {
      case "update_player_position":
        Protocol.UpdatePlayerPosition upp = parsedData.UpdatePlayerPosition;
        Protocol.Vector vecPos = upp.Position;
        Protocol.Vector vecRot = upp.Direction;
        Protocol.Vector vecScale = upp.Scale;
        string user = upp.User;

        if (user == clientName) break;

        GameObject player;
        if (!players.ContainsKey(user)) {
          player = Instantiate(carPrefab, carsContainer.transform) as GameObject;
          players.Add(user, player);
        } else {
          player = players[user] as GameObject;
        }

        player.transform.localPosition = new Vector3(vecPos.X, vecPos.Y, vecPos.Z);
        player.transform.localEulerAngles =
            new Vector3(vecRot.X, vecRot.Y, vecRot.Z);
        player.transform.localScale =
            new Vector3(vecScale.X, vecScale.Y, vecScale.Z);
        break;
      case "chat_message":
        Protocol.ChatMessage chatMsg = parsedData.ChatMessage;
        Debug.Log("Received from " + chatMsg.User +
                  " the following message: " + chatMsg.Content);
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
                            Protocol.Vector vecScale) {
    // all together
    Protocol.UpdatePlayerPosition upp = new Protocol.UpdatePlayerPosition{
        Position = vecPosition, Direction = vecRotation, Scale = vecScale,
        User = clientName};

    // final message that we can send
    Protocol.Message msg = new Protocol.Message{Type = "update_player_position",
                                                UpdatePlayerPosition = upp};

    sendMessage(msg);
  }
}
