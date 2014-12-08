using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	//Distance in the x acis the player can move before the camera follows
	public float xMargin = 1.5f;

	//Distance in the y axis the player can move before the camera follows
	public float yMargin = 1.5f;

	//How smoothly the camera catches up to its target in the x axis
	public float xSmooth = 1.5f;

	//How smoothly the camera catches up to its target in the y axis
	public float ySmooth = 1.5f;

	//The max y and x coordinates the camera will have
	private Vector2 maxXAndY;

	//The min y and x coordinates the camera will have
	private Vector2 minXAndY;

	//Reference to the player's tranform
	public Transform player;
	

	void Awake() {

		//Get the bounds for the background texture - world size
		Bounds backgroundBounds = GameObject.Find ("background01").renderer.bounds;
		Vector3 camTopLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
		Vector3 camBottomRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

		//Setting up the refrence
		player = GameObject.Find("Player").transform;

		//automatically set the min and max values
		minXAndY.x = backgroundBounds.min.x - camTopLeft.x;
		maxXAndY.x = backgroundBounds.max.x - camBottomRight.x;

		if (player == null) {
			Debug.LogError("Player object not found");
		}
	}

	//Return true if the distance between the camera and the player in the x axis is greater then the x margin
	bool CheckXMargin() {
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}

	//Return true if the distance between the camera and the player in the y axis is greater then the y margin
	bool CheckYMargin() {
		return Mathf.Abs (transform.position.y - player.position.y) > yMargin;
	}

	void FixedUpdate () {
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		//If the player has moved beyond the x margin
		if(CheckXMargin ()) {
			//The target x coordinate should be a Lerp between the camera's current x position and the players current x position
			targetX = Mathf.Lerp (transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);
		}

		//If the player has moced beyond the y margin
		if(CheckYMargin()) {
			//the target y coord should Lerp between the camera's current y position and the players current y position
			targetY = Mathf.Lerp (transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);
		}

		//The target x and y cxoord should not be larger then the max or smaller then the min
		targetX = Mathf.Clamp (targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp (targetY, minXAndY.y, maxXAndY.y);

		//Set the camera position to the target pos with the same z component
		transform.position = new Vector3(targetX, targetY, transform.position.z);

	}
}
