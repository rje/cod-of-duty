using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	public GameObject m_contents;
	public Player m_player;
	public GameObject m_storyHUD;
	public TextMesh m_invertYButton;
	
	void Update() {
		if(Input.GetButtonDown("Pause")) {
			if(m_storyHUD.activeSelf) {
				// A story element is up, don't allow pausing
				return;
			}
			
			if(m_contents.activeSelf) {
				UnPause();
			}
			else {
				Pause();
			}
		}
	}
	
	void Quit() {
		Time.timeScale = 1;
		Application.LoadLevel("main-menu");
	}
	
	void Pause() {
		Init.ShowCursor();
		m_player.m_pauseInput = true;
		UpdateInvertText();
		m_contents.SetActive(true);
		Time.timeScale = 0;
	}
	
	void UnPause() {
		Time.timeScale = 1;
		Init.HideCursor();
		m_contents.SetActive(false);
		m_player.UnpauseAfterDelay(0.1f);
	}
	
	void ToggleYAxis() {
		Debug.Log ("y axis toggle");
		var currentValue = PlayerPrefs.GetInt("y-axis-invert");
		currentValue = currentValue == 1 ? -1 : 1;
		PlayerPrefs.SetInt ("y-axis-invert", currentValue);
		PlayerPrefs.Save();
		UpdateInvertText();
	}
	
	void UpdateInvertText() {
		var currentValue = PlayerPrefs.GetInt("y-axis-invert");
		if(currentValue == -1) {
			m_invertYButton.text = "Yes";
		}
		else {
			m_invertYButton.text = "No";
		}
	}
}
