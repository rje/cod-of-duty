using UnityEngine;
using System.Collections;

public class DeathCod : MonoBehaviour {
	
	public float m_fireRange;
	public float m_followRange;
	public float m_speed;
	public Gun m_gun;
	public int m_hp;
	
	public bool m_stopAttacking;
	
	public Transform m_raycastPoint;
	
	bool m_dead = false;
	
	void Start() {	
		AddToLevel();
	}
	
	public void AddToLevel() {
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.AddFish(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(m_stopAttacking) {
			return;
		}
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		var ray = new Ray(m_raycastPoint.position, (player.transform.position + player.transform.up) - m_raycastPoint.position);
		Debug.DrawRay(m_raycastPoint.position, (player.transform.position + player.transform.up) - m_raycastPoint.position);
		var distance = (player.transform.position - transform.position).magnitude;
		if(distance > m_followRange || !CanSeePlayer(player.gameObject, ray)) {
			return;
		}
		
		if(distance <= m_fireRange) {
			FacePlayer(player);
			TryToFire();
		}
		else if(distance <= m_followRange) {
			FacePlayer(player);
			MoveForward();
		}
	}
	
	bool CanSeePlayer(GameObject player, Ray ray) {
		RaycastHit rh;
		if(Physics.Raycast(ray, out rh, float.MaxValue)) {
			if(rh.collider.gameObject == player) {
				return true;
			}
		}
		return false;
	}
	
	void TryToFire() {
		var offset = Random.Range (-0.1f, 0.1f) * m_gun.m_muzzleExit.transform.right +
					 Random.Range (-0.1f, 0.1f) * m_gun.m_muzzleExit.transform.up;
		var direction = offset + m_gun.m_muzzleExit.transform.forward;
		direction.Normalize();
		var ray = new Ray(m_gun.m_muzzleExit.transform.position, direction);
		m_gun.TryToFire(ray);
	}
	
	void OnShotBy(ShotInfo si) {
		m_hp -= si.m_damage;
		if(m_hp <= 0 && !m_dead) {
			Die();
		}
	}
	
	void FacePlayer(Player p) {
		var toLookAt = p.transform.position;
		toLookAt.y -= 0.33f;
		transform.LookAt(toLookAt);
		transform.Rotate(0, 180, 0);
	}
	
	void Die() {
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.RemoveFish(gameObject);
		Destroy(gameObject);
	}
	
	void MoveForward() {
		transform.position -= transform.forward * m_speed * Time.deltaTime;
	}
}
