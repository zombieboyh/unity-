using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float maxSpeed = 5f;
    public float moveforce = 300;
    public float jumpforce = 500;
    private Rigidbody2D herobody;
    private bool bFaceright = true;
    private bool bJump = false;
    public Transform mGroundCheck;
	// Use this for initialization
	void Start () {
        herobody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
        //herobody.simulated=false;
	}
private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        //未到最大速度，加力
        //if (Mathf.Abs(herobody.velocity.x) < maxSpeed)
        if(h*herobody.velocity.x < maxSpeed)
            herobody.AddForce(Vector2.right * h * moveforce);
        //限速
        if (Mathf.Abs(herobody.velocity.x) > maxSpeed)
            herobody.velocity = new Vector2(Mathf.Sign(herobody.velocity.x) * maxSpeed,herobody.velocity.y);
        //Debug.Log(Mathf.Sign(h));
        if (h > 0 && !bFaceright)
            Flip();
        else if(h < 0 && bFaceright)
        Flip();

        if (bJump)
        {
            herobody.AddForce(new Vector2(0, jumpforce));
            bJump = false;
        }
    }
	// Update is called once per frame
	void Update () {
        bool bGrounded = Physics2D.Linecast(herobody.transform.position,
            mGroundCheck.position,
            1<<LayerMask.NameToLayer("Ground"));
        if (bGrounded && Input.GetButtonDown("Jump"))
        {
            bJump = true;
        }
        //Debug.DrawLine(herobody.transform.position,mGroundCheck.position,)
	}
    void Flip()
    {
        bFaceright = !bFaceright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
      

    }
}
