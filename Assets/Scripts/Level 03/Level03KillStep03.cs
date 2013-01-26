using UnityEngine;
using System.Collections;

public class Level03KillStep03 : MonoBehaviour, MissionStep {
	
	bool m_complete = false;
	public MissionTracker m_tracker;
	public HUD m_hud;
	public StoryHUD m_storyHud;
	public string m_storyText;
	
	public LevelInit m_level;
	public GameObject m_motherCod;
	public string m_motherCodAnim;
	public string m_motherCodLoopAnim;
	
	bool m_doneWithStory = false;
	
	public void OnStart() {
		m_hud.SetObjectiveLabel("Catch a fish thiiiiis big");
		m_storyHud.m_storyText.text = m_storyText;
		m_storyHud.m_toNotifyOnDismissal = gameObject;
		m_storyHud.m_method = "DoneWithStory";
		m_storyHud.Show();
	}
	
	IEnumerator DoneWithStory() {
		m_motherCod.SetActive(true);
		m_motherCod.animation.Play (m_motherCodAnim);
		var player = GameObject.FindGameObjectWithTag("Player");
		var time = 0.0f;
		var rotation = player.transform.rotation;
		while(time < 10.0f) {
			time += Time.deltaTime;
			player.transform.LookAt(m_motherCod.transform.position);
			yield return null;
		}
		player.transform.rotation = rotation;
		m_motherCod.animation.Play (m_motherCodLoopAnim);
		m_doneWithStory = true;
	}
	
	public void CheckRequirements() {
		if(m_complete || !m_doneWithStory) {
			return;
		}
		OnCompletion();
	}
	
	public void OnCompletion() {
		m_complete = true;
		m_tracker.NextStep();
	}
}
