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
		
		var colliders = Physics.OverlapSphere(transform.position, m_explosionRadius);
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		var barrels = level.m_barrels;
		
		foreach(var collider in colliders) {
			var rootGO = collider.gameObject;
			while(rootGO.transform.parent != null) {
				rootGO = rootGO.transform.parent.gameObject;
			}
			var barrel = rootGO.GetComponent<Barrel>();
			if(barrel != null) {
				barrel.Explode(m_player.gameObject);
				barrels.Remove(barrel);
			}
			var deathCod = rootGO.GetComponent<DeathCod>();
			if(deathCod != null) {
				rootGO.GetComponent<Shootable>().ShotBy(m_player.gameObject, 5);
			}
		}
		var emitter = explosion.GetComponentInChildren<ParticleEmitter>();
		emitter.Emit (300);
		Destroy (gameObject);
	}
}
