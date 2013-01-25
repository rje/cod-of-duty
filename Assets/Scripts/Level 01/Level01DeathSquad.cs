using UnityEngine;
using System.Collections;

public class Level01DeathSquad : MonoBehaviour {

	public float m_fireRange;
	public Gun m_gun;
	
	void Update() {
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		var distance = (player.transform.position - transform.position).magnitude;
		if(distance <= m_fireRange) {
			FacePlayer(player);
			TryToFire();
		}
	}
	
	void TryToFire() {
		var ray = new Ray(m_gun.m_muzzleExit.transform.position, m_gun.m_muzzleExit.transform.forward);
		m_gun.TryToFire(ray);
	}
	
	void FacePlayer(Player p) {
		var toLookAt = p.transform.position;
		toLookAt.y -= 0.33f;
		transform.LookAt(toLookAt);
		transform.Rotate(0, 180, 0);
	}
}
