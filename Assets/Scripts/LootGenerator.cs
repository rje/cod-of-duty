using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootGenerator : MonoBehaviour {
	public List<GameObject> m_lootPrefabs;
	public float m_lootChance;
	public List<float> m_odds;
	
	static LootGenerator sm_instance;
	
	static LootGenerator Instance() {
		if(sm_instance == null) {
			sm_instance = GameObject.FindGameObjectWithTag("loot").GetComponent<LootGenerator>();
		}
		return sm_instance;
	}
	
	GameObject GetRandomPrefab() {
		if(Random.Range (0.0f, 1.0f) <= m_lootChance) {
			var rand = Random.Range (0.0f, 1.0f);
			var counter = 0.0f;
			for(var i = 0; i < m_odds.Count; i++) {
				counter += m_odds[i];
				if(rand <= counter) {
					return m_lootPrefabs[i];
				}
			}
		}
		return null;
	}
	
	public static GameObject GetLoot() {
		return Instance ().GetRandomPrefab();
	}
}
