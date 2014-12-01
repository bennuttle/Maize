using UnityEngine;
using System.Collections;

public class tutorialTriggerScript : MonoBehaviour {

	public bool boxOne;
	public string text;
	public bool isStart;
	public GUISkin tutSkin;

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

	void OnTriggzerEnter () {
		boxOne = true;
	}
	void OnGUI() {
		GUI.skin = tutSkin;
		if (boxOne) {
			GUI.skin.box.fontSize = 60;
			GUI.Box(new Rect(Screen.width * 0.1f, Screen.height * 0.25f, Screen.width * 0.9f, Screen.height * 0.5f),text);
		}
	}
	void OnTriggerStay () {
		boxOne = true;
	}

	void OnTriggerExit () {
		boxOne = false;
	}
}
