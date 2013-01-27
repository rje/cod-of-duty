using UnityEngine;
using System.Collections;

public class MotherCod : MonoBehaviour {
	
	public int m_maxHP;
	public int m_hp;
	
	public GameObject m_deathCodPrefab;
	public Transform m_spawnPoint;
	public float m_spawnTimeThreshold;
	public int m_spawnHpThreshold;
	public bool m_stopAttacking;
	
	public ParticleEmitter m_blood;
	
	float m_timeSinceSpawn;
	
	void Start() {
		m_hp = m_maxHP;
	}

	void OnShotBy(ShotInfo si) {
		m_hp -= si.m_damage;
		m_blood.transform.position = si.m_location;
		m_blood.Emit (30);
	}
	
	void FixedUpdate() {
		m_timeSinceSpawn += Time.fixedDeltaTime;
		if(m_hp <= m_spawnHpThreshold && m_timeSinceSpawn >= m_spawnTimeThreshold) {
			SpawnDeathCod();
			m_timeSinceSpawn = 0;
		}
	}
	
	void SpawnDeathCod() {
		if(m_stopAttacking) {
			return;
		}
		var codGO = (GameObject)GameObject.Instantiate(m_deathCodPrefab);
		codGO.transform.position = m_spawnPoint.position;
		codGO.transform.rotation = m_spawnPoint.rotation;
	}
}
