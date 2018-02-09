using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 1, jumpVelocity = 1;
	public LayerMask playerMask; 
	public bool canMoveInAir = true;
    Transform myTrans, tagGround;
    Rigidbody2D myBody;

    bool isGrounded = false; //Only can jump when on the ground

	// Use this for initialization
	void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
		tagGround = GameObject.Find(this.name+"/tag_ground").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//true if hits nothing false if hits somthing
		isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);
		
		//Change directional inputs with /Edit/InputManager
        Move(Input.GetAxisRaw("Horizontal"));
		if(Input.GetButtonDown("Jump")){
			Jump();
		}
	}

    public void Move(float horizontalInput)
    {
		if(!canMoveInAir && !isGrounded) return;
        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity = moveVel;
    }
	
	public void Jump(){
		if(isGrounded){
			myBody.velocity += jumpVelocity * Vector2.up;
		}
	}
}
