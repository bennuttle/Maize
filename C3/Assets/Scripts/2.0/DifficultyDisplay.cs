using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyDisplay : MonoBehaviour {
	Text text;
	public Slider difficultySlider;
	public void updateDifficultyNumber() {
		text = this.gameObject.GetComponent<Text>();
		text.text = difficultySlider.value.ToString();
	}
}
