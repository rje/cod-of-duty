using UnityEngine;
using System.Collections;

public class SpawnArea : MonoBehaviour {
	
	public BoxCollider m_collider;

	public Vector3 GetRandomPoint() {
		var wd2 = m_collider.extents.x / 2.0f;
		var hd2 = m_collider.extents.z / 2.0f;
		var randPos = new Vector3(Random.Range (-wd2, wd2), 0, Random.Range (-hd2, hd2));
		return transform.TransformPoint(randPos);
	}
}
