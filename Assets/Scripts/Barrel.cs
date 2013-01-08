using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrel : MonoBehaviour {
	
	public GameObject m_fishPrefab;
	public int m_numFishToGenerate;
	public Vector3 m_genMin;
	public Vector3 m_genMax;
	public List<GameObject> m_fish;
	
	void Start() {
		m_fish = new List<GameObject>();
		for(var i = 0; i < m_numFishToGenerate; i++) {
			GenerateFish();
		}
	}
	
	void GenerateFish() {
		var go = (GameObject)GameObject.Instantiate(m_fishPrefab);
		go.transform.parent = transform;
		go.transform.localPosition = new Vector3(
			Random.Range (m_genMin.x, m_genMax.x), 
			Random.Range (m_genMin.y, m_genMax.y),
			Random.Range (m_genMin.z, m_genMax.z));
		go.transform.localRotation = Quaternion.Euler(0, Random.Range (0, 360), 0);
		m_fish.Add (go);
	}
}
