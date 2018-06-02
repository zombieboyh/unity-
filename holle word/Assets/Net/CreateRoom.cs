using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    public GameObject btnCreateRoom;
    public Connect connect;
    public Text recvText;

    void Start () {
		
	}
	
	
	void Update () {
		
	}
    //创建房间
    public void onCreatRoom()
    {
        connect.SendString("createRoom ");
        Invoke("refreshRoomList", 0.7f);
        Invoke("invokeSetUnActivity", 1f);
    }

    void refreshRoomList()
    {
        string[] info = (recvText.text).Split(' ');
        GameObject.Find("Canvas").GetComponent<refreshRoomList>().refreshRoomList1(info);
    }

    //刷新房间列表
    public void onRefreshRoom()
    {
        Invoke("refreshRoomList", 0.5f);
    }

    void invokeSetUnActivity()
    {
        btnCreateRoom.gameObject.SetActive(false);
    }
}
