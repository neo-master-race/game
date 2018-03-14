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
    string clientName = "Unity-" + new System.Random().Next(1, 65536);

    // Use this for initialization
    void Start()
    {
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

    private void sendData(string data) {
        Protocol.ChatMessage chatMsg = new Protocol.ChatMessage{
            User = clientName,
            Content = data
        };

        Protocol.Message msg = new Protocol.Message{
            Type = "chat_message",
            ChatMessage = chatMsg
        };

        sendMessage(msg);
    }


    private void sendMessage(Protocol.Message msg) {
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

    // when user sends a message
    public void onSendAction() {
        GameObject go = GameObject.Find("InputField");
        string message = go.GetComponent<InputField>().text;
        sendData(message);
        go.GetComponent<InputField>().text = "";
    }

    // when there are some incoming datas, do something with them
    private void onIncomingData() {
        Byte[] data = new Byte[4];

        stream.Read(data, 0, 4); // read an int

        if (!BitConverter.IsLittleEndian) {
            Array.Reverse(data);
        }

        int dataLength = BitConverter.ToInt32(data, 0);
        Debug.Log("LENGTH=" + dataLength);

        data = new Byte[dataLength];
        Int32 bytes = stream.Read(data, 0, data.Length);

        Protocol.Message parsedData;
        try {
            parsedData = Protocol.Message.Parser.ParseFrom(data);
        } catch (Exception e) {
            String responseData = String.Empty;
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.LogWarning("cannot parse incoming message (" + responseData + ").\n\n" + e);
            return;
        }
        Protocol.ChatMessage chatMsg = parsedData.ChatMessage;

        Debug.Log("Received from " + chatMsg.User + " the following message: " + chatMsg.Content);
    }

    // Update is called once per frame
    void Update()
    {
        if (socketReady) {
            while (stream.DataAvailable) {
               onIncomingData();
            }
        }
    }



    public void updatePlayerPosition(Protocol.Vector vecPosition, Protocol.Vector vecRotation, Protocol.Vector vecScale) {
        // all together
        Protocol.UpdatePlayerPosition upp = new Protocol.UpdatePlayerPosition{
          Position = vecPosition,
          Direction = vecRotation,
          Scale = vecScale,
          User = "Unity"
        };

        // final message that we can send
        Protocol.Message msg = new Protocol.Message{
          Type = "update_player_position",
          UpdatePlayerPosition = upp
        };

        sendMessage(msg);
    }
}
