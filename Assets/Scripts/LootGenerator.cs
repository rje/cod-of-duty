using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootGenerator : MonoBehaviour {
	public List<GameObject> m_lootPrefabs;
	public float m_lootChance;
	
	static LootGenerator sm_instance;
	
	static LootGenerator Instance() {
		if(sm_instance == null) {
			sm_instance = GameObject.FindGameObjectWithTag("loot").GetComponent<LootGenerator>();
		}
		return sm_instance;
	}
	
	GameObject GetRandomPrefab() {
		/*
		if(Random.Range (0.0f, 1.0f) <= m_lootChance) {
			return m_lootPrefabs[Random.Range (0, m_lootPrefabs.Count)];
		}
		*/
		return m_lootPrefabs[2];
		//return null;
	}
	
	public static GameObject GetLoot() {
		return Instance ().GetRandomPrefab();
	}
}
