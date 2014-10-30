using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour {

	//public GUIText text;

	void onTriggerEnter(Collision other){
		GetComponent<GUIText>().enabled = true;
		Debug.Log ("Enter");
		
	}
	
	void onTriggerStay(Collision other){
		Debug.Log ("Start2");
		GetComponent<GUIText>().enabled = true;
		Debug.Log ("Stay");
	}

	void onTriggerExit(Collision other){
		GetComponent<GUIText> ().enabled = false;
		Debug.Log ("Exit");
	}


	// Use this for initialization
	void Start () {
		Debug.Log ("Starting");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
