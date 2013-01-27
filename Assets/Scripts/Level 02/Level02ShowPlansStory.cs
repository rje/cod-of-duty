using UnityEngine;
using System.Collections;

public class Level02ShowPlansStory: MonoBehaviour {
	
	public string m_storyText;
	public StoryHUD m_hud;
	public UseTrigger laptop;

	public void ShowStory(GameObject caller) {
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.m_pauseInput = true;
		laptop = caller.GetComponent<UseTrigger>();
		laptop.m_usable = false;
		m_hud.m_storyText.text = m_storyText;
		m_hud.m_toNotifyOnDismissal = gameObject;
		m_hud.m_method = "OnStoryClose";
		m_hud.Show();
	}
	
	void OnStoryClose() {
		laptop.m_finished = true;
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.UnpauseAfterDelay(0.1f);
	}
}
