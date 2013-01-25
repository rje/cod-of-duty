using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GUIText m_remainingLabel;
	public GUIText m_levelLabel;
	public GUIText m_timeLabel;
	public GUIText m_ammoLabel;
	public GUIText m_objectiveLabel;
	
	public void SetRemainingCount(int amount) {
		m_remainingLabel.text = string.Format ("Remaining: {0}", amount);
	}
	
	public void SetTimeLabel(float sinceStart) {
		m_timeLabel.text = string.Format ("Time: {0:0}", sinceStart);
	}
	
	public void SetAmmoLabel(int count, int remaining) {
		if(remaining == -1) {
			m_ammoLabel.text = string.Format ("Ammo: {0}/inf", count);
		}
		else {
			m_ammoLabel.text = string.Format ("Ammo: {0}/{1}", count, remaining);
		}
	}
	
	public void SetObjectiveLabel(string objective) {
		m_objectiveLabel.text = string.Format ("Objective:\n{0}", objective);
	}
}
