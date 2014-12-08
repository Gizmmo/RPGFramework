using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	private Rigidbody2D playerRigidBody2D;  //RigidBody component for the player
	private float movePlayerVector;			//Variable to track how much movement is needed from input
	private bool facingRight;				//For determining which way the player is currently facing
	private Animator anim;					//Reerence to the player's animator component
	private GameObject playerSprite;		//Reference to the player's sprite GameObject

	public float speed = 4.0f;				//Speed modifier for player movement

	void Awake() {
		playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		playerSprite = transform.Find("PlayerSprite").gameObject;
		anim = (Animator)playerSprite.GetComponent(typeof(Animator));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Get Horizontal Input
		movePlayerVector = Input.GetAxis("Horizontal");

		anim.SetFloat ("speed", Mathf.Abs (movePlayerVector));

		playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);

		if(movePlayerVector > 0 && !facingRight) {
			Flip();
		} else if (movePlayerVector < 0 && facingRight) {
			Flip();
		}
	}

	void Flip() {
		//Switch the way the player is looking
		facingRight = !facingRight;

		//Multiply the players x local scale by -1
		Vector3 theScale = playerSprite.transform.localScale;
		theScale.x *= -1;
		playerSprite.transform.localScale = theScale;
	}
}
