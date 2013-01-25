using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {
	
	public static void ShowCursor() {
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}
	
	public static void HideCursor() {
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}
}
