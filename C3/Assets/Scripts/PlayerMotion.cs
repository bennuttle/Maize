using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour {

	public Material testMaterial;

	enum Motion : int {NONE, FORWARD, ROTATE_LEFT, ROTATE_RIGHT, ROTATE_UP, ROTATE_DOWN};

	//Float to be used to signify how far to move forward and how far to cast rays.
	private float intervalDistance = 10f;
	//Bool to tell you that there is an obstacle in front of you so you can't move that way.
	private bool obstacleInFront;
	//Bool which shows whether there is already a queued motion command in progress.
	private bool motionLocked;
	//Int to show whether a character is moving forward or which direction he is rotating.
	private int motionVal;

	private bool isDone;

	private float tempMove;
	private float moveVal;
	// Use this for initialization
	void Start () {
		obstacleInFront = forwardCheck ();
		motionVal = (int) Motion.NONE;
//		Debug.Log (obstacleInFront);
		tempMove = 0f;
		moveVal = 0f;
		isDone = false;

	}
	
	// Update is called once per frame
	void Update () {
		obstacleInFront = forwardCheck ();
		compMotion ();
	}

	void LateUpdate () {
		forwardMotion ();
	}
	
	private void forwardMotion () {
		if (motionVal == (int) Motion.FORWARD) {
			if (tempMove > intervalDistance) {
				transform.Translate (Vector3.forward * (tempMove - intervalDistance));
				motionVal = (int) Motion.NONE;
				tempMove = 0f;
				motionLocked = false;
				forwardCheck ();
			} else {
				moveVal = 3f * Mathf.Lerp (0f, intervalDistance, Time.deltaTime);
				transform.Translate (Vector3.forward * moveVal);
				tempMove += moveVal;
				motionLocked = true;
			}
		}
	}

	private void rotateMotion () {

	}

	//Short function to see whether there is a wall infront of the character.
	private bool forwardCheck () {
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit test;
//		if (Physics.Raycast (transform.position, fwd, intervalDistance, test)) {
//			Debug.Log ("Wall here!");
//			test.transform.gameObject.renderer.material = testMaterial;
//		}
//		Debug.Log (Physics.Raycast (transform.position, fwd, intervalDistance));
		return Physics.Raycast (transform.position, fwd, intervalDistance);
	}

	//DELETE EVENTUALLY
	//Used to test game on computers
	private void compMotion () {
//		Debug.Log (obstacleInFront);
		if (!motionLocked && !obstacleInFront) {
			//Moving forward
			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {
				motionVal = (int) Motion.FORWARD;
				motionLocked = true;
			}
			//Rotating
			if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				motionVal = (int) Motion.ROTATE_LEFT;
				motionLocked = true;
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				motionVal = (int) Motion.ROTATE_RIGHT;
				motionLocked = true;
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
				motionVal = (int) Motion.ROTATE_UP;
				motionLocked = true;
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				motionVal = (int) Motion.ROTATE_DOWN;
				motionLocked = true;
			}
		}
	}

	public bool getIsDone() {
		return isDone;
	}

}
