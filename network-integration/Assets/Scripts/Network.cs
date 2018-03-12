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

    // Update is called once per frame
    void Update()
    {
        if (socketReady) {
            if (stream.DataAvailable) {
                Byte[] data = new Byte[4];

                stream.Read(data, 0, 4); // read an int

                if (!BitConverter.IsLittleEndian) {
                    Array.Reverse(data);
                }

                int i = BitConverter.ToInt32(data, 0);
                Debug.Log("DEBUG=" + i);

                data = new Byte[i];
                Int32 bytes = stream.Read(data, 0, data.Length);

                // Protocol.Message msg = new Protocol.Message{
                //     User = "moi",
                //     Content = "hello world!"
                // };

                // Debug.Log(msg);

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
        }
    }
}
