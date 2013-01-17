using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	
	public float m_force;
	public float m_timer;
	public float m_explosionRadius;
	
	public GameObject m_explosionPrefab;
	public Player m_player;
	
	float m_time = 0;
	
	void Start() {
		rigidbody.AddForce(transform.forward * m_force);
	}
	
	void Update() {
		//transform.position += transform.forward * m_speed * Time.deltaTime;
		m_time += Time.deltaTime;
		if(m_time >= m_timer) {
			Explode();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		Explode();
	}
	
	void Explode() {
		var explosion = (GameObject)GameObject.Instantiate(m_explosionPrefab);
		explosion.transform.position = transform.position;
		DestroyBarrelsInArea(transform.position);
		var emitter = explosion.GetComponentInChildren<ParticleEmitter>();
		emitter.Emit (300);
		Destroy (gameObject);
	}
	
	void DestroyBarrelsInArea(Vector3 pos) {
		var rangeSq = m_explosionRadius * m_explosionRadius;
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		var barrels = level.m_barrels;
		for(var i = barrels.Count - 1; i >= 0; i--) {
			var barrel = level.m_barrels[i];
			var barrelDistSq = (barrel.transform.position - pos).sqrMagnitude;
			if(barrelDistSq <= rangeSq) {
				barrel.Explode(m_player);
				barrels.Remove(barrel);
			}
		}
	}
}
