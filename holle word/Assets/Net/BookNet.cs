using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class BookNet : MonoBehaviour
{
    Socket socket;

    public InputField hostInput;
    public InputField portInput;
    public InputField sendText;
    public Text recvText;
    public Text clientText;
    public Button btn;

    //接收缓冲区
    const int BUFFER_SIZE = 1024;
    byte[] readBuffer = new byte[BUFFER_SIZE];
    
    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void Connetion()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        string host = hostInput.text;
        int port = int.Parse(portInput.text);
        socket.Connect(host, port);
        clientText.text = "客户端地址" + socket.LocalEndPoint.ToString();

        string str = "Hello Unity!";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);

        int count = socket.Receive(readBuffer);
        str = System.Text.Encoding.UTF8.GetString(readBuffer, 0, count);
        recvText.text = str;

        socket.Close();
    }
}
