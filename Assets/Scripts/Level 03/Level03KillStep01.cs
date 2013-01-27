using UnityEngine;
using System.Collections;

public class Level03KillStep01 : MonoBehaviour, MissionStep {
	
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Gut you some fish");
	}
	
	public void CheckRequirements() {
		if(m_complete) {
			return;
		}
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		if(level.m_fish.Count <= 0) {
			OnCompletion();
		}
	}
	
	public void OnCompletion() {
		m_complete = true;
		m_tracker.NextStep();
	}
}
