using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInit : MonoBehaviour {
	
	public GameObject m_barrelPrefab;
	public int m_numBarrels;
	
	public List<Barrel> m_barrels;
	public List<GameObject> m_fish;
	
	void Start() {
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
	}
	
	public void RemoveFish(GameObject fish) {
		m_fish.Remove(fish);
	}
	
	void OnGUI() {
		GUI.Label (new Rect(10, 10, 200, 50), "Cod remaining: " + m_fish.Count);
	}
}
