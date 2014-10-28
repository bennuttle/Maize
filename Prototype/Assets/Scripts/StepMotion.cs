using UnityEngine;
using System.Collections;

public class StepMotion : MonoBehaviour {

	private bool canMove;
	private bool turnLeft, turnRight, turnUp, turnDown;
	private bool moveForward;
	private float rotateVal = 0f;
	private float moveVal = 0f;
	private float tempRot = 0f;
	private float tempOr = 0f;
	private float tempMove = 0f;
	private bool isDone = false;

	public float minSwipeX;
	public float minSwipeY;
	private Vector2 startPos;


	private int turnCounter = 0;
	private int swipeCounter = 0;

	// Use this for initialization
	void Start () {
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		SwipeChecker ();
		if (canMove) {
			//Rotation
			if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				turnLeft = true;
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				turnRight = true;
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
				turnUp = true;
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				turnDown = true;
			}

			//Moving forward
			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {
				moveForward = true;
			}

			//Restart the level
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}	
	}

	void LateUpdate () {
		rotateChecker ();
		forwardChecker ();
	}

	private void forwardChecker () {
		if (moveForward) {
			if (tempMove > 10) {
				transform.Translate (Vector3.forward * (tempMove - 10));
				moveForward = false;
				tempMove = 0f;
				canMove = true;
//				forwardCheck ();
			} else {
				moveVal = Mathf.Lerp (0f, 10f, Time.deltaTime);
				transform.Translate (Vector3.forward * moveVal);
				tempMove += moveVal;
				canMove = false;
			}
		}
	}

	private void rotateChecker () {
		if (turnLeft) {
			if (tempRot > 90f) {
				transform.Rotate (new Vector3 (0, -1 * (tempRot - 90f), 0));
				turnLeft = false;
				tempRot = 0f;
				canMove = true;
//				forwardCheck ();
			} else {
				rotateVal = Mathf.Lerp (0f, 90f, Time.deltaTime);
				transform.Rotate (new Vector3 (0, rotateVal, 0));
				tempRot += rotateVal;
				canMove = false;
			}
		} else if (turnRight) {
			if (tempRot < -90) {
				transform.Rotate (new Vector3 (0, -1 * (tempRot + 90f), 0));
				turnRight = false;
				tempRot = 0f;
				canMove = true;
//				forwardCheck ();
			} else {
				rotateVal = Mathf.Lerp (0f, -90f, Time.deltaTime);
				transform.Rotate (new Vector3 (0, rotateVal, 0));
				tempRot += rotateVal;
				canMove = false;
			}
		} else if (turnUp) {
			if (tempOr < -90f) {
				transform.Rotate (-1 * new Vector3((tempOr + 90f), 0, 0));
				turnUp = false;
				tempOr = 0f;
				canMove = true;
//				forwardCheck ();
			} else {
				rotateVal = Mathf.Lerp (0f, -90f, Time.deltaTime);
				transform.Rotate (new Vector3(rotateVal, 0, 0));
				tempOr += rotateVal;
				canMove = false;
			}
		} else if (turnDown) {
			if (tempOr > 90f) {
				transform.Rotate (-1 * new Vector3((tempOr - 90f), 0, 0));
				turnDown = false;
				tempOr = 0f;
				canMove = true;
//				forwardCheck ();
			} else {
				rotateVal = Mathf.Lerp (0f, 90f, Time.deltaTime);
				transform.Rotate (new Vector3(rotateVal, 0, 0));
				tempOr += rotateVal;
				canMove = false;
			}
		}
	}

	private void SwipeChecker () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			canMove = false;
			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				float swipeVerDist = (new Vector2 (0, touch.position.y) - new Vector2 (0, startPos.y)).magnitude;
				float swipeHorDist = (new Vector2 (touch.position.x, 0) - new Vector2 (startPos.x, 0)).magnitude;
				if (swipeVerDist < minSwipeY && swipeHorDist < minSwipeX) {
					moveForward = true;
				} else if (swipeHorDist > swipeVerDist) {
					if (swipeHorDist > minSwipeX) {
						if (Mathf.Sign (touch.position.x - startPos.x) > 0) {
							turnLeft = true;
						} else {
							turnRight = true;
						}
					} 
				} else {
					if (swipeVerDist > minSwipeY) {
						if (Mathf.Sign (touch.position.y - startPos.y) > 0) {
							turnUp = true;
						} else {
							turnDown = true;
						}
					}
				}
				break;
			}
		}
	}
	void OnTriggerEnter(Collider coll) {
				//canMove = false;
		moveForward = false;
		Debug.Log ("test");
		isDone = true;
		}
	public bool getIsDone() {
		return isDone;
	}

//	void OnCollisionEnter(Collision collide) {
//		Debug.Log ("testing123");
//		}
//
//	private void forwardCheck () {
//		Vector3 fwd = transform.TransformDirection (Vector3.forward);
//		RaycastHit test;
//		if (Physics.Raycast (transform.position, fwd, 10f, test)) {
//			Debug.Log ("Wall here!");
//			test.transform.gameObject.renderer.material = testMaterial;
//		} else {
//			Debug.Log ("No Wall!");
//		}
//	}

}
