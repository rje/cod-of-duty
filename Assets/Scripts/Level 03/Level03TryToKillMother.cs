using UnityEngine;
using System.Collections;

public class Level03TryToKillMother : MonoBehaviour, MissionStep {
	
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	public StoryHUD m_storyHud;
	public string m_storyText;
	
	public LevelInit m_level;
	public MotherCod m_motherCod;
	
	public int m_healthThreshold;
	
	bool m_doneWithStory = false;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Turn this mother out");
		m_storyHud.m_storyText.text = m_storyText;
		m_storyHud.m_toNotifyOnDismissal = gameObject;
		m_storyHud.m_method = "DoneWithStory";
		m_storyHud.Show();
	}
	
	void DoneWithStory() {
		m_doneWithStory = true;
	}
	
	public void CheckRequirements() {
		if(m_complete || !m_doneWithStory) {
			return;
		}
		if(m_motherCod.m_hp <= m_healthThreshold) {
			OnCompletion();
		}
	}
	
	public void OnCompletion() {
		m_complete = true;
		m_tracker.NextStep();
	}
}
