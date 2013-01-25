using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour {
	
	public bool m_flip;

	void LateUpdate() {
		var player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt(player.transform.position);
		var euler = transform.rotation.eulerAngles;
		euler.x = 0;
		euler.z = 0;
		transform.rotation = Quaternion.Euler(euler);
		if(m_flip) {
			transform.Rotate(transform.up, 180.0f);
		}
	}
}
