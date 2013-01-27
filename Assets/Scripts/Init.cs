using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {
	
	static bool m_showing = true;
	
	public static void ShowCursor() {
		Screen.showCursor = true;
		Screen.lockCursor = false;
		m_showing = true;
	}
	
	public static void HideCursor() {
		Screen.showCursor = false;
		Screen.lockCursor = true;
		m_showing = false;
	}
	
	public static void RecaptureCursor() {
		if(m_showing) {
			ShowCursor();
		}
		else {
			HideCursor();
		}
	}
}
