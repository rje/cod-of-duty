using UnityEngine;
using System.Collections;

public class Level01ShowReportStory : MonoBehaviour {
	
	public string m_storyText;
	public StoryHUD m_hud;
	
	UseTrigger laptop;
	
	void Start() {
		m_hud = GameObject.FindGameObjectWithTag("storyhud").GetComponent<StoryHUD>();
	}

	public void ShowReportStory(GameObject caller) {
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
