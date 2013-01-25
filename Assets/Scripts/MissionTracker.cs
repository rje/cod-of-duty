using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface MissionStep {
	void OnStart();
	void CheckRequirements();
	void OnCompletion();
}

public class MissionTracker : MonoBehaviour {
	public List<GameObject> m_steps;
	public int m_currentIdx;
	
	void Start() {
		m_currentIdx = -1;
	}
	
	public void StartTracking() {
		if(m_currentIdx == -1) {
			SetStep (0);
		}
	}
	
	void SetStep(int idx) {
		m_currentIdx = idx;
		if(idx < m_steps.Count) {
			m_steps[idx].SendMessage("OnStart");
		}
	}
	
	public void NextStep() {
		if(m_currentIdx + 1 < m_steps.Count) {
			SetStep (m_currentIdx + 1);
		}
	}
	
	void FixedUpdate() {
		if(m_currentIdx != -1 && m_currentIdx < m_steps.Count) {
			m_steps[m_currentIdx].SendMessage("CheckRequirements");
		}
		if(m_currentIdx != -1 && m_currentIdx < m_steps.Count - 1) {
			CheckForUnexpectedDeath();
		}
	}
	
	void CheckForUnexpectedDeath() {
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if(player.m_isDead) {
			StartCoroutine(ReloadLevel());
		}
	}
	
	IEnumerator ReloadLevel() {
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel (Application.loadedLevelName);
	}
}
