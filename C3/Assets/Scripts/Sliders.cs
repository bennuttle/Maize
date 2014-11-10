using UnityEngine;
using System.Collections;

public class Sliders : MonoBehaviour {
	public GameObject mazeGenerate;
	private float xSlider;
	private float ySlider;
	private float zSlider;
//	public GUISkin customSkin;
	// Use this for initialization
	void Start () {
		xSlider = 2;
		ySlider = 2; 
		zSlider = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
//		GUI.skin = customSkin;

		GUI.Label (new Rect (Screen.width * 0.45f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.1f), "<color=#df4343><size=40>X size: " + xSlider + "</size></color>");
		xSlider = GUI.HorizontalSlider(new Rect(Screen.width*0.3f,Screen.height*0.25f, Screen.width*0.4f, Screen.height*0.1f), xSlider, 2f, 6f);
		xSlider = (int)xSlider;

		GUI.Label (new Rect (Screen.width * 0.45f, Screen.height * 0.35f, Screen.width * 0.4f, Screen.height * 0.1f), "<color=#f4c16d><size=40>Y size: " + ySlider + "</size></color>");

		ySlider = GUI.HorizontalSlider(new Rect(Screen.width*0.3f,Screen.height*0.5f, Screen.width*0.4f, Screen.height*0.1f), ySlider, 2f, 6f);
		ySlider = (int)ySlider;


		GUI.Label (new Rect (Screen.width * 0.45f, Screen.height * 0.6f, Screen.width * 0.4f, Screen.height * 0.1f), "<color=#5181c2><size=40>Z size: " + zSlider + "</size></color>");

		zSlider = GUI.HorizontalSlider(new Rect(Screen.width*0.3f,Screen.height*0.75f, Screen.width*0.4f, Screen.height*0.1f), zSlider, 2f, 6f);
		zSlider = (int)zSlider;

		if(GUI.Button(new Rect(Screen.width*0.3f,Screen.height*0.8f, Screen.width*0.4f, Screen.height*0.175f), "<size=40>Play!</size>")) {
//			Instantiate(mazeGenerate);
			this.gameObject.SetActive(false);


			Instantiation yolo = (Instantiation)mazeGenerate.GetComponent("Instantiation");
			yolo.setX((int)xSlider);
			yolo.setY((int)ySlider);
			yolo.setZ((int)zSlider);
//			yield return WaitForSeconds(5);
			mazeGenerate.SetActive(true);
		}
	}
}
