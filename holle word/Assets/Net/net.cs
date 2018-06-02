using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class net : MonoBehaviour
{
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    public InputField hostInput;
    public InputField portInput;
    public InputField sendText;
    public Text recvText;
    public Text clientText;
    public Button btn;

    const int BUFFER_SIZE = 1024;
    byte[] readBuffer = new byte[BUFFER_SIZE];

    float lastTickTime;
    Boolean ConnectedOrNot = false;

    public void Start()
    {
        lastTickTime = Time.time;
        recvText = GameObject.Find("Canvas/Panel/RecvText").GetComponent<Text>();
    }

    public void Update()
    {
        string str = "heartBeat";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);

        if (ConnectedOrNot) 
        if (Time.time - lastTickTime > 2)
        {
            socket.Send(bytes);
            lastTickTime = Time.time;
        }
    }

    public void Connetion()
    {
        
        string host = "127.0.0.1";
        int port = 10000;
        socket.Connect(host, port);
        //socket.BeginReceive(readBuffer, 0, 1024, 0, receiveCb, socket);
        //string str = "";
        ////Recv
        //int count = socket.Receive(readBuffer);
        //str = System.Text.Encoding.UTF8.GetString(readBuffer, 0, count);
        //recvText.text = str;
        ConnectedOrNot = true;
        SendText("hello serv");
        //Close
        //socket.Close();
    }     

    public void SendText(string str)
    {
        //Send
        //str = sendText.text;//这一句会出错
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);
    }

    public void receiveCb(IAsyncResult ar)
    {
        Socket sock = (Socket)ar.AsyncState;
        int num = sock.EndReceive(ar);
        string str = System.Text.Encoding.UTF8.GetString(readBuffer, 0, num);
        recvText.text = str; //这一句会出错

        sock.BeginReceive(readBuffer, 0, 1024, 0, receiveCb, socket);
    }

    
   
}
