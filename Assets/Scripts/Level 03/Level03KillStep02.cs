using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level03KillStep02 : MonoBehaviour {
	
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	public StoryHUD m_storyHud;
	public string m_storyText;
	
	public LevelInit m_level;
	public List<DeathCod> m_deathCod;
	public SpawnArea[] m_spawns;
	public int m_spawnCount;
	
	bool m_doneWithStory = false;
	
	
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Gut more fish");
		m_storyHud.m_storyText.text = m_storyText;
		m_storyHud.m_toNotifyOnDismissal = gameObject;
		m_storyHud.m_method = "DoneWithStory";
		m_storyHud.Show();
	}
	
	void DoneWithStory() {
		SpawnDeathCod();
		SpawnBarrels();
		m_doneWithStory = true;
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.UnpauseAfterDelay(0.1f);
	}
	
	void SpawnDeathCod() {
		foreach(var cod in m_deathCod) {
			cod.gameObject.SetActive(true);
			cod.AddToLevel();
		}
	}
	
	void SpawnBarrels() {
		m_level.SpawnCountInAreas(m_spawnCount, m_spawns);
	}
	
	public void CheckRequirements() {
		if(m_complete || !m_doneWithStory) {
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
