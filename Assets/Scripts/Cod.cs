using UnityEngine;
using System.Collections;

public class Cod : MonoBehaviour {
	
	public GameObject m_child;
	public float m_minSpeed;
	public float m_maxSpeed;
	
	public ParticleEmitter[] m_emitters;
	
	float m_speed;
	float m_angle;

	// Use this for initialization
	void Start () {
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.AddFish(gameObject);
		m_child.transform.localPosition = new Vector3(Random.Range (0.1f, 0.3f), 0, 0);
		m_speed = Random.Range (m_minSpeed, m_maxSpeed);
		m_angle = Random.Range (0.0f, 360.0f);
		transform.localRotation = Quaternion.Euler(0, m_angle, 0);
	}
	
	// Update is called once per frame
	void Update () {
		m_angle += m_speed;
		if(m_angle > 360.0f) {
			m_angle -= 360.0f;
		}
		transform.localRotation = Quaternion.Euler(0, m_angle, 0);
	}
	
	void OnShotBy(Player p) {
		var level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInit>();
		level.RemoveFish(gameObject);
		foreach(var emitter in m_emitters) {
			emitter.Emit (Random.Range (30, 60));
			emitter.transform.parent = null;
			Destroy (emitter, 2.5f);
		}
		Destroy (gameObject);
	}
}
