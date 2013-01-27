using UnityEngine;
using System.Collections;

public struct ShotInfo {
	public GameObject m_shooter;
	public int m_damage;
	public Vector3 m_location;
	
	public ShotInfo(GameObject shooter, Vector3 location, int damage) {
		m_shooter = shooter;
		m_damage = damage;
		m_location = location;
	}
}

public class Shootable : MonoBehaviour {
	
	public GameObject m_messageTarget;
	
	public void ShotBy(GameObject go, Vector3 location, int damage) {
		var info = new ShotInfo(go, location, damage);
		m_messageTarget.SendMessage("OnShotBy", info, SendMessageOptions.RequireReceiver);
	}
}
