using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public AudioClip m_click;
	public string m_message;

	// Use this for initialization
	void OnMouseDown() {
		SendMessage(m_message);
	}
	
	void StartGame() {
		AudioSource.PlayClipAtPoint(m_click, Vector3.zero);
		Application.LoadLevel("level 1");
	}
	
	void QuitGame() {
		AudioSource.PlayClipAtPoint(m_click, Vector3.zero);
		Application.Quit ();
	}
	
	void MainMenu() {
		AudioSource.PlayClipAtPoint(m_click, Vector3.zero);
		Application.LoadLevel ("main-menu");
	}
}
