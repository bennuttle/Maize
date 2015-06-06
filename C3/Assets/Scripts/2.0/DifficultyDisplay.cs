using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyDisplay : MonoBehaviour {
	Text text;
	public Slider difficultySlider;
	public void updateDifficultyNumber() {
		GameObject[] difficultyLevels = GameObject.FindGameObjectsWithTag ("DifficultyLevel");
		foreach( GameObject difficultyLevel in difficultyLevels) {
			text = difficultyLevel.gameObject.GetComponent<Text>();
			text.text = difficultySlider.value.ToString();
		}
	}
}
