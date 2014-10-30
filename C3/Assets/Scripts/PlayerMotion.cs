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

	private float tempMove;
	private float tempRot;
	private float tempOr;
	private float moveVal;
	private float rotateVal;

	private bool isDone;
	private bool isPaused;

	// Use this for initialization
	void Start () {
		obstacleInFront = forwardCheck ();
		motionVal = (int) Motion.NONE;
		changeFloor ();
		tempMove = 0f;
		tempRot = 0f;
		tempOr = 0f;
		moveVal = 0f;
		rotateVal = 0f;
		isDone = false;
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkPause ();
		obstacleInFront = forwardCheck ();

//		Debug.Log (obstacleInFront);
		compMotion ();
		//Debug.Log (Time.time + motionLocked);
	}

	void LateUpdate () {
		forwardMotion ();
		rotateMotion ();
	}
	
	private void forwardMotion () {
//		changeFloor ();
		if (motionVal == (int) Motion.FORWARD) {
			if (tempMove > intervalDistance) {
				//transform.Translate (Vector3.forward * (tempMove - intervalDistance));
				motionVal = (int) Motion.NONE;
				tempMove = 0f;
				motionLocked = false;

				Vector3 newPos = new Vector3((int)transform.position.x, (int) transform.position.y, (int) transform.position.z);

				newPos.x = Mathf.Round (newPos.x / 5f) * 5;
				newPos.y = Mathf.Round (newPos.y / 5f) * 5;
				newPos.z = Mathf.Round (newPos.z / 5f) * 5;

				transform.position = newPos;
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
		if (motionVal == (int) Motion.ROTATE_LEFT) {
			if (tempRot > 90f) {
				transform.Rotate (new Vector3 (0, -1 * (tempRot - 90f), 0));
				motionVal = (int) Motion.NONE;
				tempRot = 0f;

				motionLocked = false;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, 90f, Time.deltaTime);
				transform.Rotate (new Vector3 (0, rotateVal, 0));
				tempRot += rotateVal;
				motionLocked = true;
			}
		} else if (motionVal == (int) Motion.ROTATE_RIGHT) {
			if (tempRot < -90) {
				transform.Rotate (new Vector3 (0, -1 * (tempRot + 90f), 0));
				motionVal = (int) Motion.NONE;
				tempRot = 0f;

				motionLocked = false;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, -90f, Time.deltaTime);
				transform.Rotate (new Vector3 (0, rotateVal, 0));
				tempRot += rotateVal;
				motionLocked = true;
			}
		} else if (motionVal == (int) Motion.ROTATE_UP) {
			if (tempOr < -90f) {
				transform.Rotate (-1 * new Vector3((tempOr + 90f), 0, 0));
				motionVal = (int) Motion.NONE;
				tempOr = 0f;

				motionLocked = false;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, -90f, Time.deltaTime);
				transform.Rotate (new Vector3(rotateVal, 0, 0));
				tempOr += rotateVal;
				motionLocked = true;
			}
		} else if (motionVal == (int) Motion.ROTATE_DOWN) {
			if (tempOr > 90f) {
				transform.Rotate (-1 * new Vector3((tempOr - 90f), 0, 0));
				motionVal = (int) Motion.NONE;
				tempOr = 0f;

				motionLocked = false;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, 90f, Time.deltaTime);
				transform.Rotate (new Vector3(rotateVal, 0, 0));
				tempOr += rotateVal;
				motionLocked = true;
			}
		}
	}

	//Short function to see whether there is a wall infront of the character.
	private bool forwardCheck () {
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit test;
//		if (Physics.Raycast (transform.position, fwd, intervalDistance, test)) {
//			Debug.Log ("Wall here!");
//			test.transform.gameObject.renderer.material = testMaterial;
//		}
//		return Physics.Raycast (transform.position, fwd, intervalDistance);
		Physics.Raycast (transform.position, fwd, out test);
		if (test.transform.gameObject.collider.isTrigger) {
//			Debug.Log("test1"+test.transform.gameObject.collider.isTrigger);
			return false;
		} else {
//			Debug.Log("test2"+test.transform.gameObject.collider.isTrigger);
			return Physics.Raycast (transform.position, fwd, intervalDistance);
		}
	}

	private void changeFloor () {
		Vector3 dwn = transform.TransformDirection (Vector3.down);
		RaycastHit test;
		Physics.Raycast (transform.position, dwn, out test);
		test.transform.gameObject.renderer.material = testMaterial;
		Debug.Log ("test1" + test.transform.gameObject);
	}

	//DELETE EVENTUALLY
	//Used to test game on computers
	private void compMotion () {
		if (!motionLocked && !isPaused && !isDone) {
			//Moving forward
			if (Input.GetKeyDown (KeyCode.UpArrow)&& !obstacleInFront || Input.GetKeyDown (KeyCode.W)&& !obstacleInFront) {
				motionVal = (int) Motion.FORWARD;
				motionLocked = true;
				changeFloor ();
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

	public void finish() {
		this.isDone = true;
	}

	public bool getIsPaused() {
		return isPaused;
	}

	private void checkPause() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused) {
				Application.Quit(); 
			} else {
				isPaused = true;
			}
		}
	}
	void OnTriggerEnter(Collider col) {
		finish ();
	}

	public void unPause() {
		isPaused = false;
	}
}
