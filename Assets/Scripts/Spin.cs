using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float m_speed;
	public float m_angle;
	
	// Update is called once per frame
	void Update () {
		m_angle += m_speed * Time.deltaTime;
		while(m_angle >= 360.0f) {
			m_angle -= 360.0f;
		}
		transform.rotation = Quaternion.Euler(0.0f, m_angle, 0.0f);
	}
}
