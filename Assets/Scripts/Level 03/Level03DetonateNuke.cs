using UnityEngine;
using System.Collections;

public class Level03DetonateNuke : MonoBehaviour, MissionStep {
	
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	public StoryHUD m_storyHud;
	public string m_storyText;
	public UseTrigger m_laptop;
	
	bool m_doneWithStory = false;
	
	public void OnStart() {
		StopMotherAttacking(true);
		StopDeathCodAttacking(true);
		m_hud.SetObjectiveLabel("Become the bait");
		m_storyHud.m_storyText.text = m_storyText;
		m_storyHud.m_toNotifyOnDismissal = gameObject;
		m_storyHud.m_method = "DoneWithStory";
		m_storyHud.Show();
	}
	
	void StopMotherAttacking(bool val) {
		var mother = (MotherCod)GameObject.FindObjectOfType(typeof(MotherCod));
		mother.m_stopAttacking = val;
	}
	
	void StopDeathCodAttacking(bool val) {
		var deathCods = (DeathCod[])GameObject.FindObjectsOfType(typeof(DeathCod));
		foreach(var cod in deathCods) {
			cod.m_stopAttacking = val;
		}
	}
	
	void DoneWithStory() {
		m_laptop.m_usable = true;
		m_doneWithStory = true;
		StopMotherAttacking(false);
		StopDeathCodAttacking(false);
	}
	
	public void CheckRequirements() {
		if(m_complete || !m_doneWithStory) {
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
