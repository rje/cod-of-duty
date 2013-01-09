using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInit : MonoBehaviour {
	
	public GameObject m_barrelPrefab;
	public int m_numBarrels;
	
	public List<Barrel> m_barrels;
	public List<GameObject> m_fish;
	
	public HUD m_hud;
	
	float m_time;
	
	void Start() {
		m_time = 0.0f;
		m_barrels = new List<Barrel>();
		m_fish = new List<GameObject>();
		for(var i = 0; i < m_numBarrels; i++) {
			var go = (GameObject)GameObject.Instantiate(m_barrelPrefab);
			go.transform.position = new Vector3(Random.Range (-30, 30), 0, Random.Range (-30, 30));
			m_barrels.Add (go.GetComponent<Barrel>());
		}
	}
	
	public void AddFish(GameObject fish) {
		m_fish.Add (fish);
		m_hud.SetRemainingCount(m_fish.Count);
	}
	
	public void RemoveFish(GameObject fish) {
		m_fish.Remove(fish);
		m_hud.SetRemainingCount(m_fish.Count);
	}
	
	void Update() {
		m_time += Time.deltaTime;
		m_hud.SetTimeLabel(m_time);
	}
}
