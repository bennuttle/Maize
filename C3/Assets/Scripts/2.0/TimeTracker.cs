using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTracker : MonoBehaviour {
	float milliseconds = 0;
	int seconds = 0;
	int minutes = 0;
	string displayTime;
	PlayerMotion timeRefScript;
	GameObject time;
	GameObject timeRef;
	// Use this for initialization
	void Start () {
		time = this.gameObject;
		timeRef = GameObject.FindGameObjectWithTag("MainCamera");
		timeRefScript = (PlayerMotion) timeRef.GetComponent(typeof(PlayerMotion));
	}
	
	// Update is called once per frame
	void Update () {
		if(!timeRefScript.getIsDone() && !timeRefScript.getIsPaused()) {
			milliseconds += Time.deltaTime;
			if (milliseconds > 1)
			{
				seconds++;
				milliseconds = 0;
			}
			if (seconds > 59)
			{
				minutes++;
				seconds = 0;
			}
			time.GetComponent<Text> ().text = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString (".00").Replace (".", "");
		}
	}
}
