using UnityEngine;
using System.Collections;

public class FadeOnPlayerDistance : MonoBehaviour {
	
	public float m_maxDistance;

	void FixedUpdate() {
		var player = GameObject.FindGameObjectWithTag("Player");
		var dist = (player.transform.position - transform.position).magnitude;
		if(dist > m_maxDistance) {
			renderer.material.color = Color.clear;
		}
		else {
			var alpha = 1.0f - dist / m_maxDistance;
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
		}
	}
}
