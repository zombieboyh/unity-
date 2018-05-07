using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {
    private Transform hero;
    public float Xdist = 3f;
    public float Ydist = 3f;
    public Vector2 minxy;
    public Vector2 maxxy;
    public float xsmooth = 5f;
    public float ysmooth = 5f;
	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Player").transform;

    }
	// Update is called once per frame
	void Update () {
        /*transform.position = new Vector3(hero.position.x, hero.position.y,transform.position.z);*/
        TrackCam();
    }
    void TrackCam()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;
        if (IsMoveX())
        {
            newX = Mathf.Lerp(transform.position.x,
                  hero.position.x,
                  xsmooth * Time.deltaTime);
        }

        if (IsMoveY())
        {
            newY = Mathf.Lerp(transform.position.y,
                hero.position.y,
                ysmooth * Time.deltaTime);
        }
        newX = Mathf.Clamp(newX, minxy.x, maxxy.x);
        newY = Mathf.Clamp(newY, minxy.y, maxxy.y);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
    bool IsMoveX()
    {
        if (Mathf.Abs(transform.position.x - hero.position.x) > Xdist)
            return true;
        else return false;

    }
    bool IsMoveY()
    {
        if (Mathf.Abs(transform.position.y - hero.position.y) > Ydist)
            return true;
        else return false;

    }
    /*void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform; 
    }
    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - PlayerControl.position.x) > xMargin;
    }
    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - PlayerControl.position.y) > yMargin;
    }
    void FixedUpdate()
    {
        TrackPlayer();  
    }
    void TrackPlayer()
    {
        float CamNewX = transform.position.x;
        float CamNewY = transform.position.y;
        if (MoveX())//计算摄像机位置
            CamNewX = Mathf.Lerp(transform.position.x,
            Player.position.x,SmoothX*Time.deltaTime);
        if (MoveY())
            CamNewX = Mathf.Lerp(transform.position.y,
            Player.position.y, SmoothY * Time.deltaTime);

    }*/
}
