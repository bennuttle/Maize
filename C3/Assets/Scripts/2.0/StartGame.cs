using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		this.gameObject.SetActive(false);
		GameObject difficultyLevel = GameObject.FindGameObjectWithTag ("DifficultyLevel");
		if (difficultyLevel == null) {
						PlayerPrefs.SetInt ("CurrentMaze", 1);
						PlayerPrefs.Save ();
						Instantiation yolo = (Instantiation)this.GetComponent ("Instantiation");
						//			yolo.setX((int)diffSlider);
						//			yolo.setY((int)diffSlider);
						//			yolo.setZ((int)diffSlider);
						//			mazeGenerate.SetActive(true);
						//			yolo.createMaze((int)diffSlider,(int)diffSlider,(int)diffSlider);
						yolo.createMaze (1);
				} else {
						int difficulty = int.Parse (difficultyLevel.GetComponent<Text> ().text);
						PlayerPrefs.SetInt ("CurrentMaze", (int)difficulty);
						PlayerPrefs.Save ();
						Instantiation yolo = (Instantiation)this.GetComponent ("Instantiation");
						//			yolo.setX((int)diffSlider);
						//			yolo.setY((int)diffSlider);
						//			yolo.setZ((int)diffSlider);
						//			mazeGenerate.SetActive(true);
						//			yolo.createMaze((int)diffSlider,(int)diffSlider,(int)diffSlider);
						yolo.createMaze ((int)difficulty);
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
