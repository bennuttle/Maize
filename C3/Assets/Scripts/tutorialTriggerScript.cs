using UnityEngine;
using System.Collections;

public class tutorialTriggerScript : MonoBehaviour {

	public bool boxOne;
	public string text;
	public bool isStart;

	// Use this for initialization
	void Start () {
//		boxOne = true;
		if (isStart) {
			boxOne = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter () {
		boxOne = true;
	}
	void OnGUI() {
		if (boxOne) {
		GUI.Box(new Rect(Screen.width * 0.2f, Screen.height * 0.5f, Screen.width * 0.6f, Screen.height * 0.05f),text);
		}
	}
	void OnTriggerStay () {
		boxOne = true;
	}

	void OnTriggerExit () {
		boxOne = false;
	}
}
