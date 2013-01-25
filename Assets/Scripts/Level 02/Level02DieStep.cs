using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level02DieStep : MonoBehaviour, MissionStep {
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	
	public List<GameObject> m_deathSquad;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Turn around");
		StartCoroutine(SpawnDeathSquad());
	}
	
	IEnumerator SpawnDeathSquad() {
		yield return new WaitForSeconds(0.66f);
		foreach(var go in m_deathSquad) {
			go.SetActive(true);
		}
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
		StartCoroutine(LoadLevelAfterDelay(2.0f, "level 2"));
	}
	
	IEnumerator LoadLevelAfterDelay(float delay, string level) {
		yield return new WaitForSeconds(delay);
		Application.LoadLevel(level);
	}
}
