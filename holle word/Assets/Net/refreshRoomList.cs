using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class refreshRoomList : MonoBehaviour
{
    GameObject[] addressObj;
    GameObject[] enterRoomBtns;

    public Connect connect;
    Socket socket;

    private void Start()
    {
        this.socket = connect.socket;
    }
    public void refreshRoomList1(String[] args)
    {
        //清空
        if (addressObj != null)
            for (int i = 0; i < addressObj.Length; i++)
                Destroy(addressObj[i]);
        if (enterRoomBtns != null)
            for (int i = 0; i < enterRoomBtns.Length; i++)
                Destroy(enterRoomBtns[i]);

        int roomCount = Convert.ToInt32(args[1]);
       
        addressObj = new GameObject[roomCount];
        enterRoomBtns = new GameObject[roomCount];
        GameObject content = GameObject.Find("Canvas/Panel/ScrollRect/Content");
        //content.GetComponent<RectTransform>().sizeDelta = new Vector2(217, 50 * roomCount);
        for (int i = 0; i < roomCount; i++)
        {
            addressObj[i] = GameObject.Instantiate(Resources.Load("textRoomItem", typeof(GameObject))) as GameObject;
            addressObj[i].transform.SetParent(GameObject.Find("Canvas/Panel/ScrollRect/Content").transform, false);
            addressObj[i].GetComponent<Text>().text = args[i + 2];
            addressObj[i].transform.localScale = new Vector3(0.53f, 1.07f, 1f);

            enterRoomBtns[i] = GameObject.Instantiate(Resources.Load("enterRoomBtn", typeof(GameObject))) as GameObject;
            enterRoomBtns[i].transform.SetParent(GameObject.Find("Canvas/Panel/ScrollRect/Content").transform, false);
            enterRoomBtns[i].transform.localScale = new Vector3(0.39f, 0.54f, 1f);

            enterRoomBtns[i].GetComponent<EnterRoomBtnNum>().setNum(i);            //保存编号在对象中
            GameObject tempObj = enterRoomBtns [i];   //保存对象
            enterRoomBtns[i].GetComponent<Button>().onClick.AddListener(delegate () 
            {
                this.OnClick(tempObj);
            });
            Debug.Log(i);
            if (args[i + 2] == socket.LocalEndPoint.ToString())
            {
                enterRoomBtns[i].GetComponent<Button>().enabled = false;
                enterRoomBtns[i].transform.Find("Text").GetComponent<Text>().text = "自己";
            }

            

        }
    }

    public void OnClick(GameObject sender)
    {
        int num = sender.GetComponent<EnterRoomBtnNum>().getNum();
        String strRoomAddr = addressObj[num].GetComponent<Text>().text;
        SendEnterRoom(strRoomAddr);

    }

    public void SendEnterRoom(String roomAddress)
    {
        String str = "enterRoom " + roomAddress;
        connect.SendString(str);
        Debug.Log("sendEnterRoom");
    }

}
