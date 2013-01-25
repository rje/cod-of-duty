using UnityEngine;
using System.Collections;

public class Level02GetPlansStep : MonoBehaviour, MissionStep {
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	public UseTrigger m_laptop;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Get the plans off the computer");
		m_laptop.m_usable = true;
	}
	
	public void CheckRequirements() {
		if(m_complete) {
			return;
		}
		if(m_laptop.m_finished) {
			OnCompletion();
		}
	}
	
	public void OnCompletion() {
		m_complete = true;
		m_tracker.NextStep();
	}
}
