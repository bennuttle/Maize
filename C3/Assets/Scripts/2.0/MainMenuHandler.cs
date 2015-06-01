using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour {

	public void SelectButton(int button) {
		Application.LoadLevel (button);
	}
}
