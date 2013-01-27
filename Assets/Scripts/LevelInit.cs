using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInit : MonoBehaviour {
	
	public GameObject m_barrelPrefab;
	public int m_numBarrels;
	
	public List<Barrel> m_barrels;
	public List<GameObject> m_fish;
	
	public SpawnArea[] m_spawns;
	
	public StoryHUD m_storyHUD;
	
	public HUD m_hud;
	
	float m_time;
	
	void Start() {
		m_time = 0.0f;
		m_barrels = new List<Barrel>();
		m_fish = new List<GameObject>();
		SpawnCountInAreas(m_numBarrels, m_spawns);
		m_storyHUD.Show ();
	}
	
	void OnApplicationFocus(bool val) {
		Init.RecaptureCursor();
	}
	
	public void SpawnCountInAreas(int count, SpawnArea[] areas) {
		for(var i = 0; i < count; i++) {
			var go = (GameObject)GameObject.Instantiate(m_barrelPrefab);
			go.transform.position = GetSpawnPoint(areas) + new Vector3(0, 0.1f, 0);
			var barrel = go.GetComponent<Barrel>();
			m_barrels.Add (barrel);
		}
	}
	
	void FixedUpdate() {
		m_hud.SetRemainingCount(m_fish.Count);
	}
	
	Vector3 GetSpawnPoint(SpawnArea[] areas) {
		var randomSpawn = areas[Random.Range (0, areas.Length)];
		var pos = randomSpawn.GetRandomPoint();
		return pos;
	}
	
	public void AddFish(GameObject fish) {
		if(!m_fish.Contains(fish)) {
			m_fish.Add (fish);
		}
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
