using UnityEngine;
using System.Collections;
using Maize;
using System;

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

	private Vector2 startPos;
	private float minSwipeX = 150f;
	private float minSwipeY = 150f;
	//Screen width and height;
	private float screenWidth;
	private float screenHeight;

	//Current Maze Information
	private int stepsForward;
	private int turns;
	private int oldTotalSteps;
	private int oldTotalTurns;
	private int level;
	private

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

		stepsForward = 0;
		turns = 0;
		oldTotalSteps = PlayerPrefs.GetInt ("TotalSteps");
		oldTotalTurns = PlayerPrefs.GetInt ("TotalTurns");

		screenWidth = Screen.width;
		screenHeight = Screen.height;
	



	}
	
	// Update is called once per frame
	void Update () {
		//To make sure the screen didn't rotate.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		

		obstacleInFront = forwardCheck ();

//		Debug.Log (obstacleInFront);
//		tapDetection ();
		SwipeChecker ();
		compMotion ();
		//Debug.Log (Time.time + motionLocked);
	}

	void LateUpdate () {
		forwardMotion ();
		rotateMotion ();
		checkPause ();

		updateCurrentMazeStats ();		
	}

	private void SwipeChecker () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			motionLocked = true;
			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				float swipeVerDist = (new Vector2 (0, touch.position.y) - new Vector2 (0, startPos.y)).magnitude;
				float swipeHorDist = (new Vector2 (touch.position.x, 0) - new Vector2 (startPos.x, 0)).magnitude;
				if (swipeVerDist < minSwipeY && swipeHorDist < minSwipeX) {
					if (motionVal == (int) Motion.NONE && !obstacleInFront) {
						motionVal = (int) Motion.FORWARD;
					}
				} else if (swipeHorDist > swipeVerDist) {
					if (PlayerPrefs.GetInt("InvertX") == 0) {
						if (swipeHorDist > minSwipeX) {
							if (Mathf.Sign (touch.position.x - startPos.x) < 0) {
								if (motionVal == (int) Motion.NONE) {	
									motionVal = (int) Motion.ROTATE_LEFT;
									changeFloor ();
								}
							} else {
								if (motionVal == (int) Motion.NONE) {
									motionVal = (int) Motion.ROTATE_RIGHT;
									changeFloor ();
								}
							}
						}
					} else {
						if (swipeHorDist < minSwipeX) {
							if (Mathf.Sign (touch.position.x - startPos.x) < 0) {
								if (motionVal == (int) Motion.NONE) {	
									motionVal = (int) Motion.ROTATE_LEFT;
									changeFloor ();
								}
							} else {
								if (motionVal == (int) Motion.NONE) {
									motionVal = (int) Motion.ROTATE_RIGHT;
									changeFloor ();
								}
							}
						}
					}
				} else {
					if (PlayerPrefs.GetInt("InvertY") == 0) {
						if (swipeVerDist > minSwipeY) {
							if (Mathf.Sign (touch.position.y - startPos.y) < 0) {
								if (motionVal == (int) Motion.NONE) {
									motionVal = (int) Motion.ROTATE_UP;
									changeFloor ();
								}
							} else {
								if (motionVal == (int) Motion.NONE) {
									motionVal = (int) Motion.ROTATE_DOWN;
									changeFloor ();
								}
							}
						}
					} else {
						if (swipeVerDist < minSwipeY) {
							if (Mathf.Sign (touch.position.y - startPos.y) < 0) {
								if (motionVal == (int) Motion.NONE) {
									motionVal = (int) Motion.ROTATE_UP;
									changeFloor ();
								}
							} else {
								if (motionVal == (int) Motion.NONE) {
									motionVal = (int) Motion.ROTATE_DOWN;
									changeFloor ();
								}
							}
						}
					}
				}
				break;
			}
		}
	}

	private void forwardMotion () {
//		changeFloor ();
		if (motionVal == (int) Motion.FORWARD && !isPaused && !isDone) {
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
				changeFloor ();

				stepsForward++;
			} else {
				moveVal = 3f * Mathf.Lerp (0f, intervalDistance, Time.deltaTime);
				transform.Translate (Vector3.forward * moveVal);
				tempMove += moveVal;
				motionLocked = true;
			}
		}

	}


	private void rotateMotion () {
		if (motionVal == (int) Motion.ROTATE_LEFT && !isPaused && !isDone) {
			if (tempRot > 90f) {
				transform.Rotate (new Vector3 (0, -1 * (tempRot - 90f), 0));
				motionVal = (int) Motion.NONE;
				tempRot = 0f;
				changeFloor ();
				motionLocked = false;
				turns++;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, 90f, Time.deltaTime);
				transform.Rotate (new Vector3 (0, rotateVal, 0));
				tempRot += rotateVal;
				motionLocked = true;
			}
		} else if (motionVal == (int) Motion.ROTATE_RIGHT && !isPaused && !isDone) {
			if (tempRot < -90) {
				transform.Rotate (new Vector3 (0, -1 * (tempRot + 90f), 0));
				motionVal = (int) Motion.NONE;
				tempRot = 0f;
				changeFloor ();
				motionLocked = false;
				turns++;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, -90f, Time.deltaTime);
				transform.Rotate (new Vector3 (0, rotateVal, 0));
				tempRot += rotateVal;
				motionLocked = true;
			}
		} else if (motionVal == (int) Motion.ROTATE_UP && !isPaused && !isDone) {
			if (tempOr < -90f) {
				transform.Rotate (-1 * new Vector3((tempOr + 90f), 0, 0));
				motionVal = (int) Motion.NONE;
				tempOr = 0f;
				changeFloor ();
				motionLocked = false;
				turns++;
			} else {
				rotateVal = 3f * Mathf.Lerp (0f, -90f, Time.deltaTime);
				transform.Rotate (new Vector3(rotateVal, 0, 0));
				tempOr += rotateVal;
				motionLocked = true;
			}
		} else if (motionVal == (int) Motion.ROTATE_DOWN && !isPaused && !isDone) {
			if (tempOr > 90f) {
				transform.Rotate (-1 * new Vector3((tempOr - 90f), 0, 0));
				motionVal = (int) Motion.NONE;
				tempOr = 0f;
				changeFloor ();
				motionLocked = false;
				turns++;
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

		//FOR ASH'S TUTORIAL LEVEL
		//if (!Physics.Raycast (transform.position, fwd)) return false;

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
		Vector3 rght = transform.TransformDirection (Vector3.right);
		Vector3 lft = transform.TransformDirection (Vector3.left);
		Vector3 top = transform.TransformDirection (Vector3.up);
		Vector3 bck = transform.TransformDirection (Vector3.back);
		RaycastHit test;
		Physics.Raycast (transform.position, dwn, out test, intervalDistance);
		if (test.transform != null && !test.transform.gameObject.collider.isTrigger) {
			test.transform.gameObject.renderer.material = testMaterial;
		}
		Physics.Raycast (transform.position, top, out test, intervalDistance);
		if (test.transform != null && !test.transform.gameObject.collider.isTrigger) {
			test.transform.gameObject.renderer.material = testMaterial;
		}
//		Physics.Raycast (transform.position, lft, out test, intervalDistance);
//		if (test.transform != null && !test.transform.gameObject.collider.isTrigger) {
//			test.transform.gameObject.renderer.material = testMaterial;
//		}
//		Physics.Raycast (transform.position, rght, out test, intervalDistance);
//		if (test.transform != null && !test.transform.gameObject.collider.isTrigger) {
//			test.transform.gameObject.renderer.material = testMaterial;
//		}
//		Physics.Raycast (transform.position, bck, out test, intervalDistance);
//		if (test.transform != null && !test.transform.gameObject.collider.isTrigger) {
//			test.transform.gameObject.renderer.material = testMaterial;
//		}
//		Debug.Log ("test1" + test.transform.gameObject);
	}

	//DELETE EVENTUALLY
	//Used to test game on computers
	private void compMotion () {
		if (!motionLocked && !isPaused && !isDone) {
			//Moving forward
			if (Input.GetKeyDown (KeyCode.UpArrow)&& !obstacleInFront || Input.GetKeyDown (KeyCode.W)&& !obstacleInFront) {
				motionVal = (int) Motion.FORWARD;
				motionLocked = true;
//				changeFloor ();
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

	public static int getLevel(int experience){
		return (int)Math.Floor (25 + Math.Sqrt (625 + 100 * experience)) / 50;
	}
	public static int getExperience(int steps){
		return (steps / 2);
	}
	public void finish() {
		if (PlayerPrefs.GetInt ("CurrentMaze") > PlayerPrefs.GetInt ("LargestMaze")) {
			PlayerPrefs.SetInt ("LargestMaze", PlayerPrefs.GetInt ("CurrentMaze"));
		}

		int totalExp = PlayerPrefs.GetInt("Experience") + getExperience(stepsForward);
		PlayerPrefs.SetInt("Experience", totalExp);
		PlayerPrefs.SetInt("Level", getLevel(totalExp));

		PlayerPrefs.Save ();

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

	private void updateCurrentMazeStats () {
		PlayerPrefs.SetInt ("Steps", stepsForward);
		PlayerPrefs.SetInt ("Turns", turns);

		PlayerPrefs.SetInt ("TotalSteps", oldTotalSteps + stepsForward);
		PlayerPrefs.SetInt ("TotalTurns", oldTotalTurns + turns);
		PlayerPrefs.SetFloat ("TotalTimeSpent", PlayerPrefs.GetFloat ("TotalTimeSpent") + Time.deltaTime);
	}
}
