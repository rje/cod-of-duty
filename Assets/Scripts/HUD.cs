using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GUIText m_remainingLabel;
	public GUIText m_levelLabel;
	public GUIText m_timeLabel;
	
	public void SetRemainingCount(int amount) {
		m_remainingLabel.text = string.Format ("Remaining: {0}", amount);
	}
	
	public void SetTimeLabel(float sinceStart) {
		m_timeLabel.text = string.Format ("Time: {0:0}", sinceStart);
	}
}
