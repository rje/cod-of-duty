using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInit : MonoBehaviour {
	
	public GameObject m_barrelPrefab;
	public int m_numBarrels;
	
	public List<Barrel> m_barrels;
	
	void Start() {
		m_barrels = new List<Barrel>();
		for(var i = 0; i < m_numBarrels; i++) {
			var go = (GameObject)GameObject.Instantiate(m_barrelPrefab);
			go.transform.position = new Vector3(Random.Range (-30, 30), 0, Random.Range (-30, 30));
			m_barrels.Add (go.GetComponent<Barrel>());
		}
	}
}
