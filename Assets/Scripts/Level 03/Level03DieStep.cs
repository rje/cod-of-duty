using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level03DieStep : MonoBehaviour, MissionStep {
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Be Vaporized");
	}
	
	public void CheckRequirements() {
		if(m_complete) {
			return;
		}
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if(player.m_isDead) {
			OnCompletion();
		}
	}
	
	public void OnCompletion() {
		m_complete = true;
		StartCoroutine(LoadLevelAfterDelay(6.0f, "game over"));
	}
	
	IEnumerator LoadLevelAfterDelay(float delay, string level) {
		yield return new WaitForSeconds(delay);
		Application.LoadLevel(level);
	}
}
