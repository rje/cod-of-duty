using UnityEngine;
using System.Collections;

public struct ShotInfo {
	public GameObject m_shooter;
	public int m_damage;
	
	public ShotInfo(GameObject shooter, int damage) {
		m_shooter = shooter;
		m_damage = damage;
	}
}

public class Shootable : MonoBehaviour {
	
	public GameObject m_messageTarget;
	
	public void ShotBy(GameObject go, int damage) {
		var info = new ShotInfo(go, damage);
		m_messageTarget.SendMessage("OnShotBy", info, SendMessageOptions.RequireReceiver);
	}
}
