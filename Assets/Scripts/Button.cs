using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown() {
		Application.LoadLevel("test");
	}
}
