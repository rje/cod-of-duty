using UnityEngine;
using System.Collections;

public class Cod : MonoBehaviour {
	
	public GameObject m_child;
	public float m_minSpeed;
	public float m_maxSpeed;
	
	public Barrel m_barrel;
	
	public ParticleEmitter[] m_emitters;
	
	float m_speed;
	float m_angle;

	// Use this for initialization
	void Start () {
		m_child.transform.localPosition = new Vector3(Random.Range (0.1f, 0.3f), 0, 0);
		m_speed = Random.Range (m_minSpeed, m_maxSpeed);
		m_angle = Random.Range (0.0f, 360.0f);
		transform.localRotation = Quaternion.Euler(0, m_angle, 0);
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.AddFish(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		m_angle += m_speed;
		if(m_angle > 360.0f) {
			m_angle -= 360.0f;
		}
		transform.localRotation = Quaternion.Euler(0, m_angle, 0);
	}
	
	void OnShotBy(ShotInfo si) {
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.RemoveFish(gameObject);
		foreach(var emitter in m_emitters) {
			emitter.Emit (Random.Range (30, 60));
			emitter.transform.parent = null;
			GameObject.Destroy (emitter.gameObject, 2.5f);
		}
		m_barrel.RemoveFish(gameObject);
		
		GenerateLoot();
		
		Destroy (gameObject);
	}
	
	void GenerateLoot() {
		var toSpawn = LootGenerator.GetLoot();
		if(toSpawn != null) {
			var spawned = (GameObject)GameObject.Instantiate(toSpawn);
			spawned.transform.position = transform.position + new Vector3(Random.Range (-2, 2), 0, Random.Range (-2, 2));
			spawned.GetComponent<Loot>().ApplyForce();
		}
	}
}
