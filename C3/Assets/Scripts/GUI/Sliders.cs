using UnityEngine;
using System.Collections;

public class Sliders : MonoBehaviour {
	public GameObject mazeGenerate;
	private float diffSlider;
	public Texture2D rText;
	public Texture2D bText;
	public Texture2D yText;
	private float heightConst = Screen.height * .1f;
	// Use this for initialization
	void Start () {
		diffSlider = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width * 0.25f, Screen.height * 0.2f, Screen.width * 0.55f, heightConst), "<color=#df4343><size=40>Difficulty: " + diffSlider +"</size></color>");

		GUI.skin.horizontalSlider.fixedHeight = heightConst;
		GUI.skin.horizontalSliderThumb.fixedHeight = heightConst;
		GUI.skin.horizontalSliderThumb.fixedWidth = heightConst;
		GUI.skin.horizontalSlider.normal.background = bText;
		GUI.skin.horizontalSliderThumb.normal.background = yText;
		GUI.skin.horizontalSliderThumb.hover.background = yText;
		GUI.skin.horizontalSliderThumb.active.background = yText;

		diffSlider = GUI.HorizontalSlider(new Rect(Screen.width*0.1f,Screen.height*0.4f, Screen.width*0.8f, heightConst), diffSlider, 1f, 10f);
		diffSlider = (int)diffSlider;


		GUI.skin.button.normal.background = rText;
		GUI.skin.button.hover.background = rText;
		GUI.skin.button.active.background = rText;
		if(GUI.Button(new Rect(Screen.width*0.2f,Screen.height*0.625f, Screen.width*0.6f, Screen.height*0.15f), "<size=40>Play!</size>")) {
			this.gameObject.SetActive(false);


			Instantiation yolo = (Instantiation)mazeGenerate.GetComponent("Instantiation");
//			yolo.setX((int)diffSlider);
//			yolo.setY((int)diffSlider);
//			yolo.setZ((int)diffSlider);
//			mazeGenerate.SetActive(true);
//			yolo.createMaze((int)diffSlider,(int)diffSlider,(int)diffSlider);
			yolo.createMaze((int)diffSlider);
		}
	}
}
