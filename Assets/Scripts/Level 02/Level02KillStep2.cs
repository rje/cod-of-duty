using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level02KillStep2 : MonoBehaviour {
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	
	public List<GameObject> m_fishToActivate;
	public SpawnArea[] m_spawns;
	public int m_spawnCount;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Cast the net");
		foreach(var fish in m_fishToActivate) {
			fish.SetActive(true);
			fish.GetComponent<DeathCod>().AddToLevel();
		}
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.SpawnCountInAreas(m_spawnCount, m_spawns);
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
