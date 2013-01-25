using UnityEngine;
using System.Collections;

public class Level2OpenDoors : MonoBehaviour, MissionStep {
	
	bool m_complete = false;
	public MissionTracker m_tracker;
	public GameObject m_leftDoor;
	public GameObject m_rightDoor;
	public float newZ;
	
	public void OnStart() {
		var pos = m_leftDoor.transform.position;
		pos.z = -newZ;
		m_leftDoor.transform.position = pos;
		
		pos = m_rightDoor.transform.position;
		pos.z = newZ;
		m_rightDoor.transform.position = pos;
	}
	
	public void CheckRequirements() {
		if(m_complete) {
			return;
		}
		OnCompletion();
	}
	
	public void OnCompletion() {
		m_complete = true;
		m_tracker.NextStep();
	}
}
