using UnityEngine;
using System.Collections;

public class UseTrigger : MonoBehaviour {
	
	public bool m_inUse;
	public bool m_playerInRange;
	public bool m_usable = true;
	public bool m_finished = false;
	
	public GameObject m_toNotify;
	public string m_message;
	
	void FireUse() {
		m_toNotify.SendMessage(m_message, gameObject);
	}

	void Update() {
		if(!m_inUse) {
			CheckForUse();
		}
	}
	
	public void SetInUse(bool val) {
		m_inUse = val;
	}
	
	void CheckForUse() {
		if(!m_playerInRange || !m_usable) {
			return;
		}
		
		if(Input.GetKeyDown(KeyCode.E)) {
			FireUse();
		}
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.tag == "Player") {
			m_playerInRange = true;
		}
	}
	
	void OnTriggerExit(Collider c) {
		if(c.tag == "Player") {
			m_playerInRange = false;
		}
	}
}
