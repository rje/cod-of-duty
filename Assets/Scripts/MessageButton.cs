using UnityEngine;
using System.Collections;

public class MessageButton : MonoBehaviour {

	public GameObject m_toNotify;
	public string m_message;
	public AudioClip m_click;
	
	void OnMouseDown() {
		AudioSource.PlayClipAtPoint(m_click, Vector3.zero);
		m_toNotify.SendMessage(m_message, SendMessageOptions.RequireReceiver);
	}
}
