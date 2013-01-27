using UnityEngine;
using System.Collections;

public class StoryHUD : MonoBehaviour {

	public TextMesh m_title;
	public TextMesh m_storyText;
	public TextMesh m_button;
	
	public GameObject m_toNotifyOnDismissal;
	public string m_method;
	
	public string m_inAnim;
	public string m_outAnim;
	
	void Start() {
		Show ();
	}
	
	void Dismiss() {
		StartCoroutine(DoDismissal());
	}
	
	IEnumerator DoDismissal() {
		animation.Play (m_outAnim);
		yield return new WaitForSeconds(0.33f);
		Init.HideCursor();
		gameObject.SetActive(false);
		GetPlayer ().m_pauseInput = false;
		StartTrackerIfNecessary();
		if(m_toNotifyOnDismissal != null) {
			m_toNotifyOnDismissal.SendMessage(m_method);
		}
	}
	
	public void Show() {
		Init.ShowCursor();
		gameObject.SetActive(true);
		animation.Play (m_inAnim);
		GetPlayer ().m_pauseInput = true;
	}
	
	Player GetPlayer() {
		var playerGO = GameObject.FindGameObjectWithTag("Player");
		return playerGO.GetComponent<Player>();
	}
	
	void StartTrackerIfNecessary() {
		var tracker = GameObject.FindGameObjectWithTag("tracker").GetComponent<MissionTracker>();
		tracker.StartTracking();
	}
}
