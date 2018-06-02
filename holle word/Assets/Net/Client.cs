using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;


public class Client : MonoBehaviour
{
    public InputField hostInput;
    public InputField portInput;
    //public InputField sendText;
    public Text recvText;
    public Text clientText;
    public Button btn;

    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    string host = "127.0.0.1";
    int port = 10001;
    string str1;

    private byte[] readBuff = new byte[1024];
    float lastTickTime;
    bool ConnectedOrNot = false;

    public Canvas canv;
    void Start()
    {
        lastTickTime = Time.time;
        socket.Connect(host, port);
        ConnectedOrNot = true;
        socket.BeginReceive(readBuff, 0, 1024, 0, receiveCb, socket);

    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Debug.Log("btn awake");
    }

    void Update()
    {

        string str = "heartBeat ";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);

        if (ConnectedOrNot)
            if (Time.time - lastTickTime > 2)
            {
                socket.Send(bytes);
                lastTickTime = Time.time;
            }
        recvText.text = str1;
    }

    public void client()
    {
        SendString("createRoom ");
    }

    public void SendString(String str)
    {

        byte[] sendBuff = System.Text.Encoding.Default.GetBytes(str);
        try {
            socket.Send(sendBuff);
        }
        catch (Exception e)
        { clientText.text = "发送" + str + "时出现空对象错误"; }
    }

    public void receiveCb(IAsyncResult ar)
    {
        Socket sock = (Socket)ar.AsyncState;
        int num = sock.EndReceive(ar);
        str1 = System.Text.Encoding.UTF8.GetString(readBuff, 0, num);
        sock.BeginReceive(readBuff, 0, 1024, 0, receiveCb, socket);
    }

     public void exit()
    {
        socket.Close();
    }

      public void onCreatRoom()
    {
        SendString("createRoom ");
        Invoke("refreshRoomList",1f);
    }

    void refreshRoomList()
    {
        string[] info = (recvText.text).Split( ' ' );
        GameObject.Find("Canvas").GetComponent<refreshRoomList>().refreshRoomList1(info);
    }
}

