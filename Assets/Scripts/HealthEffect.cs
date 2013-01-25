using UnityEngine;
using System.Collections;

public class HealthEffect : MonoBehaviour {
	
	public float m_min;
	public float m_max;
	
	public GameObject m_left;
	public GameObject m_right;

	public void SetHealthPerc(float perc) {
		var x = m_min + (m_max - m_min) * perc;
		var pos = m_left.transform.localPosition;
		pos.x = -x;
		m_left.transform.localPosition = pos;
		
		pos = m_right.transform.localPosition;
		pos.x = x;
		m_right.transform.localPosition = pos;
	}
}
