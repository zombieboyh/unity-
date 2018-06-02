using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect : MonoBehaviour
{
    public Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    string host = "127.0.0.1";
    int port = 10001;

    private byte[] readBuff = new byte[1024];
    float lastTickTime;
    bool ConnectedOrNot = false;
    string receiveCbStr;
    public static string[] args;


    public Canvas canv;
    public Text errorText;
    public Text recvText;

    
    void Start ()
    {
        lastTickTime = Time.time;
        socket.Connect(host, port);
        ConnectedOrNot = true;
        socket.BeginReceive(readBuff, 0, 1024, 0, receiveCb, socket);
        receiveCbStr = "roomlist0";

    }


    void Update ()
    {
        string str = "heartBeat ";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        string[] info = receiveCbStr.Split(' ');

        if (ConnectedOrNot)
            if (Time.time - lastTickTime > 2)
            {
                socket.Send(bytes);
                lastTickTime = Time.time;
            }

        if ("beginGame" == info[0].Trim())
        {
            canv.GetComponent<CanvasGroup>().alpha = 0;
            canv.GetComponent<CanvasGroup>().interactable = false;
            canv.GetComponent<CanvasGroup>().blocksRaycasts = false;
            SceneManager.LoadScene(1);
        }
        //刷新recvText
        recvText.text = receiveCbStr;

    }

    //接收回调
    public void receiveCb(IAsyncResult ar)
    {
        Socket sock = (Socket)ar.AsyncState;
        int num = sock.EndReceive(ar);
        receiveCbStr = System.Text.Encoding.UTF8.GetString(readBuff, 0, num);
        sock.BeginReceive(readBuff, 0, 1024, 0, receiveCb, socket);
    }

    public void SendString(String str)
    {

        byte[] sendBuff = System.Text.Encoding.Default.GetBytes(str);
        try
        {
            socket.Send(sendBuff);
        }
        catch (Exception e)
        { errorText.text = "发送" + str + "时出现空对象错误"; }
        Debug.Log("connect.SendString "+str);
    }

    public void exit()
    {
        socket.Close();
    }
}
